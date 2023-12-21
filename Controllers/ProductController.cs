using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantumStore.Models;
using QuantumStore.Models.ViewModels;

// Определение пространства имен для контроллера
namespace QuantumStore.Controllers;

// Класс контроллера продуктов
public class ProductController : Controller
{
    // Объявление контекста базы данных
    private readonly StoreContext _db;

    // Конструктор контроллера, принимающий контекст базы данных в качестве параметра
    public ProductController(StoreContext db)
    {
        _db = db;
    }

    // Метод для отображения страницы продуктов
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index(int? orderBy, int currentPage = 1)
    {
        var viewModel = new IndexViewModel();
        viewModel.Product = _db.Products.ToList();
        viewModel.User = _db.Users.ToList();
        int pageSize = 3;
        int count = _db.Products.ToList().Count;
        viewModel.Product = _db.Products.ToList();
        viewModel.Pagination = new PaginationViewModel(count, currentPage, pageSize);
        return View(viewModel);
    }

    // Метод для отображения страницы создания продукта
    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        var products = new Product();
        var viewModel = new CreateViewModel()
        {
            Product = products,
        };
        return View(viewModel);
    }

    // Метод для обработки данных создания продукта
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Product product)
    {
        product.User = _db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name);
        product.UserId = product.User!.Id;
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // Метод для отображения страницы продукта
    [HttpGet]
    [AllowAnonymous]
    public IActionResult About(int id)
    {
        var viewModel = new AboutViewModel
        {
            Product = _db.Products.FirstOrDefault(product => product.Id == id)!,
            User = _db.Users.ToList(),
            Comment = _db.Comments.Where(comment => comment.Product == _db.Products.FirstOrDefault(product => product.Id == id)).ToList()
        };
        return View(viewModel);
    }

    // Метод для удаления продукта
    [HttpPost]
    [Authorize]
    public IActionResult Delete(int id)
    {
        Product phone = _db.Products.FirstOrDefault(p => p.Id == id)!;
        _db.Products.Remove(phone);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    // Метод для отображения страницы редактирования продукта
    [HttpGet]
    [Authorize]
    public IActionResult Edit(int id)
    {
        var viewModel = new CreateViewModel
        {
            Product = _db.Products.FirstOrDefault(product => product.Id == id)!,
        };
        return View(viewModel);
    }

    // Метод для обработки данных редактирования продукта
    [HttpPost]
    [Authorize]
    public IActionResult Edit(Product product)
    {
        _db.Products.Update(product);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}