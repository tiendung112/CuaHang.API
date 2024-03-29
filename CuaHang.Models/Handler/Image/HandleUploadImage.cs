﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;

namespace CuaHang.Models.Handler.Image
{
    public class HandleUploadImage
    {
        // Thông tin đăng nhập Cloudinary
        static string cloudName = "defwlfzwl";
        static string apiKey = "619677395578241";
        static string apiSecret = "opfhYWKT1zDUbPWD4qGWBGH1gJg";

        // Tạo đối tượng tài khoản và đối tượng Cloudinary
        static public Account account = new Account(cloudName, apiKey, apiSecret);
        static public Cloudinary _cloudinary = new Cloudinary(account);

        // Phương thức để tải lên một hình ảnh lên Cloudinary
        public static async Task<string> Upfile(IFormFile file,string duongdan)
        {
            // Kiểm tra xem tập tin có null hoặc rỗng không
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Không có tập tin được chọn.");
            }
            // Mở một luồng để đọc tập tin
            using (var stream = file.OpenReadStream())
            {
                // Thiết lập các tham số cho việc tải lên hình ảnh lên Cloudinary
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream), 
                    PublicId = $"{duongdan}/" + $"{duongdan}" + DateTime.Now.Ticks + "image", 
                    Transformation = new Transformation().Width(300).Height(400).Crop("fill") 
                };
                //thêm phân chia foulder theo account
                // Tải lên hình ảnh lên Cloudinary
                var uploadResult = await HandleUploadImage._cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message); 
                }

                string imageUrl = uploadResult.SecureUrl.ToString();

                // Trả về URL của hình ảnh đã tải lên
                return imageUrl;
            }
        }
    }
}
