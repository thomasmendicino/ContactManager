using CommunityToolkit.Mvvm.Input;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ContactManager
{
    public partial class ContactManagerViewModel:ObservableCollection<Contact>
    {
        /* public ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();

        public ContactManagerViewModel()
        {
            AddContact = new RelayCommand(OnClick);
            Save = new RelayCommand<object?>(OnSave);
            FileAccess repo = new FileAccess();
           
            foreach (var ct in repo.GetContacts())
            {
                Add(ct);
            }
        }
        public ICommand AddContact { get; }
        public ICommand Save { get; }
        public void OnClick()
        {
            var cust = new Customer { Name = "test", Address = "test" };
            Add(cust);
            //OnCollectionChanged(cust);
        }
        public void OnSave(object? state)
        {
            var textbox = state as TextBox;
            ////Add(new Customer { Name = textbox ?? textbox.Text, Address = "test2", Phone = "test2" });
        } */
    }
}
