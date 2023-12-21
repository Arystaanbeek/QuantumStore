using QuantumStore.Models;

namespace QuantumStore.Models.ViewModels;

public class AboutViewModel
{
    public Product Product { get; set; }
    public List<User> User { get; set; }
    public List<Comment> Comment { get; set; }
}