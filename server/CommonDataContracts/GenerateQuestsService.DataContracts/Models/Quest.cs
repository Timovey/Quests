
using GenerateQuestsService.DataContracts.Models.Stages;

namespace GenerateQuestsService.DataContracts.Models
{
    public class Quest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public IList<Stage> Stages { get; set; }
    }
}
