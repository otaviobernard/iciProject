using Microsoft.EntityFrameworkCore;
using NoticiasProvaIci.Models;

namespace NoticiasProvaIci.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<NewsTag> NewsTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(250).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(250).IsRequired();
                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(n => n.User)
                      .WithMany(u => u.News)
                      .HasForeignKey(n => n.UserId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<NewsTag>(entity =>
            {
                entity.HasKey(nt => nt.Id);

                entity.HasOne(nt => nt.News)
                      .WithMany(n => n.NewsTag)
                      .HasForeignKey(nt => nt.NewsId);

                entity.HasOne(nt => nt.Tag)
                      .WithMany(t => t.NewsTag)
                      .HasForeignKey(nt => nt.TagId);
            });
        }
    }
}