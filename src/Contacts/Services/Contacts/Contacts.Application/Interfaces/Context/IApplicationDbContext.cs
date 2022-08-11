using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<ContactInfo> ContactInfos { get; set; }
        DbSet<InfoType> InfoTypes { get; set; }
    }
}
