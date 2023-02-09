using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.Models.Stages;
using System.Runtime.Serialization;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    public class CreateQuestContract : CommonHttpRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public IList<Stage> Stages { get; set; }
    }
}
