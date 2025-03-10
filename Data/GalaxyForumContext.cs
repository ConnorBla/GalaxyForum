using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GalaxyForum.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace GalaxyForum.Data
{

    public class GalaxyForumContext : IdentityDbContext<ApplicationUser>
    {
        public GalaxyForumContext(DbContextOptions<GalaxyForumContext> options)
            : base(options)
        {
        }

        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Comment> Comments { get; set; }


        //following code retrieved from chatgpt after repeated issues of failed migrations.

        //following is the error that was being encountered:
        //Introducing FOREIGN KEY constraint 'FK_Comments_Discussions_DiscussionId' on table 'Comments' may cause cycles or multiple cascade paths. Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Discussion>()
                .ToTable("Discussions");
            // Configure Comment -> Discussion relationship
            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.Discussion)
            //    .WithMany(d => d.Comments)
            //    .HasForeignKey(c => c.DiscussionId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .IsRequired(false);

            //// Configure Comment -> ApplicationUser relationship
            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.ApplicationUser)
            //    .WithMany() 
            //    .HasForeignKey(c => c.ApplicationUserId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .IsRequired(false); 
        }

    }
}