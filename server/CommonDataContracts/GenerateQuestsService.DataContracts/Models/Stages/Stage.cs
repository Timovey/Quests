using GenerateQuestsService.DataContracts.Enums;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public class Stage
    {
        /// <summary>
        /// internal set - получить в модели id можно,присвоить нельзя
        /// </summary>

        public int Order { get; set; }

        public string Title { get; set; }

        public virtual StageType Type { get; }
    }
}
