using CuaHang.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.ResponsesModels.Product
{
    public class ProductResponseModel
    {
        public int Product_ID { get; set; }
        public int product_typeID { get; set; }
        public string? name_product { get; set; }
        public double? price { get; set; }
        public string? avartar_image_product { get; set; }
        public string? title { get; set; }
        public int discount { get; set; }
        public int status { get; set; }
        public int number_of_views { get; set; }

        public ProductResponseModel(Entities.Product product)
        {
            Product_ID = product.Id;
            product_typeID = product.product_typeID;
            name_product = product.name_product;
            price = product.price;
            avartar_image_product = product.avartar_image_product;
            title = product.title;
            discount = product.discount;
            status = product.status;
            number_of_views = product.number_of_views;

        }
        public ProductResponseModel()
        {
            
        }
    }
}
