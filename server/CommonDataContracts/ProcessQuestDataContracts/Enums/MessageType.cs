using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Enums
{
    public enum MessageType : byte
    {
        Unknown = 0,
        Chat = 1,
        Process = 2,
    }
}
