using OOORUL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelAuthorization : ViewModelBase
    {
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

        public void AuthorizationProcess()
        {

        }
        
    }
}
