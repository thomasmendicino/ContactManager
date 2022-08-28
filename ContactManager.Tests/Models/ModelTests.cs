using FluentAssertions;

using ContactManager.Models;
using ContactManager.Services;
using ContactManager.Tests.Stubs;

namespace ContactManager.Tests.Models
{
    public class ModelTests
    {
        StubContactCreator _stubContactCreator = new StubContactCreator();
        StubContactRepo _stubContactRepo = new StubContactRepo();
        StubVendorCodeValidator _stubVendorCodeValidator = new StubVendorCodeValidator();
        [Fact]
        public void NewCustomers_Should_SetBaseProperties()
        {
            Customer customer = new Customer();

            string notes = "Test notes";
            string phone = "test phone";
            string address = "test address";
            string name = "test name";
            string company = "test company";

            customer.Notes = notes;
            customer.Phone = phone;
            customer.Address = address;
            customer.Name = name;
            customer.Company = company;

            customer.Notes.Should().Be(notes);
            customer.Phone.Should().Be(phone);
            customer.Address.Should().Be(address);
            customer.Name.Should().Be(name);
            customer.Company.Should().Be(company);
        }
        [Fact]
        public void NewVendors_Should_SetBaseProperties()
        {
            Vendor vendor = new Vendor();

            string phone = "test phone";
            string address = "test address";
            string name = "test name";
            string company = "test company";

            vendor.Phone = phone;
            vendor.Address = address;
            vendor.Name = name;
            vendor.Company = company;

            vendor.Phone.Should().Be(phone);
            vendor.Address.Should().Be(address);
            vendor.Name.Should().Be(name);
            vendor.Company.Should().Be(company);
        }
        [Fact]
        public void ContactToString_Should_ReturnName()
        {
            string testName = "Roger";
            Customer customer = CreateCustomerWithPropertiesSet(name: testName);

            customer.ToString().Should().Be(testName);
        }
        [Fact]
        public async void ContactList_Should_AddCustomerWhenCustomerIsSupplied()
        {
            Customer customer = CreateCustomerWithPropertiesSet();
            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            await contactList.AddContact(customer);

            _stubContactCreator.vendorsAdded.Should().BeEmpty();
            _stubContactCreator.customersAdded.Should().NotBeEmpty();
            _stubContactCreator.customersAdded.ElementAt(0).Should().Be(customer);
        }
        [Fact]
        public async void ContactList_Should_AddVendorWhenVendorIsSupplied()
        {
            Vendor vendor = CreateVendorWithPropertiesSet();
            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            await contactList.AddContact(vendor);

            _stubContactCreator.customersAdded.Should().BeEmpty();
            _stubContactCreator.vendorsAdded.Should().NotBeEmpty();
            _stubContactCreator.vendorsAdded.ElementAt(0).Should().Be(vendor);
        }
        [Fact]
        public async void ContactListGetContacts_Should_ReturnVendorsWhenNoCustomers() {
            _stubContactRepo.customersToGet = null;
            _stubContactRepo.vendorsToGet = new List<Vendor> { CreateVendorWithPropertiesSet(name: "vendor A"), CreateVendorWithPropertiesSet(name: "vendor B") };

            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            IEnumerable<Contact> results = await contactList.GetAllContacts();

            results.Count().Should().Be(2);
            foreach (Contact contact in results)
            {
                contact.Should().BeOfType<Vendor>();
            }
        }
        [Fact]
        public async void ContactListGetContacts_Should_ReturnCustomersWhenNoVendors() {
            
            _stubContactRepo.customersToGet = new List<Customer> { CreateCustomerWithPropertiesSet(name: "Customer A"), CreateCustomerWithPropertiesSet(name: "Customer B") };
            _stubContactRepo.vendorsToGet = null;

            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            IEnumerable<Contact> results = await contactList.GetAllContacts();

            results.Count().Should().Be(2);
            foreach (Contact contact in results)
            {
                contact.Should().BeOfType<Customer>();
            }
        }
        [Fact]
        public async void ContactListGetContacts_Should_ReturnBothVendorsAndCustomers() {
            _stubContactRepo.customersToGet = new List<Customer> { CreateCustomerWithPropertiesSet(name: "Customer A"), CreateCustomerWithPropertiesSet(name: "Customer B") };
            _stubContactRepo.vendorsToGet = new List<Vendor> { CreateVendorWithPropertiesSet(name: "vendor A"), CreateVendorWithPropertiesSet(name: "vendor B") };

            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            IEnumerable<Contact> results = await contactList.GetAllContacts();

            results.Count().Should().Be(4);
            results.ElementAt(0).Should().BeOfType<Customer>();
            results.ElementAt(1).Should().BeOfType<Customer>();
            results.ElementAt(2).Should().BeOfType<Vendor>();
            results.ElementAt(3).Should().BeOfType<Vendor>();
        }
        [Fact]
        public async void ContactListGetVendorList_Should_ReturnVendors()
        {
            string vendorCode = "A1";
            Vendor vendor = CreateVendorWithPropertiesSet(vendorCode: vendorCode);
            _stubVendorCodeValidator.VendorExists = true;
            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            Vendor results = await contactList.GetVendorFromMasterList(vendor);

            results.Should().NotBeNull();
            results.VendorCode.Should().Be(vendorCode);
        }
        [Fact]
        public async void ContactListAddVendorMasterList_Should_AddVendor()
        {
            Vendor vendor = CreateVendorWithPropertiesSet(name: "Vendor D");

            ContactList contactList = new ContactList(_stubContactCreator, _stubContactRepo, _stubVendorCodeValidator);

            contactList.AddVendorMasterRecord(vendor);

            _stubContactCreator.vendorMasterList.Should().NotBeEmpty();
            _stubContactCreator.vendorMasterList.ElementAt(0).Should().Be(vendor);

        }

        private static Customer CreateCustomerWithPropertiesSet(string? name = null, string? address = null, string? phone = null, string? company = null, string? notes = null)
        {
            Customer customer = new Customer();

            string _notes = notes ?? "Test notes";
            string _phone = phone ?? "test phone";
            string _address = address ?? "test address";
            string _name = name ?? "test name";
            string _company = company ?? "test company";

            customer.Notes = _notes;
            customer.Phone = _phone;
            customer.Address = _address;
            customer.Name = _name;
            customer.Company = _company;

            return customer;
        }

        private static Vendor CreateVendorWithPropertiesSet(string? name = null, string? address = null, string? phone = null, string? company = null, string? vendorCode = null)
        {
            Vendor vendor = new Vendor();

            string _vendorCode = vendorCode ?? "Test notes";
            string _phone = phone ?? "test phone";
            string _address = address ?? "test address";
            string _name = name ?? "test name";
            string _company = company ?? "test company";

            vendor.VendorCode = _vendorCode;
            vendor.Phone = _phone;
            vendor.Address = _address;
            vendor.Name = _name;
            vendor.Company = _company;

            return vendor;
        }
    }
}
