using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Enums
{
    public enum StageProcessType : byte
    {
        Unknow = 0,
        Video = 1,
        QrCode = 2,
        Text = 3,
        Map = 4,
        Test = 5,
    }
}
