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
                return;
            }

            using (var client = new XConnectClient(config))
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
    }
}
