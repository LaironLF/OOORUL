using OOORUL.Model;
using OOORUL.ViewModels.VMPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.ViewModels.VMWindows
{
    internal class ViewModelWindowOrder : ViewModelBase
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

        public List<ViewModelBase> ViewModels { get; set; } = new List<ViewModelBase>
        {
            new ViewModelPageOrder(),
        };

        // ****************************************
        // Определяем общую команду смены ВьюМодели
        // ****************************************

        private void ChangeViewModel(ViewModelBase viewModel) => CurrentViewModel = viewModel;


        // Конструктор

        public ViewModelWindowOrder()
        {
            // Добавляем в страницы переходов команды перехода.

            PageChangeMediator.AddAction("TransitToPageOrder", x => ChangeViewModel(ViewModels[0]));
            PageChangeMediator.AddAction("TransitToPageOrderTicket", x => ChangeViewModel(new ViewModelPageOrderTicket()));

            // Устанавливаем стартовый экран

            PageChangeMediator.Transit("TransitToPageOrder");
        }
    }
}
