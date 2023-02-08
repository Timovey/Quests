using GenerateQuestsService.DataContracts.Enums;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class MapStage
    {
        public StageType Type { get; } = StageType.Map;

        public Coordinates Coords { get; set; }
    }
}
