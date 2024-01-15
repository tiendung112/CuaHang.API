using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.RequestModels
{
    public class SignInData
    {
        public required string Email { get; set; }
        public required string MatKhau { get; set; }
    }
}
