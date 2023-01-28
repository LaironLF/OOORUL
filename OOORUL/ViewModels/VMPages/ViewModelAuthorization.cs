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
        private RelayCommand _transitToPageGuestAthorization;
        public RelayCommand TransitToPageGuestAthorization
        {
            get
            {
                return _transitToPageGuestAthorization ?? (_transitToPageGuestAthorization = new RelayCommand(x =>
                {
                    PageChangeMediator.Transit("TransitToGuestAutho");
                }));
            }
        }
    }
}
