using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.XConnect;

namespace xConnectIntro.Model
{
    public class LeadCaptured : Event
    {
        public string ContactInfo { get; set; }

        public LeadCaptured(Guid definitionId, DateTime timestamp) : base(definitionId, timestamp)
        {
            
        }
    }
}
