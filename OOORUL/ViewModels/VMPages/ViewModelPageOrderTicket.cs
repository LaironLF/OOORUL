using OOORUL.Model;
using OOORUL.Model.Core;
using OOORUL.Model.DataBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOORUL.ViewModels.VMPages
{
    internal class ViewModelPageOrderTicket : ViewModelBase
    {
        // Конструктор
        // ---------------------------

        public ViewModelPageOrderTicket()
        {
            OrderDate = DataMediator.GetOrderDate();
            DelieverDate = DataMediator.GetOrdetDeliveryDate();
            pickupPoint = DataMediator.GetPickupPoint().Address;
            OrderNum = DataMediator.GetOrderId();
            OrderCode = DataMediator.GetOrderCode();

            var productsBuscket = DataMediator.GetListBuscetProduct();

            foreach (var prod in productsBuscket)
                products += (products == "" ? "" : ", ") + prod.ProductName;

            OrderSumm = productsBuscket.Sum(x => x.CostWithDiscount);
            OrderDiscountingSumm = productsBuscket.Sum(x => (int)x.ProductDiscountAmount);
        }


        // Поля вьюмодельки
        // ---------------------------

        public DateTime OrderDate { get; set; } //
        public DateTime DelieverDate { get; set; } //
        public double OrderSumm { get; set; }
        public int OrderDiscountingSumm { get; set; }
        public string pickupPoint { get; set; } //
        public int OrderCode { get; set; } //
        public int OrderNum { get; set; } //
        public string products { get; set; } = ""; //



        // Обработка кнопок вьюшек
        // ---------------------------



        // Функции
        // ---------------------------

    }
}
