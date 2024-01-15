using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.Requests
{
    public class BasePaginationInpu
    {
        public int PageSize { get; set; } = 10;
        public int PageNo { get; set; } = 1; 
        public string Keyword { get; set; } = string.Empty;
    }
}
