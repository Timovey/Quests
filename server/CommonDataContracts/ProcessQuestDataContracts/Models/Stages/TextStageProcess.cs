using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Models.Stages
{
    public class TextStageProcess : StageProcess
    {
        public override StageProcessType Type { get; } = StageProcessType.Text;

        public string Text { get; set; }
    }
}
