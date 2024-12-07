
using AllUpProject.DAL;
using AllUpProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AllUpProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            var categories = _context.Categories.ToList();

            List<Slider> sliders = new List<Slider>
            {
                new Slider
                {
                    Title = "Sony Bravia",
                    Subtitle = "4K HDR Smart TV",
                    Order = 1,
                    Description = "Explore 360 Platform",
                },
                new Slider
                {
                    Title = "Fulldive VR",
                    Subtitle = "2020 Virtual Reality",
                    Order = 2,
                    Description = "Explore The World",
                }
            };

            var model = new HomeViewModel
            {
                Products = products,
                Categories = categories,
                Sliders = sliders
                
            };
            _context.SaveChanges();

            return View(model);
        }
    }
}
