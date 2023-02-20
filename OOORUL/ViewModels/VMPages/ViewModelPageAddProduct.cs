using Microsoft.Win32;
using OOORUL.Model;
using OOORUL.Model.DataBase;
using OOORUL.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelPageAddProduct : ViewModelBase
    {

        // Конструкторы
        // --------------------------------

        internal ViewModelPageAddProduct(Product product)
        {
            SelectedProduct = product;
        }
        internal ViewModelPageAddProduct()
        {
            UpdateLists();
            if (SelectedProduct != null) UpdateFields();
        }

        // Поля
        // --------------------------------

        private static Product SelectedProduct;
        private DataBaseHelper dataBaseHelper = new DataBaseHelper();

        private string _articulField;
        public string ArticulField
        {
            get { return _articulField; }
            set { _articulField = value; onPropertyChanged(nameof(ArticulField)); }
        }

        private bool _articulFieldIsInteractble = true;
        public bool ArticulFieldIsInteractble
        {
            get { return _articulFieldIsInteractble; }
            set { _articulFieldIsInteractble = value; onPropertyChanged(nameof(ArticulFieldIsInteractble)); }
        }

        private bool _deleteButtonIsInteractble = false;
        public bool DeleteButtonIsInteractble
        {
            get { return _deleteButtonIsInteractble; }
            set { _deleteButtonIsInteractble = value; onPropertyChanged(nameof(DeleteButtonIsInteractble)); }
        }

        private string _nameField;
        public string NameField
        {
            get { return _nameField; }
            set { _nameField = value; onPropertyChanged(nameof(NameField)); }
        }

        private int _countField;
        public int CountField
        {
            get { return _countField; }
            set { _countField = value; onPropertyChanged(nameof(CountField)); }
        }

        private string _countableMetricField;
        public string CountableMetricField
        {
            get { return _countableMetricField; }
            set { _countableMetricField = value; onPropertyChanged(nameof(CountableMetricField)); }
        }

        private int? _countInPackage;
        public int? CountInPackage
        {
            get { return _countInPackage; }
            set { _countInPackage = value; onPropertyChanged(nameof(CountInPackage)); }
        }

        private int? _minimalCountField;
        public int? MinimalCountField
        {
            get { return _minimalCountField; }
            set { _minimalCountField = value; onPropertyChanged(nameof(MinimalCountField)); }
        }

        public List<string> NameOfShippingCompanyField { get; set; }

        private int _selectedIndexCompany;
        public int SelectedIndexCompany
        {
            get { return _selectedIndexCompany; }
            set { _selectedIndexCompany = value; onPropertyChanged(nameof(SelectedIndexCompany)); }
        }

        private int _maximalDiscountingField;
        public int MaximalDiscountingField
        {
            get { return _maximalDiscountingField; }
            set { _maximalDiscountingField = value; onPropertyChanged(nameof(MaximalDiscountingField)); }
        }

        private int? _currentDiscountingField;
        public int? CurrentDiscountingField
        {
            get { return _currentDiscountingField; }
            set { _currentDiscountingField = value; onPropertyChanged(nameof(CurrentDiscountingField)); }
        }

        private decimal _costByOneField;
        public decimal CostByOneField
        {
            get { return _costByOneField; }
            set { _costByOneField = value; onPropertyChanged(nameof(CostByOneField)); }
        }

        private string _descriptionField;
        public string DescriptionField
        {
            get { return _descriptionField; }
            set { _descriptionField = value; onPropertyChanged(nameof(DescriptionField)); }
        }

        private int _selectedIndexCategory;
        public int SelectedIndexCategory
        {
            get { return _selectedIndexCategory; }
            set { _selectedIndexCategory = value; onPropertyChanged(nameof(SelectedIndexCategory)); }
        }

        public List<string> CategoryList { get; set; }

        private int _selectedIndexMaker;
        public int SelectedIndexMaker
        {
            get { return _selectedIndexMaker; }
            set { _selectedIndexMaker = value; onPropertyChanged(nameof(SelectedIndexMaker)); }
        }

        public List<string> MakerFieldList { get; set; }

        private string _imgSource = "picture.png";
        public string ImgSource
        {
            get { return $"pack://application:,,,/Resources/{_imgSource}"; }
            set { _imgSource = value; onPropertyChanged(nameof(ImgSource)); }
        }

        // Обработка кнопочек
        // --------------------------------

        private RelayCommand _addImageAction;
        public RelayCommand AddImageAction
        {
            get
            {
                return _addImageAction ?? (_addImageAction = new RelayCommand(x =>
                {
                    AddImageProcess();
                }));
            }
        }

        private RelayCommand _deleteProductAction;
        public RelayCommand DeleteProductAction
        {
            get
            {
                return _deleteProductAction ?? (_deleteProductAction = new RelayCommand(x =>
                {
                    DeleteProductProcess();
                }));
            }
        }

        private RelayCommand _saveProductAction;
        public RelayCommand SaveProductAction
        {
            get
            {
                return _saveProductAction ?? (_saveProductAction = new RelayCommand(x =>
                {
                    if (SelectedProduct != null) 
                        UpdateProductProcess();
                    else 
                        AddProductProcess();
                }));
            }
        }

        // Функции
        // --------------------------------

        private void DeleteProductProcess()
        {
            try
            {
                if (MessageBox.Show($"Вы действительно хоитете удалить {SelectedProduct.ProductName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dataBaseHelper.DeleteProduct(SelectedProduct);
                    MessageBox.Show($"{SelectedProduct.ProductName} успешно удалён", "информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PageChangeMediator.Transit("TransitToListProduct");
                } 
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddProductProcess()
        {
            try
            {
                if (MessageBox.Show($"Вы действительно хоитете добавить {NameField}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Проверяем корректность данных
                    CheckFieldCorrectable();

                    Product product = CreateLocalProduct();

                    dataBaseHelper.AddProduct(product);
                    MessageBox.Show($"{NameField} успешно добавлен", "информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PageChangeMediator.Transit("TransitToListProduct");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateProductProcess()
        {
            try
            {
                if (MessageBox.Show($"Вы действительно хоитете обновить {SelectedProduct.ProductName}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Проверяем корректность данных
                    CheckFieldCorrectable();

                    Product product = CreateLocalProduct();

                    dataBaseHelper.UpdateProduct(product);
                    MessageBox.Show($"{SelectedProduct.ProductName} успешно обновлён", "информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    PageChangeMediator.Transit("TransitToListProduct");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Product CreateLocalProduct()
        {
            Product product = new Product()
            {
                ProductArticleNumber = ArticulField,
                ProductName = NameField,
                ProductDescription = DescriptionField,
                MinCount = MinimalCountField,
                MaxDiscountAmount = (byte)MaximalDiscountingField,
                ProductDiscountAmount = (byte)CurrentDiscountingField,
                Unit = CountableMetricField,
                ProductImage = _imgSource,
                ProductCost = CostByOneField,

                ProductCategory = SelectedIndexCategory + 1,
                ProductManufacturer = SelectedIndexMaker + 1,
                Supplier = SelectedIndexCompany + 1,

                CountInPack = CountInPackage,
                ProductQuantityInStock = CountField,
            };
            return product;
        }

        private void CheckFieldCorrectable()
        {
            StringBuilder errors = new StringBuilder();

            if (ArticulField == null || ArticulField == "")
                errors.AppendLine("Артикль не может быть пустой!");
            if (MinimalCountField < 0)
                errors.AppendLine("Минимальное количество не может быть меньше нуля!");
            if (CurrentDiscountingField > MaximalDiscountingField)
                errors.AppendLine("Действующая скидка не может быть больше максимальной!");

            if (errors.Length > 0)
                throw new Exception(errors.ToString());
        }

        private void AddImageProcess()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg)| *.png; *.jpg; *.jpeg";
            openFileDialog.InitialDirectory = @"C:\Users\USER\Desktop\Задачи\Лабораторная 1\OOORUL\OOORUL\Resources";
            if (openFileDialog.ShowDialog() == true)
            {
                ImgSource = openFileDialog.SafeFileName;
            }
        }

        private void UpdateLists()
        {
            NameOfShippingCompanyField = dataBaseHelper.GetProviderList().Select(x => x.NameProvider).ToList();
            CategoryList = dataBaseHelper.GetCategoryList().Select(x => x.NameCategory).ToList();
            MakerFieldList = dataBaseHelper.GetMakersList().Select(x => x.NameMaker).ToList();
        }

        private void UpdateFields()
        {
            ArticulFieldIsInteractble = false;
            DeleteButtonIsInteractble = true;

            ArticulField = SelectedProduct.ProductArticleNumber;
            NameField = SelectedProduct.ProductName;
            CountField = SelectedProduct.ProductQuantityInStock;
            CountableMetricField = SelectedProduct.Unit;
            CountInPackage = SelectedProduct.CountInPack;
            MinimalCountField = SelectedProduct.MinCount;

            SelectedIndexCompany = SelectedProduct.Supplier - 1;
            SelectedIndexCategory = SelectedProduct.ProductCategory - 1;
            SelectedIndexMaker = SelectedProduct.ProductManufacturer - 1;

            MaximalDiscountingField = SelectedProduct.MaxDiscountAmount;
            CurrentDiscountingField = SelectedProduct.ProductDiscountAmount;
            CostByOneField = SelectedProduct.ProductCost;
            DescriptionField = SelectedProduct.ProductDescription;
            ImgSource = SelectedProduct.ProductImage;
        }

    }
}
