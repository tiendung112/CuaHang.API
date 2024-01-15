using CuaHang.Models.ResponsesModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuaHang.Models.ResponsesModels
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public AccountResponseModels UserData { get; set; }
    }
}
