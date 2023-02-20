using OOORUL.Model;
using OOORUL.Model.Core;
using OOORUL.Model.DataBase;
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
                UpdateButton();
                onPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private bool buttonVisible;
        public bool ButtonVisible
        {
            get { return buttonVisible; }
            set 
            { 
                buttonVisible = value; 
                onPropertyChanged(nameof(ButtonVisible)); 
            }
        }

        private string buttonContent;
        public string ButtonContent
        {
            get { return buttonContent; }
            set { buttonContent = value; onPropertyChanged(nameof(ButtonContent)); }
        }

        private RelayCommand buttonAction;
        public RelayCommand ButtonAction
        {
            get
            {
                return buttonAction ?? (buttonAction = new RelayCommand(x =>
                {
                    ButtonProcess();
                }));
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

        // Функции всякие там

        private void ButtonProcess()
        {
            if (CurrentViewModel.GetType() == typeof(ViewModelListProduct))
            {
                PageChangeMediator.Transit("TransitToAutho");
                DataMediator.DeleteData();
            }
            if (CurrentViewModel.GetType() == typeof(ViewModelPageAddProduct))
                PageChangeMediator.Transit("TransitToListProduct");
        }

        private void UpdateButton()
        {
            ButtonVisible = _currentViewModel.GetType() != typeof(ViewModelAuthorization);
            if (_currentViewModel.GetType() == typeof(ViewModelListProduct))
                ButtonContent = "Выход";
            else
                ButtonContent = "Назад";
        }

        public WindowMainViewModel()
        {
            // --- Добавляем действия перехода между экранами ---

            PageChangeMediator.AddAction("TransitToAutho", x => ChangeViewModel(new ViewModelAuthorization()));
            PageChangeMediator.AddAction("TransitToListProduct", x => ChangeViewModel(new ViewModelListProduct()));
            PageChangeMediator.AddAction("OpenWindowOrder", x => OpenWindow(new WindowOrder()));
            PageChangeMediator.AddAction("TransitToPageAddProduct", args =>
            {
                if (args.Length == 0) ChangeViewModel(new ViewModelPageAddProduct(null));
                if (args.Length == 1) ChangeViewModel(new ViewModelPageAddProduct((Product)args[0]));
            });


            // --- выставляем стартовый экран

            PageChangeMediator.Transit("TransitToAutho");
        }
    }
}
