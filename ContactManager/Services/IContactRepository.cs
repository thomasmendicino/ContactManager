using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IContactRepository
    {
        public Task<IEnumerable<Contact>> GetAllContacts();
        
    }
}
