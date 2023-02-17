using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Models.Stages
{
    public class QrCodeStageProcess : StageProcess
    {
        public override StageProcessType Type { get; } = StageProcessType.QrCode;

        public string Code { get; set; }
    }
}
