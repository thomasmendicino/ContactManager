using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactList
    {
        private IContactCreator _contactCreator;
        public ContactList(IContactCreator contactCreator)
        {

        }

    }
}
