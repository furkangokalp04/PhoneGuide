using Microsoft.EntityFrameworkCore;

namespace PhoneGuide.Contacts.Entities
{
    public class ContactContext:DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contacts");
            modelBuilder.Entity<ContactInfo>().ToTable("ContactInfos");
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}
