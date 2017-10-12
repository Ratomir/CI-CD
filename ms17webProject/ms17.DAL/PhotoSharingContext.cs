using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ms17.DAL
{
    public class PhotoSharingContext : IdentityDbContext<ApplicationUser>, IPhotoSharingContext
    {
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        public PhotoSharingContext() : base("msConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }

        public static PhotoSharingContext Create()
        {
            return new PhotoSharingContext();
        }

        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get { return Photos; }
        }

        IQueryable<Comment> IPhotoSharingContext.Comments
        {
            get { return Comments; }
        }

        int IPhotoSharingContext.SaveChanges()
        {
            return SaveChanges();
        }

        T IPhotoSharingContext.Add<T>(T entity)
        {
            return Set<T>().Add(entity);
        }

        Photo IPhotoSharingContext.FindPhotoById(int ID)
        {
            return Set<Photo>().Find(ID);
        }

        Photo IPhotoSharingContext.FindPhotoByTitle(string Title)
        {
            Photo photo = (from p in Set<Photo>()
                           where p.Title == Title
                           select p).FirstOrDefault();
            return photo;
        }

        Comment IPhotoSharingContext.FindCommentById(int ID)
        {
            return Set<Comment>().Find(ID);
        }


        T IPhotoSharingContext.Delete<T>(T entity)
        {
            return Set<T>().Remove(entity);
        }
    }
}