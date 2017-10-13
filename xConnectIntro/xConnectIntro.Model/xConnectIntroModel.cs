using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.XConnect;
using Sitecore.XConnect.Client.Configuration;
using Sitecore.XConnect.Schema;

namespace xConnectIntro.Model
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class xConnectIntroModel
    {
        public static XdbModel Model { get; } = BuildModel();

        private static XdbModel BuildModel()
        {
            var builder = new XdbModelBuilder("xConnectIntroModel", new XdbModelVersion(1, 0));
            builder.ReferenceModel(Sitecore.XConnect.Collection.Model.CollectionModel.Model);
            builder.DefineFacet<Contact, SalesRegion>(SalesRegion.FacetName);
            builder.DefineEventType<LeadCaptured>(true);

            return builder.BuildModel();
        }
    }
}
