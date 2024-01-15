using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("XacNhanEmail")]
    public class EmailValidate : BaseEntity
    {
        public int TaiKhoanID { get; set; }
        public TaiKhoan? TaiKhoan { get; set; }
        public DateTime? ThoiGianHetHan { get; set; }
        public int? MaXacNhan { get; set; }
        public bool DaXacNhan { get; set; } = false;

    }
}
