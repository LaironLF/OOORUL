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
        private static Order _order;


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

        public static void DeleteProductFromBusket(Product product) => buscetProducts.Remove(product);
        public static List<Product> GetListBuscetProduct() => buscetProducts;

        public static bool IsUserAuthorizated() => user != null;

        public static string GetUserName() => user.UserName;
        public static string GetUserSurname() => user.UserSurname;
        public static string GetUserPatronymic() => user.UserPatronymic;
        public static void SetActualOrder(Order order) => _order = order;
        public static Order GetActualOrder() => _order;

        public static DateTime GetOrderDate() => _order.OrderDate;
        public static DateTime GetOrdetDeliveryDate() => _order.OrderDeliveryDate;
        public static int GetOrderCode() => _order.ReceiptCode;
        public static PickupPoint GetPickupPoint() => _order.PickupPoint;
        public static int GetOrderId() => _order.OrderID;

    }
}
