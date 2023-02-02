using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model.Helpers
{
    internal class DataBaseHelper
    {
        private static DataBaseEntities _dataBaseEntities;
        public DataBaseHelper() => _dataBaseEntities = DataBaseEntities.GetContext();

        public User SearchAccount(string login, string password) => _dataBaseEntities.User.Where(x => x.UserLogin == login && x.UserPassword == password).FirstOrDefault();

        public List<Product> GetProductList() => _dataBaseEntities.Product.ToList();

        public List<Product> GetOrderedProductsList() => _dataBaseEntities.Product.ToList().OrderBy(x => x.CostWithDiscount).ToList();
        public List<Product> GetOrderedByDescendingList() => _dataBaseEntities.Product.ToList().OrderByDescending(x => x.CostWithDiscount).ToList();

    }

}
