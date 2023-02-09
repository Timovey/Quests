using GenerateQuestsService.DataContracts.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class Stage
    {
        public string Title { get; set; }

        [JsonPropertyOrder(-5)]
        [JsonPropertyName("type")]
        public virtual StageType Type { get; }
    }
}
