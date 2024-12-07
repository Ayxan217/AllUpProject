using AllUpProject.Areas.Admin.ViewModels;
using AllUpProject.DAL;
using AllUpProject.Models;
using AllUpProject.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllUpProject.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class SliderController : Controller
    {
        private AppDbContext _context;
        private IWebHostEnvironment _env;


        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);

        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(SliderVM sliderVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Slider newSlider = new Slider() { 
           
                Title = sliderVM.Title,
                Subtitle = sliderVM.Subtitle,
                Description = sliderVM.Description ,
                Image = await sliderVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images"),
                Order = sliderVM.Order

                
            };
           
            await _context.Sliders.AddAsync(newSlider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
