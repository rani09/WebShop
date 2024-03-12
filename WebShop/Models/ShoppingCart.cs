using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Items = new List<OrderItem>();
        }

        public List<OrderItem> Items { get; set; }

        public static ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Current.Session["basket"] == null)
                HttpContext.Current.Session["basket"] = new ShoppingCart();

            return (ShoppingCart)HttpContext.Current.Session["basket"];
        }

        public decimal TotalPrice(bool withShippingfee = false)
        {
            if (withShippingfee)
                return Items.Sum(i => i.LineTotal) + ShippingFee();

            return Items.Sum(i => i.LineTotal);
        }

        public int TotalCount()
        {
            return Items.Sum(i => i.Quantity);
        }

        public int ShippingFee()
        {
            if (this.TotalPrice() > 500)
                return 0;

            return 49;
        }

        private int GetIndex(int productID, int variantID)
        {
            return Items.FindIndex(i => i.ProductId == productID && i.VariantID == variantID);
        }

        public void Update(ApplicationDbContext db, int productID, int variantID, int amount, bool add = true)
        {
            Product item = db.Products.Find(productID);
            if (item != null)
            {
                int index = GetIndex(productID, variantID);
                if (index == -1)
                {
                    OrderItem line = new OrderItem();
                    line.ProductId = productID;
                    line.Name = item.Title;
                    line.Image = item.Image;
                    line.Description = item.Description;
                    line.Quantity = amount;
                    line.Price = item.Price;
                    line.VariantID = variantID;

                    //if (variantID != 0)
                    //{
                    //    ProductVariant variant = db.ProductVariants.Find(variantID);
                    //    line.Description += " " + variant.Title;
                    //}

                    Items.Add(line);
                }
                else
                {
                    if (add)
                        Items[index].Quantity += amount;
                    else
                        Items[index].Quantity = amount;
                }
            }
        }

        public void Delete(int dealID, int variantID)
        {
            int index = GetIndex(dealID, variantID);
            if (index != -1)
            {
                if (Items[index].Quantity > 1)
                    Items[index].Quantity -= 1;
                else
                    Items.RemoveAt(index);
            }

            if (Items.Count == 0)
                Items.Clear();
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}