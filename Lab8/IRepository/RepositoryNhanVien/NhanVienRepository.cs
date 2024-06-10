
using Lab8.Model;
using System;

namespace Lab8.IRepository.RepositoryNhanVien
{
    public class NhanVienRepository : IRepositoryNhanVien
    {
        private readonly AppDbContext nhanVienContext;
        
        public NhanVienRepository(AppDbContext nhanVienContext)
        {
            this.nhanVienContext = nhanVienContext;
        }

        public NhanVien AddNhanVien(NhanVien nhanVien)
        {
            try
            {
                nhanVienContext.nhanViens.Add(nhanVien);
                nhanVienContext.SaveChanges();
                return nhanVien;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public void DeleteNhanVien(Guid id)
        {
            var deleteNhanvienId = nhanVienContext.nhanViens.FirstOrDefault(x => x.Id == id);
            try
            {
                nhanVienContext.Remove(deleteNhanvienId);
                nhanVienContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<NhanVien> GetAllNhanVien()
        {
            return nhanVienContext.nhanViens.ToList();
        }

        public NhanVien GetByIdNhanVien(Guid id)
        {
            var findNhanvienById = nhanVienContext.nhanViens.FirstOrDefault(x => x.Id == id);
            if (findNhanvienById == null)
            {
                return null;
            }
            return findNhanvienById;
        }

        

        public NhanVien UpdateNhanVien(NhanVien nhanVien)
        {
            var findIdUpdate = nhanVienContext.nhanViens.FirstOrDefault(l => l.Id == nhanVien.Id);
            try
            {
                if (findIdUpdate == null)
                {
                    return null;
                }
                else
                {
                    findIdUpdate.Ten = nhanVien.Ten;
                    findIdUpdate.Tuoi = nhanVien.Tuoi;
                    findIdUpdate.Role = nhanVien.Role;
                    findIdUpdate.Nam = nhanVien.Nam;
                    findIdUpdate.Email = nhanVien.Email;
                    findIdUpdate.Luong = nhanVien.Luong;
                    findIdUpdate.TrangThai = nhanVien.TrangThai;
                    findIdUpdate.ChucVu = nhanVien.ChucVu;
                    nhanVienContext.nhanViens.Update(findIdUpdate);
                    nhanVienContext.SaveChanges();
                    return findIdUpdate;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
