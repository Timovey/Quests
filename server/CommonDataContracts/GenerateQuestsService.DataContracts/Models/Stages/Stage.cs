using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.Enums;

namespace GenerateQuestsService.DataContracts.Models.Stages
{
    public abstract class Stage : CommonHttpRequest
    {
        public string Title { get; set; }

        public abstract StageType Type { get; }
    }
}
