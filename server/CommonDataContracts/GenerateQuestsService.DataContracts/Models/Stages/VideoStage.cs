using GenerateQuestsService.DataContracts.Enums;
using System.Runtime.Serialization;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class VideoStage
    {
        public StageType Type { get;} = StageType.Video;

        public string Url { get; set; }

    }
}
