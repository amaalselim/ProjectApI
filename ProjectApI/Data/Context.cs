using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectApI.Models;

namespace ProjectApI.Data
{
    public class Context : IdentityDbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products  { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<WishList> wishLists { get; set; }
        public DbSet<WishListItem> wishListItems { get; set; }   




        public Context()
        {
             
        }
        public Context(DbContextOptions<Context> options) : base(options) 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=WebApi;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        }
    }
}
