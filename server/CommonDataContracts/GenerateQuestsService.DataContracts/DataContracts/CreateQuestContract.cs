using CommonInfrastructure.Http;
using GenerateQuestsService.DataContracts.Models.Stages;
using System.Runtime.Serialization;

namespace GenerateQuestsService.DataContracts.DataContracts
{
    //[KnownType(typeof(MapStage))]
    //[KnownType(typeof(QrCodeStage))]
    //[KnownType(typeof(TestStage))]
    //[KnownType(typeof(TextStage))]
    //[KnownType(typeof(VideoStage))]
    public class CreateQuestContract
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public Stage Stage { get; set; }

        public Base d { get; set; }
    }
}
