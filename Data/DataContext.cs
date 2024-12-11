using Microsoft.EntityFrameworkCore;

namespace ComputerECommerce.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.OrderItem> OrderItems { get; set; }
        public DbSet<Models.ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Models.ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.UserCredentials> UserCredentials { get; set; }
        public DbSet<Models.Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Models.UserCredentials>()
                .HasKey(uc => uc.hashedEmail);

            // Configure your entity relationships and constraints here
        }
    }
}