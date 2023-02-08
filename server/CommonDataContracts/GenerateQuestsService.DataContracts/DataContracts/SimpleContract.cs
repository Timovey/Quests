

using GenerateQuestsService.DataContracts.DataContracts.Stages;
using System.Text.Json.Serialization;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class SimpleContract
    {
        public int Id { get; set; }

        public BaseStage d { get; set; }
    }
}
