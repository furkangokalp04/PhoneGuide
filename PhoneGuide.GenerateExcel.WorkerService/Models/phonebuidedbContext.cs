using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PhoneGuide.GenerateExcel.WorkerService.Models
{
    public partial class PhoneGuidedbContext : DbContext
    {
        public PhoneGuidedbContext()
        {
        }

        public PhoneGuidedbContext(DbContextOptions<PhoneGuidedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactInfo> ContactInfos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<ContactInfo>(entity =>
            {
                entity.HasIndex(e => e.ContactId, "IX_ContactInfos_ContactId");

                entity.Property(e => e.EmailAddress).HasColumnName("EMailAddress");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactInfos)
                    .HasForeignKey(d => d.ContactId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
