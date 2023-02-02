using OOORUL.Model.Helpers;
using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelListProduct : ViewModelBase
    {
        internal ViewModelListProduct()
        {
            ProductList = dataBaseHelper.GetProductList();
        }


        // | База данных, ага
        DataBaseHelper dataBaseHelper = new DataBaseHelper();

        private List<Product> _productList;
        public List<Product> ProductList
        {
            get { return _productList; }
            set { _productList = value; onPropertyChanged(nameof(ProductList)); }
        }

        // | Поля для объектов формы

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
        }

        public List<Product> GetFilteredListByDiscount(List<Product> products, int min, int max) => products.Where(x => x.ProductDiscountAmount >= min && x.ProductDiscountAmount < max).ToList();

    }
}
