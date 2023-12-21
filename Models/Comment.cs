using QuantumStore.Models;

namespace QuantumStore.Models;

public class Comment
{
    public int Id { get; set; }
    public string CommentText { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}