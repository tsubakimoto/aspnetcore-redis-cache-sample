using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly string _cacheKey = "MyCacheKey";

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [Route("/cget")]
        public IActionResult CacheGet()
        {
            return Content(_cache.GetString(_cacheKey));
        }

        [Route("/cset")]
        public IActionResult CacheSet()
        {
            _cache.SetString(_cacheKey, DateTime.Now.ToLongTimeString());
            return Content(_cache.GetString(_cacheKey));
        }

        [Route("/cremove")]
        public IActionResult CacheRemove()
        {
            _cache.Remove(_cacheKey);
            return Content(_cache.GetString(_cacheKey));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
