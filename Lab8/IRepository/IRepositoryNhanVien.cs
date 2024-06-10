

using Lab8.Model;

namespace Lab8.IRepository
{
    public interface IRepositoryNhanVien
    {
        IEnumerable<NhanVien> GetAllNhanVien();
        NhanVien GetByIdNhanVien(Guid id);
        NhanVien AddNhanVien(NhanVien nhanVien);
        NhanVien UpdateNhanVien(NhanVien nhanVien);
        void DeleteNhanVien(Guid id);

    }
}
