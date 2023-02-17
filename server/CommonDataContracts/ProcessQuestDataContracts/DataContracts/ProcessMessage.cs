using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.DataContracts
{
    public class ProcessMessage
    {
        public MessageType Type { get; set; }

        public string Value { get; set; }
    }
}
