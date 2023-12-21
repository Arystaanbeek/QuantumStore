using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuantumStore.Models;
using QuantumStore.Models.ViewModels;

// Определение пространства имен для контроллера
namespace QuantumStore.Controllers;

// Класс контроллера учетных записей
public class AccountController : Controller
{
    // Объявление контекста базы данных
    private readonly StoreContext _db;

    // Конструктор контроллера, принимающий контекст базы данных в качестве параметра
    public AccountController(StoreContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // Метод для обработки данных регистрации пользователя
    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = (await _db.Users.FirstOrDefaultAsync(u => u.Email.Equals(model.Email)))!;
            if (user is null)
            {
                await _db.Users.AddAsync(new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                });
                await _db.SaveChangesAsync();
                await Autenticate(model.Email);
                
                return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError(nameof(model.Email), errorMessage: "The mail is occupied by another user.");
        }
        // Возвращение модели в случае ошибки
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    // Метод для обработки данных входа пользователя
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        
        if (ModelState.IsValid)
        {
            
            User? user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Email.Equals(model.Email) &&
                u.Password.Equals(model.Password));
            
            if (user is not null)
            {
                await Autenticate(model.Email);
                
                return RedirectToAction("Index", "Product");
            }
        }
        
        ModelState.AddModelError("Incorrect", "Invalid username and/or password.");
        
        return View(model);
    }

    // Метод для выхода из системы
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        
        await HttpContext.SignOutAsync();
        
        return RedirectToAction("Index", "Product");
    }

    // Метод для аутентификации пользователя
    [NonAction]
    private async Task Autenticate(string email)
    {
        // Создание списка утверждений
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, email)
        };
        ClaimsIdentity id = new ClaimsIdentity(
            claims,
            "ApplicationUserCookie");
        // Вход в систему
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(id), new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            });
    }
}
