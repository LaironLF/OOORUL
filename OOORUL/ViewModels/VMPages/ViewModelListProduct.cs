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
        // | База данных, ага
        DataBaseHelper dataBaseHelper = new DataBaseHelper();

        public List<Product> ProductList
        {
            get { return dataBaseHelper.GetProductList(); }
        }

    }
}
