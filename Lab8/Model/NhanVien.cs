using System.ComponentModel.DataAnnotations;

namespace Lab8.Model
{
    public class NhanVien
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên là bắt buộc.")]
        public string Ten { get; set; }

        [Range(18, 70, ErrorMessage = "Tuổi phải nằm trong khoảng từ 18 đến 70.")]
        public int Tuoi { get; set; }

        [Required(ErrorMessage = "Role là bắt buộc.")]
        public int Role { get; set; }

        [Required(ErrorMessage = "Năm là bắt buộc")]
        public int Nam {  get; set; }   

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; } 

        [Required(ErrorMessage = "Lương là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Lương phải là một số dương.")]
        public int Luong { get; set; }

        public bool TrangThai { get; set; }

        [Required(ErrorMessage = "Chức vụ là bắt buộc.")]
        public string ChucVu { get; set; }
        public string RoleDescription
        {
            get
            {
                return Role == 1 ? "Admin" : Role == 2 ? "Nhân viên" : "Không xác định";
            }
        }
    }
}
