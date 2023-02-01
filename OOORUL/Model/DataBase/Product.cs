//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OOORUL.Model.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderProduct = new HashSet<OrderProduct>();
        }
    
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductCategory { get; set; }
        public string ProductImage { get; set; }
        public int ProductManufacturer { get; set; }
        public decimal ProductCost { get; set; }
        public Nullable<byte> ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
        public string Unit { get; set; }
        public byte MaxDiscountAmount { get; set; }
        public int Supplier { get; set; }
        public Nullable<int> CountInPack { get; set; }
        public Nullable<int> MinCount { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Maker Maker { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual Provider Provider { get; set; }

        public string Background
        {
            get
            {
                if (this.MaxDiscountAmount > 15)
                    return "#7fff00";
                return "#fff";
            }
        }

        public string CostWithDiscount
        {
            get
            {
                if(this.MaxDiscountAmount > 0)
                {
                    var CostWithDiscount = Convert.ToDouble(this.ProductCost) - Convert.ToDouble(this.ProductCost) * Convert.ToDouble(this.ProductDiscountAmount / 100.00);
                    return CostWithDiscount.ToString();
                }
                return this.ProductCost.ToString();
            }
        }
    }
}
