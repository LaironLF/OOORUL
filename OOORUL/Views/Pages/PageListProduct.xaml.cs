using OOORUL.Model.DataBase;
using OOORUL.ViewModels.VMPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOORUL.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageListProduct.xaml
    /// </summary>
    public partial class PageListProduct : Page
    {
        public PageListProduct()
        {
            InitializeComponent();
        }

        private void sele(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (ViewModelListProduct)DataContext;
            viewModel.SelectedProducts = lv_products.SelectedItems
                .Cast<Product>()
                .ToList();
        }
    }
}
