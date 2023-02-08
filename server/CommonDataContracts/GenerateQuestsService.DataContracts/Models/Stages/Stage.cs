using GenerateQuestsService.DataContracts.Enums;
using System.Runtime.Serialization;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class Stage
    {
        public string Title { get; set; }

        public StageType Type { get; }
    }
}
