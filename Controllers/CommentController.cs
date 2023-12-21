using Microsoft.AspNetCore.Mvc;
using QuantumStore.Models;

// Определение пространства имен для контроллера
namespace QuantumStore.Controllers;

public class CommentController : Controller
{
    // Объявление контекста базы данных
    private readonly StoreContext _db;

    public CommentController(StoreContext db)
    {
        _db = db;
    }

    // Метод для отправки комментария
    [HttpPost]
    public IActionResult SendComment(int productId, string commentContext)
    {
        var comment = new Comment();
        comment.CommentText = commentContext;
        comment.Product = _db.Products.FirstOrDefault(u => u.Id == productId)!;
        comment.User = _db.Users.FirstOrDefault(u => u.Email == User.Identity!.Name)!;
        comment.ProductId = comment.Product.Id;
        comment.UserId = comment.User.Id;
        _db.Comments.Add(comment);
        _db.SaveChanges();

        return Redirect("/Product/About/" + productId);
    }

    // Метод для удаления комментария
    [HttpPost]
    public IActionResult DeleteComment(int id)
    {
        Comment comment = _db.Comments.FirstOrDefault(c => c.Id == id)!;
        _db.Remove(comment);
        _db.SaveChanges();
        return Redirect("/Product/About/" + id);
    }
}