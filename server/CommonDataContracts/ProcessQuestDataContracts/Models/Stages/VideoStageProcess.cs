using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Models.Stages
{
    public class VideoStageProcess : StageProcess
    {
        public override StageProcessType Type { get; } = StageProcessType.Video;

        public string Url { get; set; }

    }
}
