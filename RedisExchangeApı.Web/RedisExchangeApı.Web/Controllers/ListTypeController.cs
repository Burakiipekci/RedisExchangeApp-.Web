using Microsoft.AspNetCore.Mvc;
using RedisExchangeApı.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeApı.Web.Controllers
{
    public class ListTypeController : Controller
    {
        RedisService _redisService;
        IDatabase db;
        private string listKey = "names";
        public ListTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.getDb(0);

        }
        public IActionResult Index()
        {                 
            List<string> list = new List<string>();
            if (db.KeyExists(listKey))
            {
                db.ListRange(listKey).ToList().ForEach(x=>
                {
                    list.Add(x.ToString());
                });

            }



            return View(list);

        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            db.ListRightPush(listKey, name);

            return RedirectToAction("Index");
        }  

        public IActionResult DeleteItem(string name)
        {
            db.ListRemoveAsync(listKey, name).Wait();

            return RedirectToAction("Index");
        }        
        public IActionResult DeleteFirstItem(string name)
        {
            db.ListLeftPop(listKey);

            return RedirectToAction("Index");
        }
    }
}
