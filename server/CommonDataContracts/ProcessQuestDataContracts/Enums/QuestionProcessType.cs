using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Enums
{
    public enum QuestionProcessType : byte
    {
        Unknown = 0,
        Insert = 1,
        Single = 2,
        Multiple = 3,
        Order = 4,
    }
}
