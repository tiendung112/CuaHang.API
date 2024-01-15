using CuaHang.Models.Handler.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CuaHang.Models.Entities;
using CuaHang.Repositories.Repo.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CuaHang.Models.Handler.Image;

namespace CuaHang.Business.Implements
{
    public class BaseServices
    {
        
        public int GenerateCodeActive()
        {
            Random random = new Random();
            return random.Next(100000, 999999);
        }
        public string SendEmail(EmailTo emailTo)
        {
            if (!Validate.IsValidEmail(emailTo.Mail))
            {
                return "Định dạng email không hợp lệ";
            }
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dung0112.dev.test@gmail.com", "xssyibnbzpqhmzsz"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("dung0112.dev.test@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }
        public async Task<string> FileToURL(IFormFile file, int spid)
        {
            string url = "";
            int imageSize = 2 * 2048 * 1024;
            if (file != null)
            {
                if (!HandleImage.IsImage(file, imageSize))
                {
                    throw new Exception("Ảnh không hợp lệ");
                }
                else
                {
                    var anhSP = await HandleUploadImage.Upfile(file, $"/TTLTSEDU/Product/{spid}");
                    url = anhSP == "" ? "ảnh của sản phẩm" : anhSP;
                }
            }
            return url;
        }
    }
}
