using GenerateQuestsService.DataContracts.Enums;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class TextStage
    {
        public StageType Type { get; } = StageType.Text;

        public string Text { get; internal set; }
    }
}
