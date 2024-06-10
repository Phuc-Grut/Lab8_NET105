using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Phan_a_bContrller : ControllerBase
    {
        [HttpGet("tinh-lai-kep")]
        public string TinhTien(double money, int n, double p) //money = tien, n = so thang, p = ty le
        {
            double tienlai = money * Math.Pow((1 + p / 100), n) - money;
            return "Số tiền lãi của bạn là: " + tienlai;
        }

        [HttpPost("tim-so-lon-thu-3")]
        public IActionResult Timsolonthu3([FromBody] int[] numbers)
        {
            if (numbers == null || numbers.Length < 3)
            {
                return BadRequest("Mảng phải chứa ít nhất 3 phần tử.");
            }

            var x = numbers.Distinct().ToArray();
            if (x.Length < 3)
            {
                return BadRequest("Mảng phải chứa ít nhất 3 phần tử khác nhau.");
            }

            Array.Sort(x);
            int thirdLargest = x[^3];

            return Ok(thirdLargest);
        }
    }
}
