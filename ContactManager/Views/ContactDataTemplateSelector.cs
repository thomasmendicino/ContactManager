using ContactManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ContactManager.Views
{
    public class ContactDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null)// && item is Task)
            {
                ContactViewModel contactViewModel = (ContactViewModel)item;

                if (contactViewModel.ContactType == "ContactManager.Models.Customer")
                {
                    return element.FindResource("customerTemplate") as DataTemplate;
                }
                /*if (taskitem.Priority == 1)
                    return
                        element.FindResource("importantTaskTemplate") as DataTemplate;*/
                else
                {
                    return element.FindResource("vendorTemplate") as DataTemplate;
                }
            }

            return null;
        }
    }
}
