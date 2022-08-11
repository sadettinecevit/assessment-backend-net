using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.Common
{
    public interface IBaseEntity
    {
        public int UUID { get; set; }
    }
}
