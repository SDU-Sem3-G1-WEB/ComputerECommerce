using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using ComputerECommerce.Models;

namespace ComputerECommerce.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                context.Products.RemoveRange(context.Products);
                context.Categories.RemoveRange(context.Categories);
                context.SaveChanges();

                context.Categories.AddRange(
                    new Category
                    {
                        Id = "1",
                        Name = "Laptops",
                        Description = "All kinds of laptops",
                        ParentCategoryId = null
                    },
                    new Category
                    {
                        Id = "2",
                        Name = "Desktops",
                        Description = "All kinds of desktops",
                        ParentCategoryId = null
                    },
                    new Category
                    {
                        Id = "3",
                        Name = "Accessories",
                        Description = "Computer accessories",
                        ParentCategoryId = null
                    }
                );
                context.Products.AddRange(
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Apple MacBook Air",
                        Description = "A high-performance laptop with Apple M2 chip.",
                        Price = 1199.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 10,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Dell XPS 13",
                        Description = "A powerful and compact laptop with Intel i7 processor.",
                        Price = 999.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 15,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "HP Spectre x360",
                        Description = "A versatile 2-in-1 laptop with Intel i7 processor.",
                        Price = 1099.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 12,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Lenovo ThinkPad X1 Carbon",
                        Description = "A durable and high-performance laptop with Intel i7 processor.",
                        Price = 1299.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 8,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Microsoft Surface Laptop 4",
                        Description = "A sleek and powerful laptop with AMD Ryzen processor.",
                        Price = 1149.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 10,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Asus ZenBook 14",
                        Description = "A lightweight and stylish laptop with Intel i5 processor.",
                        Price = 899.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 20,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Acer Swift 3",
                        Description = "A budget-friendly laptop with AMD Ryzen processor.",
                        Price = 699.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 25,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Razer Blade 15",
                        Description = "A high-performance gaming laptop with NVIDIA GeForce RTX.",
                        Price = 1599.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 5,
                        CategoryId = "1"
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Samsung Galaxy Book Pro",
                        Description = "A sleek and lightweight laptop with Intel i7 processor.",
                        Price = 1199.00M,
                        Image = "/images/products/laptop.jpg",
                        Quantity = 10,
                        CategoryId = "1"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}