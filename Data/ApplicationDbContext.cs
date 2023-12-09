using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace Forum.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Forum.Models.ForumThread> ForumThread { get; set; } = default!;
        public DbSet<Forum.Models.ThreadMessage> ThreadMessage { get; set; } = default!;
    }
}
