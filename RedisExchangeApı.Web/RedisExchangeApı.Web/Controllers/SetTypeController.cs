using Microsoft.AspNetCore.Mvc;
using RedisExchangeApı.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeApı.Web.Controllers
{
	public class SetTypeController : Controller
	{
		RedisService _redisService;
		IDatabase db;
		private string listKey = "hashname";
		public SetTypeController(RedisService redisService)
		{
			_redisService = redisService;
			db = _redisService.getDb(0);

		}
		public IActionResult Index()
		{
			HashSet<string> nameList = new HashSet<string>();
			if (db.KeyExists(listKey))
			{
				db.SetMembers(listKey).ToList().ForEach(x => nameList.Add(x.ToString()));
			}
			return View();
		}
		[HttpPost]
		public IActionResult Add(string name)
		{

			db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
			db.SetAdd(listKey, name);
			return RedirectToAction("Index");
		}	   
		public async Task< IActionResult> DeleteItem(string name)
		{

			await db.SetRemoveAsync(listKey, name);
			return RedirectToAction("Index");
		}
	}
}
