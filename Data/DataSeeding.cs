using ComputerECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ComputerECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerECommerce.Data
{
    public class DataSeeder
    {
        private readonly DataContext context;
        public DataSeeder(DataContext context)
        {
            this.context = context;
        }
        private Category laptop = new Category { Id = "Laptop", Name = "Laptop", Description = "Laptop computers", ParentCategoryId = "" };
        private Category desktop = new Category { Id = "Desktop", Name = "Desktop", Description = "Desktop computers", ParentCategoryId = "" };
        private Product macbook = new Product { Id = Guid.NewGuid().ToString(), Name = "Macbook Pro 13", Description = "Apple Macbook Pro", Price = 2000, Quantity = 10, Image = "image.png", CategoryId = "Laptop" };
        public void Seed()
        {
            context.Categories.Add(laptop);
            context.Categories.Add(desktop);
            context.Products.Add(macbook);
        }
    }
}