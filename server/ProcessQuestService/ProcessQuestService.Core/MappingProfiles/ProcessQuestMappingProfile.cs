using AutoMapper;
using GenerateQuestsService.DataContracts.DataContracts;
using ProcessQuestDataContracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestService.Core.MappingProfiles
{
    public class ProcessQuestMappingProfile : Profile
    {
        public ProcessQuestMappingProfile() {
            CreateMap<StartQuestContract, GetQuestContract>();
        }
    }
}
