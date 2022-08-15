using Microsoft.EntityFrameworkCore;
using Contacts.Application.Interfaces.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Domain.Entities;

namespace Contacts.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<InfoType> InfoTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InfoType>().HasData(new InfoType[] {
                new InfoType() { UUID = 1, Name = "Telefon Numarası" },
                new InfoType() { UUID = 2, Name = "E-mail Adresi" },
                new InfoType() { UUID = 3, Name = "Konum" } });

            modelBuilder.Entity<Contact>().HasData(new Contact[] {
                new Contact() { UUID = 1, Name = "Sadettin", Lastname = "Ecevit", Company = "Test" },
                new Contact() { UUID = 2, Name = "Sezen", Lastname = "Aksu", Company = "Test" },
                new Contact() { UUID = 3, Name = "Müzeyyen", Lastname = "Senar", Company = "Test" } });

            modelBuilder.Entity<ContactInfo>().HasData(new ContactInfo[] {
                new ContactInfo() { UUID = 1, ContactId = 1, InfoTypeId = 1, Info = "05xx xxx xx xx" },
                new ContactInfo() { UUID = 2, ContactId = 1, InfoTypeId = 3, Info = "Amsterdam" },
                new ContactInfo() { UUID = 3, ContactId = 2, InfoTypeId = 1, Info = "05xx xxx xx xx" },
                new ContactInfo() { UUID = 4, ContactId = 3, InfoTypeId = 3, Info = "Amsterdam" },
                new ContactInfo() { UUID = 5, ContactId = 3, InfoTypeId = 3, Info = "05xx xxx xx xx" } });

            //modelBuilder.Entity<ContactInfo>().HasOne(p=>p.Contact).WithMany().IsRequired();
            //modelBuilder.Entity<ContactInfo>().HasOne(p=>p.InfoType).WithMany().IsRequired();
        }
    }
}
