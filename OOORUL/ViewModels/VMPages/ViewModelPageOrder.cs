using OOORUL.Model;
using OOORUL.Model.Core;
using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelPageOrder : ViewModelBase
    {
        // Конструктор
        // -------------------------

        internal ViewModelPageOrder()
        {
            UpdateListView();
            CalculateTotalPrice();
        }

        // Поля
        // -------------------------

        public Product SelectedProduct { get; set; }

        private ObservableCollection<Product> _listBuscetProduct;
        public ObservableCollection<Product> ListBuscetProduct
        {
            get { return _listBuscetProduct; }
            set { _listBuscetProduct = value; onPropertyChanged(nameof(ListBuscetProduct)); }
        }

        private double _totalPrice = 0;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; onPropertyChanged(nameof(TotalPrice)); }
        }

        // Обработка кнопок
        // -------------------------

        private RelayCommand _deleteAction;
        public RelayCommand DeleteAction
        {
            get
            {
                return _deleteAction ?? (_deleteAction = new RelayCommand(x => 
                {
                    if(MessageBox.Show("Вы уверены что хотите удалить этот товар?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        DataMediator.DeleteProductFromBusket(SelectedProduct);
                    UpdateListView();
                }));
            }
        }

        // Функции
        // -------------------------

        public void CalculateTotalPrice()
        {
            foreach (var product in _listBuscetProduct)
                TotalPrice += product.CostWithDiscount;
        }

        public void UpdateListView() => ListBuscetProduct = DataMediator.GetListBuscetProduct().ToProductObsarvableCollection();

    }
}
