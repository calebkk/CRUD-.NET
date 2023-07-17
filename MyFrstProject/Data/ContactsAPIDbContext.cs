using Microsoft.EntityFrameworkCore;

public class ContactsAPIDbContext : DbContext
{
    public ContactsAPIDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Contact> Contacts{get;set;}
}
namespace MyFrstProject.Models
{
    public class UpdateContactRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public long phone { get; set; }
        public string address { get; set; }
    }
}