using GenerateQuestsService.DataContracts.Models.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class ShortQuestViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public int StageCount { get; set; }
        /// <summary>
        /// ИД Автора квеста
        /// </summary>
        [JsonIgnore]
        public int UserId { get; set; }

        public string Author { get; set; }
    }
}
