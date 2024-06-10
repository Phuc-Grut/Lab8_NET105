using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab8.Model;
using Newtonsoft.Json;

namespace Lab8WebApp.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly string _connectstring = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=Lab8API;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public NhanViensController()
        {
        }

        // GET: NhanViens
        public async Task<IActionResult> Index()
        {
            List<NhanVien> ListNhanVien = new List<NhanVien>();
            using (var http = new HttpClient())
            {
                using (var response = await http.GetAsync("https://localhost:7294/api/NhanViens"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ListNhanVien = JsonConvert.DeserializeObject<List<NhanVien>>(apiResponse);
                }
            }

            // Gán mô tả vai trò cho từng nhân viên
            

            return View(ListNhanVien);
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NhanVien nhanVien;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectstring);

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                nhanVien = await context.nhanViens.FindAsync(id);
                if (nhanVien == null)
                {
                    return NotFound();
                }
            }

            // Gán mô tả vai trò cho nhân viên
            

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhanViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Tuoi,Role,Nam,Email,Luong,TrangThai,ChucVu")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(_connectstring);
                using (var context = new AppDbContext(optionsBuilder.Options))
                {
                    context.Add(nhanVien);
                    await context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NhanVien nhanVien;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectstring);

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                nhanVien = await context.nhanViens.FindAsync(id);
                if (nhanVien == null)
                {
                    return NotFound();
                }
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Ten,Tuoi,Role,Nam,Email,Luong,TrangThai,ChucVu")] NhanVien nhanVien)
        {
            if (id != nhanVien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(_connectstring);

                try
                {
                    using (var context = new AppDbContext(optionsBuilder.Options))
                    {
                        // Kiểm tra số năm làm việc và cập nhật lương nếu cần
                        if (nhanVien.Nam > 5)
                        {
                            nhanVien.Luong = (int)(nhanVien.Luong * 1.1); // Tăng lương 10%
                        }

                        context.Update(nhanVien);
                        await context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NhanVien nhanVien;
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectstring);

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                nhanVien = await context.nhanViens.FindAsync(id);
                if (nhanVien == null)
                {
                    return NotFound();
                }
            }

            

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectstring);

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                var nhanVien = await context.nhanViens.FindAsync(id);
                if (nhanVien != null)
                {
                    context.nhanViens.Remove(nhanVien);
                    await context.SaveChangesAsync();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(Guid id)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(_connectstring);

            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                return context.nhanViens.Any(e => e.Id == id);
            }
        }

        private string GetRoleDescription(int role)
        {
            return role == 1 ? "Admin" : role == 2 ? "Nhân viên" : "Không xác định";
        }
       
    }
}
