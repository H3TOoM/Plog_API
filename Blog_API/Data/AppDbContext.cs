using Blog_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options ) { }

        public DbSet<Plog> Plogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<Comment>()
                .HasOne( c => c.Plog )
                .WithMany( p => p.Comments )
                .HasForeignKey( c => c.PlogId )
                .OnDelete( DeleteBehavior.Cascade );

            modelBuilder.Entity<Comment>()
                .HasOne( c => c.User )
                .WithMany( u => u.Comments )
                .HasForeignKey( c => c.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Like>()
                .HasOne( l => l.Plog )
                .WithMany( p => p.Likes )
                .HasForeignKey( l => l.PlogId )
                .OnDelete( DeleteBehavior.Cascade ); 

            modelBuilder.Entity<Like>()
                .HasOne( l => l.User )
                .WithMany( u => u.Likes )
                .HasForeignKey( l => l.UserId )
                .OnDelete( DeleteBehavior.Restrict );

        }


    }
}
