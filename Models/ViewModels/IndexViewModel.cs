using QuantumStore.Models;

namespace QuantumStore.Models.ViewModels;

public class IndexViewModel
{
    public List<Product> Product { get; set; }
    public List<User> User { get; set; }
    public PaginationViewModel Pagination { get; set; }
}