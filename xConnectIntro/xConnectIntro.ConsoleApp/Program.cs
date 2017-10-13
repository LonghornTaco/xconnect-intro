using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.Schema;

namespace xConnectIntro.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddContact();
            //SearchContacts();
            GetContact();

            Console.ReadLine();
        }

        private static async void SearchContacts()
        {
            using (var client = GetClient())
            {
                var queryable = client.Contacts
                    .Where(c => c.Interactions.Any(x => x.StartDateTime > DateTime.UtcNow.AddDays(-30)))
                    .WithExpandOptions(new ContactExpandOptions( "Personal" ));

                var results = await queryable.ToSearchResults();
                var contacts = await results.Results.Select(x => x.Item).ToList();
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"{contact.Personal().FirstName} {contact.Personal().LastName}");
                }
            }
        }

        private static void GetContact()
        {
            using (var client = GetClient())
            {
                var contactReference = new IdentifiedContactReference("twitter", "longhorntaco");
                var contact = client.Get(contactReference, new ExpandOptions() { FacetKeys = { "Personal" } });

                if (contact != null)
                {
                    Console.WriteLine($"{contact.Personal().FirstName} {contact.Personal().LastName}");
                }
            }
        }

        private static void AddContact()
        {
            using (var client = GetClient())
            {
                var identifiers = new ContactIdentifier[]
                {
                    new ContactIdentifier("twitter", "longhorntaco", ContactIdentifierType.Known),
                    new ContactIdentifier("domain", "longhorn.taco", ContactIdentifierType.Known)
                };
                var contact = new Contact(identifiers);

                var personalInfoFacet = new PersonalInformation
                {
                    FirstName = "Longhorn",
                    LastName = "Taco"
                };
                client.SetFacet<PersonalInformation>(contact, PersonalInformation.DefaultFacetKey, personalInfoFacet);

                var emailFacet = new EmailAddressList(new EmailAddress("longhorn@taco.com", true), "twitter");
                client.SetFacet<EmailAddressList>(contact, EmailAddressList.DefaultFacetKey, emailFacet);

                client.AddContact(contact);
                client.Submit();
            }
        }

        private static XConnectClient GetClient()
        {
            var config = new XConnectClientConfiguration(
                new XdbRuntimeModel(CollectionModel.Model),
                new Uri("https://sc9_xconnect"),
                new Uri("https://sc9_xconnect"));

            try
            {
                config.Initialize();
            }
            catch (XdbModelConflictException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return new XConnectClient(config);
        }
    }
}
