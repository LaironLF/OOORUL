using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.Model.Core
{
    internal static class DataMediator
    {
        public static User user;
        private static List<Product> buscetProducts = new List<Product>();


        public static void DeleteData()
        {
            user = null;
        }

        public static void AddToBuscetList(List<Product> products)
        {
            foreach(Product product in products)
                if(!buscetProducts.Contains(product))
                    buscetProducts.Add(product);
        }
    }
}
