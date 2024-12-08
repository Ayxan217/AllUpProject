
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

        public async Task<IActionResult> Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            var categories = _context.Categories.ToList();

            HomeViewModel homeVM = new()
            {
                     Sliders = await _context.Sliders.OrderBy(s=>s.Order).ToListAsync()
            };
           
         
           await _context.SaveChangesAsync();

            return View(homeVM);
        }
    }
}
