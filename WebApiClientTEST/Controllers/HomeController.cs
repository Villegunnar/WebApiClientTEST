using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WebApiClientTEST.Models;

namespace WebApiClientTEST.Controllers
{
    public class HomeController : Controller
    {
       
        public HomeController()
        {
          
        }

        public IActionResult Index()
        {

            IEnumerable<MovieViewModel> trendingMovies = null;

            using(var client = new HttpClient())
            {
               

                client.BaseAddress = new Uri("https://api.themoviedb.org");
                var responseTask = client.GetAsync("/3/trending/all/day?api_key=980643b7daa7582814d69d137ed8bd8c");
                responseTask.Wait();

                var result = responseTask.Result;


                if (result.IsSuccessStatusCode)
                {





                    var readJob = result.Content.ReadAsAsync<IList<MovieViewModel>>();
                    readJob.Wait();
                    trendingMovies = readJob.Result;
                }
                else
                {
                    trendingMovies = Enumerable.Empty<MovieViewModel>();
                    ModelState.AddModelError(string.Empty, "Error occured!");


                }
            }



            return View(trendingMovies);
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