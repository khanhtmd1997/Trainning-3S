using Microsoft.AspNetCore.Mvc;

namespace TaskTranning.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Error404</returns>
        [Route("Error/404")]
        public IActionResult Error404()
        {
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Error401</returns>
        [Route("Error/401")]
        public IActionResult Error401(string requestPath)
        {
            ViewBag.RequestPath = requestPath ?? "/";
            return View();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Error400</returns>
        [Route("Error/400")]
        public IActionResult Error400()
        {
            return View();
        }
    }
}