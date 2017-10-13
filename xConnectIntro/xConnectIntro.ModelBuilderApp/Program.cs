using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.XConnect;
using Sitecore.XConnect.Schema;
using xConnectIntro.Model;

namespace xConnectIntro.ModelBuilderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = Sitecore.XConnect.Serialization.XdbModelWriter.Serialize(xConnectIntroModel.Model);
            File.WriteAllText(xConnectIntroModel.Model.FullName + ".json", model);
        }
    }
}
