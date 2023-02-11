using OOORUL.Model.Core;
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
        public List<PickupPoint> GetPickupPointList() => _dataBaseEntities.PickupPoint.ToList();

        public string[] GetPickupPointsListString()
        {
            var listProduct = GetPickupPointList();
            var templist = new string[listProduct.Count];
            for (int i = 0; i < templist.Length; i++)
                templist[i] = listProduct[i].Address;
            return templist;
        }

        public Order CreateOrder(int pickupPointID, User user, List<Product> products)
        {
            Random random = new Random();
            var date = DateTime.Now;
            if (products.Any(p => p.ProductQuantityInStock < 3))
                date = date.AddDays(6);
            else
                date = date.AddDays(3);

            Order order = new Order()
            {
                OrderStatus = 1,
                OrderDate = DateTime.Now,
                OrderPickupPoint = pickupPointID+1,
                OrderDeliveryDate = date,
                ReceiptCode = random.Next(100, 1000),
                CurrentFullName = user != null ? $"{user.UserSurname} {user.UserName} {user.UserPatronymic}" : "Гость",
            }; 
            _dataBaseEntities.Order.Add(order);

            for (int i = 0; i < products.Count; i++)
            {
                OrderProduct orderProduct = new OrderProduct()
                {
                    OrderID = order.OrderID,
                    ProductArticleNumber = products[i].ProductArticleNumber,
                    CountProduct = 1,
                };
                _dataBaseEntities.OrderProduct.Add(orderProduct);
            }
            _dataBaseEntities.SaveChanges();
            return order;
        }
    }

}
