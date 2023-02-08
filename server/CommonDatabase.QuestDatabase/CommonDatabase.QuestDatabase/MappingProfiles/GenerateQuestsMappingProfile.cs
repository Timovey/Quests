using AutoMapper;
using CommonDatabase.QuestDatabase.Models;
using GenerateQuestsService.DataContracts.DataContracts;

namespace CommonDatabase.QuestDatabase.MappingProfiles
{
    public class GenerateQuestsMappingProfile : Profile
    {
        public GenerateQuestsMappingProfile()
        {
            //----------------------------- DateRange
            CreateMap<CreateQuestContract, QuestEntity>();
            CreateMap<UpdateQuestContract, QuestEntity>();
            CreateMap<QuestEntity, QuestViewModel>();

        }
    }
}
