using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.Models.Stages;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class UpdateQuestContract : CommonHttpRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public IList<Stage> Stages { get; set; }
    }
}
