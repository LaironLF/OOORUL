using OOORUL.Model.Helpers;
using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOORUL.Model.Core;
using OOORUL.Model;
using System.Windows;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelListProduct : ViewModelBase
    {
        // | Конструкторы
        // --------------------------------------------------

        internal ViewModelListProduct()
        {
            if (DataMediator.user == null) 
                UserFullname = "Гость";
            else 
                UserFullname = $"{DataMediator.user.UserSurname} {DataMediator.user.UserName} {DataMediator.user.UserPatronymic}";

            if (DataMediator.user.UserRole == 3) AddBtnVisible = true;
            UpdateSortList();
        }

        // | База данных, ага
        // ---------------------------------------------------
        DataBaseHelper dataBaseHelper = new DataBaseHelper();

        private List<Product> _productList;
        public List<Product> ProductList
        {
            get { return _productList; }
            set { _productList = value; onPropertyChanged(nameof(ProductList)); }
        }

        // | Поля для объектов формы
        // -----------------------------------------------------

        public string UserFullname { get; set; }
        public List<Product> SelectedProducts { get; set; }

        private bool _btnVisible = false;
        public bool BtnVisible
        {
            get { return _btnVisible; }
            set { _btnVisible = value; onPropertyChanged(nameof(BtnVisible)); }
        }

        private bool _addBtnVisible = false;
        public bool AddBtnVisible
        {
            get { return _addBtnVisible; }
            set { _addBtnVisible = value; onPropertyChanged(nameof(AddBtnVisible)); }
        }

        private int _productCount = 0;
        public int ProductCount
        {
            get { return _productCount; }
            set { _productCount = value; onPropertyChanged(nameof(ProductCount)); }
        }

        private string _searchField;
        public string SearchField
        {
            get { return _searchField; }
            set
            {
                _searchField = value;
                UpdateSortList();
                onPropertyChanged(nameof(SearchField));
            }
        }

        private int _productSubstractedCount = 0;
        public int ProductSubstractedCount
        {
            get { return _productSubstractedCount; }
            set { _productSubstractedCount = value; onPropertyChanged(nameof(ProductSubstractedCount)); }
        }

        private int _selectedIndexSortingList = 0;
        public int SelectedIndexSortingList
        {
            get { return _selectedIndexSortingList; }
            set 
            { 
                _selectedIndexSortingList = value; 
                UpdateSortList(); 
                onPropertyChanged(nameof(SelectedIndexSortingList)); 
            }
        }

        private int _selectedIndexFilterList = 0;
        public int SelectedIndexFilterList
        {
            get { return _selectedIndexFilterList; }
            set 
            { 
                _selectedIndexFilterList = value;
                UpdateSortList();
                onPropertyChanged(nameof(SelectedIndexFilterList)); 
            }
        }

        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию"
        };

        public string[] FilterList { get; set; } =
        {
            "Все диапазоны",
            "0% - 9,99%",
            "10% - 14,99%",
            "15% и более"
        };

        // | Обработка кнопочек
        // ---------------------------------------------------

        private RelayCommand _addToBuscetAction;
        public RelayCommand AddToBuscetAction
        {
            get
            {
                return _addToBuscetAction ?? (_addToBuscetAction = new RelayCommand(x =>
                {
                    AddToBuscetProcess();
                }));
            }
        }

        private RelayCommand _transitToOrderAction;
        public RelayCommand TransitToOrderAction
        {
            get
            {
                return _transitToOrderAction ?? (_transitToOrderAction = new RelayCommand(x =>
                {
                    PageChangeMediator.Transit("OpenWindowOrder");
                }));
            }
        }

        private RelayCommand _transitToAddProductAction;
        public RelayCommand TransitToAddProductAction
        {
            get
            {
                return _transitToAddProductAction ?? (_transitToAddProductAction = new RelayCommand(x =>
                {

                }));
            }
        }


        // | Функции
        // ----------------------------------------------------

        private void AddToBuscetProcess()
        {
            DataMediator.AddToBuscetList(SelectedProducts);
            if (SelectedProducts.Count > 1)
                MessageBox.Show("Товары добавлены в корзину", "Консультант");
            else
                MessageBox.Show("Товар добавлен в корзину", "Консультант");
            BtnVisible = true;
        }

        private void UpdateSortList()
        {
            switch (SelectedIndexSortingList)
            {
                case 0:
                    ProductList = dataBaseHelper.GetProductList();
                    break;
                case 1:
                    ProductList = dataBaseHelper.GetOrderedProductsList();
                    break;
                case 2:
                    ProductList = dataBaseHelper.GetOrderedByDescendingList();
                    break;
            }
            ProductCount = ProductList.Count();
            switch (SelectedIndexFilterList)
            {
                case 1:
                    ProductList = GetFilteredListByDiscount(ProductList, 1, 10);
                    break;
                case 2:
                    ProductList = GetFilteredListByDiscount(ProductList, 10, 15);
                    break;
                case 3:
                    ProductList = GetFilteredListByDiscount(ProductList, 15, 101);
                    break;
            }
            if (SearchField != "" && SearchField != null)
                ProductList = ProductList.Where(x => x.ProductName.ToLower().Contains(SearchField.ToLower())).ToList();
            ProductSubstractedCount = ProductList.Count();
        }

        private List<Product> GetFilteredListByDiscount(List<Product> products, int min, int max) 
            => products.Where(x => x.ProductDiscountAmount >= min && x.ProductDiscountAmount < max).ToList();


    }
}
