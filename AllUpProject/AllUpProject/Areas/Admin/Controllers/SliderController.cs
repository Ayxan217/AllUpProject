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

        public async Task<IActionResult> Update (int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s=>s.Id==id);
            if (slider is null) return NotFound();

            UpdateSliderVM sliderVM = new()
            {
                Title = slider.Title,
                Subtitle = slider.Subtitle,
                Description = slider.Description,
                Image = slider.Image
            };

            return View(sliderVM);
        }


        [HttpPost]

        public async Task<IActionResult> Update(int? id , UpdateSliderVM slideVM)
        {

            if (!ModelState.IsValid) return View(slideVM);

            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return BadRequest();
            if (slideVM.Photo != null)
            {
                if (!slideVM.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError(nameof(UpdateSliderVM.Photo), "type is incorrect");
                    return View(slideVM);
                }
                if (!slideVM.Photo.ValidateSize(FileSize.Mb, 2))
                {
                    ModelState.AddModelError(nameof(UpdateSliderVM.Photo), "size is incorrect");
                    return View(slideVM);
                }

                string fileName = await slideVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");

                slider.Image.DeleteImage(_env.WebRootPath, "assets", "images");
                slideVM.Image = fileName;
            }

            slider.Title = slideVM.Title;
            slider.Subtitle = slideVM.Subtitle;
            slider.Description = slideVM.Description;
            slider.Image = slideVM.Image;
            slider.Order = slideVM.Order;
            slider.IsDeleted = false;
            slider.CreatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id<1) return BadRequest();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();
            slider.Image.DeleteImage(_env.WebRootPath,"assets","images");

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
