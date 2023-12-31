﻿using QuantumStore.Models;

namespace QuantumStore.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImgSrc { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}