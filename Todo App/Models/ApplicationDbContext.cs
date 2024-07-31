using Microsoft.EntityFrameworkCore;

namespace Todo_App.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option)
            :base(option)
        {
            
        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .Property(u => u.Id)
               .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<User>()
                .Property(u => u.CreateDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
              .Property(u => u.UpdateDate)
              .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<User>()
               .HasMany(u => u.TodoList)
               .WithOne(t => t.User)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Todo>()
                .Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Todo>()
               .Property(t => t.UpdatedDate)
               .HasDefaultValueSql("GETDATE()");
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Todo> Todos { get; set; }
    }
}
