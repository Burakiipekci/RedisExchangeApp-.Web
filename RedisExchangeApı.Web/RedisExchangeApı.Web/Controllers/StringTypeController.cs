using Microsoft.AspNetCore.Mvc;
using RedisExchangeApı.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeApı.Web.Controllers
{
    public class StringTypeController : Controller
    {
        RedisService _redisService;
        IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.getDb(0);
                
        }
      
        public IActionResult Index()
        {

          
            db.StringSet("name", "Burak İpekçi");
            db.StringSet("user", 100);


            return View();
        }
        public IActionResult Show()
        {
            var value = db.StringGet("name");

            db.StringIncrement("user",1);    // user keyindeki datayı birer birer arttırır
            var get = db.StringGetRange("name", 0, 3); //0. indexden başla ve 3. indexli harfe kadar getir.
          
            if (value.HasValue)
            {
                ViewBag.value = value.ToString();

            }
      


            return View();
        }
    }
}
