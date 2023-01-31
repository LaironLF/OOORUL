using OOORUL.Model;
using OOORUL.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelAuthorization : ViewModelBase
    {
        // Поля текстовые, ну или обычные переменные
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


        private string CaptchaValue;

        // Тут будет каптча, будем менять её видимость
        // -------------------------------------------

        private bool _tbCaptchaVisible = false;
        public bool TbCaptchaVisible
        {
            get { return _tbCaptchaVisible; }
            set { _tbCaptchaVisible = value; onPropertyChanged(nameof(TbCaptchaVisible)); }
        }

        // Обработка кнопочек
        // -------------------------------------------

        private RelayCommand _btnAuthorizate;
        public RelayCommand BtnAuthorizate
        {
            get
            {
                return _btnAuthorizate ?? (_btnAuthorizate = new RelayCommand(x =>
                {
                    if (TbCaptchaVisible) CaptchaGenerate();
                    AuthorizationProcess();
                }));
            }
        }

        // Функции
        // --------------------------------------------

        private void AuthorizationProcess()
        {
            try
            {
                if (TbCaptchaVisible) CaptchaVerification();

                var dataBaseHelper = new DataBaseHelper();
                var user = dataBaseHelper.SearchAccount(TbLogin, TbPassword);

                if (user == null)
                {
                    TbCaptchaVisible = true;
                    throw new Exception("Неверный логин или пароль");
                }

                MessageBox.Show(user.UserPatronymic, user.UserName);
            }
            catch(Exception e) { MessageBox.Show(e.Message, "Ошибка"); }
        }

        private void CaptchaGenerate()
        {
            CaptchaValue = "Test";
        }

        public void CaptchaVerification()
        {
            if (CaptchaValue != TbCaptchaValue) throw new Exception("Неверная Каптча!");
        }
        
    }
}
