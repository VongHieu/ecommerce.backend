﻿namespace shopecommerce.Domain.Entities
{
    public class ProductsPrices
    {
        public ProductsPrices()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public string id { get; set; }
        public decimal weight { get; set; }
        public decimal price { get; set; }
        public decimal? price_sale { get; set; }
        public string product_id { get; set; }
        #endregion

        #region Generated Relationships
        public virtual Products product_Products { get; set; }
        #endregion

        public void SetPriceSale(int discount)
        {
            this.price_sale = this.price - (this.price * discount / 100);
        }

        public void SetPriceAndWeight(decimal price, decimal weight)
        {
            this.price = price;
            this.weight = weight;
        }
    }
}
