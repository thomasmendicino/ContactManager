 using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class FileAccess
    {/*
        public string filePath = @"C:\Temp\contacts.txt";
        public FileAccess() {
            

        }
        public List<Customer> GetContacts()
        {
            List<Customer> customers = new List<Customer>();

            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string[] lines = System.IO.File.ReadAllLines(filePath);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of contacts.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                if (!string.IsNullOrEmpty(line))
                {
                    string[] props = line.Split('\t');
                    customers.Add(new Customer()
                    {
                        Name = props[0],
                        Address = props[1],
                        Phone = props[2]
                    });
                }                
            }

            return customers;            
        }

        public async Task<bool> AddContact(Contact contact)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Concat(contact.Name, '\t', contact.Address, '\t', contact.Phone));

            using StreamWriter file = new(filePath, append: true);

            try { await file.WriteLineAsync(sb.ToString()); }
            catch (Exception ex) { Console.WriteLine(ex.Message); return false; }

            return true;
        }
    */}
}