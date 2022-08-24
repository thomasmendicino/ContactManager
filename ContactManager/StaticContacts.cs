/* using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManager.Models;

namespace ContactManager
{
    public class StaticContacts : ObservableCollection<Contact>
    {
        FileAccess repo = new FileAccess();
        public StaticContacts()
        {
            /*Add(new Customer() { Name = "Thomas", Address = "555", Phone = "1234" });
            Add(new Customer() { Name = "Jonas", Address = "444", Phone = "4567" });
            Add(new Customer() { Name = "Fred", Address = "333", Phone = "789" });
            
            foreach (var ct in repo.GetContacts())
            {
                Add(ct);
            }
        }
    }
}
 */