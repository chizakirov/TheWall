using Microsoft.EntityFrameworkCore;
 
namespace thewall.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> users {get;set;}
        public DbSet<Message> messages {get;set;} 
        public DbSet<Comment> comments {get; set;}
    }
}