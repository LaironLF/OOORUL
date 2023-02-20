using OOORUL.Model.Core;
using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        public List<Maker> GetMakersList() => _dataBaseEntities.Maker.ToList();
        public List<Category> GetCategoryList() => _dataBaseEntities.Category.ToList();
        public List<Provider> GetProviderList() => _dataBaseEntities.Provider.ToList();
        
        public void DeleteProduct(Product product)
        {
            _dataBaseEntities.Product.Remove(product);
            var orderProducts = _dataBaseEntities.OrderProduct.Where(x => x.ProductArticleNumber == product.ProductArticleNumber).ToList();
            orderProducts.ForEach(x => _dataBaseEntities.OrderProduct.Remove(x));
            _dataBaseEntities.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            _dataBaseEntities.Product.Add(product);
            _dataBaseEntities.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            var baseProduct = _dataBaseEntities.Product.FirstOrDefault(x => x.ProductArticleNumber == product.ProductArticleNumber);
            baseProduct.ProductName = product.ProductName;
            baseProduct.ProductManufacturer = product.ProductManufacturer;
            baseProduct.ProductDescription = product.ProductDescription;
            baseProduct.ProductCategory = product.ProductCategory;
            baseProduct.ProductStatus = product.ProductStatus;
            baseProduct.Unit = product.Unit;
            baseProduct.ProductCost = product.ProductCost;
            baseProduct.ProductDiscountAmount = product.ProductDiscountAmount;
            baseProduct.MaxDiscountAmount = product.MaxDiscountAmount;
            baseProduct.ProductImage = product.ProductImage;
            baseProduct.Supplier = product.Supplier;
            baseProduct.CountInPack = product.CountInPack;
            baseProduct.MinCount = product.MinCount;
            baseProduct.ProductQuantityInStock = product.ProductQuantityInStock;
            

            _dataBaseEntities.SaveChanges();
        }

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
