using OOORUL.Model;
using OOORUL.Model.Core;
using OOORUL.Model.DataBase;
using OOORUL.Model.Helpers;
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
            PickupPoints = dataBaseHelper.GetPickupPointList();
        }

        // Поля
        // -------------------------

        private DataBaseHelper dataBaseHelper = new DataBaseHelper(); 
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

        public string[] AuthorizatedUser
        {
            get
            {
                var temp = new string[1];
                if (!DataMediator.IsUserAuthorizated())
                    temp[0] = "Гость";

                if (DataMediator.IsUserAuthorizated())
                    temp[0] = $"{DataMediator.GetUserSurname()} {DataMediator.GetUserName()} {DataMediator.GetUserPatronymic()}";

                return temp;
            }
        }

        public List<PickupPoint> PickupPoints { get; set; }

        private int _selectedIndexPickupPoint;
        public int SelectedIndexPickupPoint
        {
            get { return _selectedIndexPickupPoint; }
            set { _selectedIndexPickupPoint = value; onPropertyChanged(nameof(SelectedIndexPickupPoint)); }
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
                    CalculateTotalPrice();
                }));
            }
        }

        private RelayCommand _orderAction;
        public RelayCommand OrderAction
        {
            get
            {
                return _orderAction ?? (_orderAction = new RelayCommand(x =>
                {
                    OrderProcess();
                }));
            }
        }

        // Функции
        // -------------------------

        private void OrderProcess()
        {
            try
            {
                var order = dataBaseHelper.CreateOrder(SelectedIndexPickupPoint, DataMediator.user, ListBuscetProduct.ToList());
                DataMediator.SetActualOrder(order);
                MessageBox.Show("Заказ успешно создан!");
                PageChangeMediator.Transit("TransitToPageOrderTicket");
            }
            catch
            {

            }
        }

        public void CalculateTotalPrice()
        {
            TotalPrice = 0;
            foreach (var product in _listBuscetProduct)
                TotalPrice += product.CostWithDiscount;
        }

        public void UpdateListView() => ListBuscetProduct = DataMediator.GetListBuscetProduct().ToProductObsarvableCollection();

    }
}
