using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using VuQuangHuyMVC.Models;

namespace VuQuangHuyMVC.Controllers
{
	public class CategoryController : Controller
	{
		private readonly CategoryService categoryService;

		public CategoryController()
		{
			categoryService = new CategoryService();	
		}

		// GET: CategoryController
		public async Task<IActionResult> Index()
		{
			var categorys = await categoryService.GetAll();
			return View(categorys);
		}

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryService.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            // Trả về tên danh mục cha (nếu có)
            ViewBag.ParentCategoryName = category.ParentCategory != null ? category.ParentCategory.CategoryName : "None";

            return View(category);
        }


        // GET: CategoryController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ParentCategoryId = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Category category)
		{
			Console.WriteLine($"CategoryName: {category.CategoryName}");
			Console.WriteLine($"ParentCategoryId: {category.ParentCategoryId}");
			Console.WriteLine($"des: {category.CategoryDesciption}");
			Console.WriteLine($"status: {category.IsActive}");

			if (ModelState.IsValid)
			{
				await categoryService.AddCategory(category);
				return RedirectToAction(nameof(Index));
			}

			ViewBag.ParentCategoryId = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName", category.ParentCategoryId);
			return View(category);
		}

        // GET: CategoryController/Edit/5
        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound(); // Kiểm tra nếu id bị null
            }

            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(); // Kiểm tra nếu category không tồn tại
            }

            ViewBag.ParentCategoryId = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName", category.ParentCategoryId);

            return View(category); // Đảm bảo trả về model hợp lệ
        }


        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId) // Kiểm tra xem id có trùng với category không
            {
                return BadRequest(); // Trả về lỗi nếu id không khớp
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await categoryService.UpdateCategory(category); // Cập nhật danh mục
                    return RedirectToAction(nameof(Index)); // Chuyển hướng về danh sách
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating category: " + ex.Message);
                }
            }

            // Nếu có lỗi, load lại danh sách danh mục cha
            ViewBag.ParentCategoryId = new SelectList(await categoryService.GetAll(), "CategoryId", "CategoryName", category.ParentCategoryId);

            return View(category); // Trả về view với dữ liệu đã nhập
        }


        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            await categoryService.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
