using OOORUL.Model;
using OOORUL.ViewModels.VMPages;
using OOORUL.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOORUL.ViewModels
{
    internal class WindowMainViewModel : ViewModelBase
    {

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                onPropertyChanged(nameof(CurrentViewModel));
            }
        }

        // Лист для тех вбюмоделей, для которых нет смысла
        // каждый раз объявлять новый экземпляр

        private List<ViewModelBase> _viewModels;
        public List<ViewModelBase> ViewModels
        {
            get
            {
                if(_viewModels == null )
                    _viewModels = new List<ViewModelBase>();
                return _viewModels;
            }
        }

        // ****************************************
        // Определяем общую команду смены ВьюМодели
        // ****************************************

        private void ChangeViewModel(ViewModelBase viewModel) => CurrentViewModel = viewModel;
        private void OpenWindow(Window window) => window.ShowDialog();


        public WindowMainViewModel()
        {
            // --- Добавляем действия перехода между экранами ---

            PageChangeMediator.AddAction("TransitToAutho", x => ChangeViewModel(new ViewModelAuthorization()));
            PageChangeMediator.AddAction("TransitToListProduct", x => ChangeViewModel(new ViewModelListProduct()));
            PageChangeMediator.AddAction("OpenWindowOrder", x => OpenWindow(new WindowOrder()));


            // --- выставляем стартовый экран

            PageChangeMediator.Transit("TransitToAutho");
        }
    }
}
