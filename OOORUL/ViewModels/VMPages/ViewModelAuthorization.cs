using OOORUL.Model;
using OOORUL.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelAuthorization : ViewModelBase
    {
        // | Поля текстовые, ну или обычные переменные
        // ------------------------------------------

        private string _tblogin;
        public string TbLogin
        {
            get { return _tblogin; }
            set { _tblogin = value; onPropertyChanged(nameof(TbLogin)); }
        }

        private string _tbPassword;
        public string TbPassword
        {
            get { return _tbPassword; }
            set { _tbPassword = value; onPropertyChanged(nameof(TbPassword)); }
        }

        private string _tbCaptchaValue;
        public string TbCaptchaValue
        {
            get { return _tbCaptchaValue; }
            set { _tbCaptchaValue = value; onPropertyChanged(nameof(TbCaptchaValue)); }
        }

        private ImageSource _captchaImage;
        public ImageSource CaptchaImage
        {
            get { return _captchaImage; }
            set { _captchaImage = value; onPropertyChanged(nameof(CaptchaImage)); }
        }

        private CaptchaGenerator captchaGenerator = new CaptchaGenerator();
        private int UnsuccefullAuthorizationCount = 0;

        // | Тут будет каптча, будем менять её видимость
        // -------------------------------------------

        private bool _tbCaptchaVisible = false;
        public bool TbCaptchaVisible
        {
            get { return _tbCaptchaVisible; }
            set { _tbCaptchaVisible = value; onPropertyChanged(nameof(TbCaptchaVisible)); }
        }

        // | Обработка кнопочек
        // -------------------------------------------

        private RelayCommand _btnAuthorizate;
        public RelayCommand BtnAuthorizate
        {
            get
            {
                return _btnAuthorizate ?? (_btnAuthorizate = new RelayCommand(x =>
                {
                    AuthorizationProcess();
                }));
            }
        }

        // | Функции
        // --------------------------------------------

        private void AuthorizationProcess()
        {
            try
            {
                if (TbCaptchaVisible && captchaGenerator.captchaText != TbCaptchaValue) 
                    throw new Exception("Неверная Каптча!");

                var dataBaseHelper = new DataBaseHelper();
                var user = dataBaseHelper.SearchAccount(TbLogin, TbPassword);

                if (user == null)
                {
                    TbCaptchaVisible = true;
                    throw new Exception("Неверный логин или пароль");
                }

                MessageBox.Show(user.UserPatronymic, user.UserName);
            }
            catch(Exception e) 
            { 
                MessageBox.Show(e.Message, "Ошибка");
                if (UnsuccefullAuthorizationCount > 3) BlockPageProcess();
                UnsuccefullAuthorizationCount++;
            }
            finally
            {
                if (TbCaptchaVisible) CaptchaGenerate();
            }
        }

        private void BlockPageProcess()
        {
            throw new NotImplementedException();
        }

        private void CaptchaGenerate()
        {
            TbCaptchaValue = "";
            captchaGenerator.Generate(200, 50);
            CaptchaImage = captchaGenerator.captchaImage.ToImageSource();
        }
        
    }
}
