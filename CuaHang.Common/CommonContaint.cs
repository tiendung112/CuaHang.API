namespace CuaHang.Common
{
    public class CommonContaint
    {
        public const string Id = "Id";
        public const string Username = "Username";

        public static class ExceptionMessage
        {
            public const string NOT_FOUND = "{0} not found.";
            public const string ITEM_NOT_FOUND = "Item not found.";
            public const string ALREADY_EXIST = "{0} already exist.";
            public const string SUCCESS = "{0} success.";
            public const string SHOULD_GREATER_TODAY = "{0} Date is late.";
            public const string INVALID = "{0} invalid.";
            public const string EMAIL_NOT_ACTIVATED = "Email not activated";
            public const string FILE_NOT_FOUND = "File {0} not found";
            public const string EXPIRATION_TIME = "Expiration time";
            public const string VERIFIED = "Account has been verified";
            public const string EXPIRED = "Confirmation code has expired";
            public const string EMAIL_EXIST = "Email is exist";
            public const string USer_EXIST = "USer is exist";

        }

        public static class ContextItem
        {

            public const string PERMISSIONS = "Permissions";
        }
        public class MailMessage
        {
            public const string TIEU_DE_MAIL_YEU_CAU_LIEN_HE = "[{0}] Yêu cầu liên hệ đến bộ phận {1}";
            public const string NOI_DUNG_MAIL_YEU_CAU_LIEN_HE = "<h2> Bộ phận {0} nhận yêu cầu liên hệ với nội dung như sau: </h2><p> ID: {1} </p><p> Họ tên: {2} </p> <p>Số điện thoại: {3} </p><p> Email: {4} </p><p> Nội dung: </p><blockquote><p> {5} </p> </blockquote>";
            public const string changePassSucces = "Đổi mật khẩu thành công";
            public const string subjectChangePass = "Nhận mã xác nhận để tạo mật khẩu mới từ đây: ";
            public const string ContentChangePass = "Mã kích hoạt của bạn là: {0}, mã này sẽ hết hạn sau 5 phút";
            public const string SendCodeChangPassSucces = "Gửi mã xác nhận về email thành công, vui lòng kiểm tra email";
            public const string newCodeAccount = "Xác Nhận Đăng Ký Tài Khoản mới ";
            public const string inforCodeAccount = "Mã kích hoạt của bạn là: {0}, mã này sẽ hết hạn sau 15 phút";
            public const string creatAccountNoti = "Bạn đã gửi yêu cầu đăng ký tài khoản, vui lòng nhập mã xác nhận đã được gửi về email của bạn"; }

        public class AppSettingKeys
        {
            public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]";
            public const string DEFAULT_CONNECTION = "DefaultConnection";
            public const string AUTH_SECRET = "AuthSecret";
        }
        public static class ToKen
        {
            public const string AUTH_SECRET = "AppSettings:Secretkey";
            public const string SecretKey = "jrgwwpnhkussqbminqoyoysfrrfexbrd";
            public const string ToKenNotValid = "Token Không hợp lệ";
            public const string RefreshTokenNotExist = "Refresh Token không tồn tại";
            public const string ToKenUnexpired = "Token chưa hết hạn";
            public const string RenewTokenSuccess = "Làm mới token thành công";
        }
        public static class JWT
        {
            public const string SchemeName = "Bearer";
            public const string BearerFormat = "JWT";
            public const string Description = "Nhập token vào đây";
            public const string Authorization = "Authorization";
        }
        public static class ErrorText
        {
            public const string notEnoughInfor = "Chưa điền đầy đủ thông tin ";
        }
        public static class Router
        {
            public const string Route = "api/[controller]/[action]";

        }
        public static class Role
        {
            public const string RoleId = "RoleId";
            public const string ADMIN = "ADMIN";
            public const string MOD = "MOD";
        }
        public static class Image
        {
            public const string ImageNotFound = "Ảnh không hợp lệ";
            public const string ImageAvatarDefault = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
        }
        public static class Code
        {
            public const string CodeIncorrect = "Mã xác nhận không chính xác";
            public const string CodeExpired = "Mã xác nhận đã hết hạn";
        }




    }
}