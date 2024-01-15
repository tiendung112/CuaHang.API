using CuaHang.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.RequestsModels.Products
{
    public class GetPageProductRequestModel :BasePaginationInpu
    {
        public int? product_typeID { get; set; }
        public string? keyword { get; set; }
        public int? TuGia { get; set; }
        public int? DenGia { get; set; }
        public string? title { get; set; }

        
    }
}
