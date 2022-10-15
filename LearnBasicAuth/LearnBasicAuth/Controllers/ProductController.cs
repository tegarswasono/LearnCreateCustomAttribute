using LearnBasicAuth.Auth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnBasicAuth.Controllers
{
    //ini kita buat attribute sendiri dan kita cek apakah ada passing header authorization gak di sana
    //sebenarnya ada cara yang lebih bagus, jadi kita pakai attribute bawaan aspnet core yaitu [Authorize]
    //cara setup nya gimana, baca sendiri ya di internet, yg jelas biasa nya kamu di minta extends ke class / interface tertentu 
    //lalu daftarkan class yang baru kamu buat ke startup.cs, app.UseAuthentication(); => code ini jelas harus di tambahkan 

    [ApiController]
    [Route("api/[controller]")]
    [BasicAuthMyBro]
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult<List<ProductModel>> Index()
        {
            var tmp = new List<ProductModel>()
            {
                new ProductModel(){ Name = "Baju Anak", Stok = 5, Price=50000 },
                new ProductModel(){ Name = "Baju Pria", Stok = 10, Price=20000 },
                new ProductModel(){ Name = "Baju Wanita", Stok = 6, Price=80000 },
                new ProductModel(){ Name = "Baju Sekolah", Stok = 20, Price=40000 }
            };
            return Ok(tmp);
        }
    }
    public class ProductModel
    {
        public string Name { set; get; }
        public long Stok { set; get; }
        public double Price { set; get; }
    }
}
