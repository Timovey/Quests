using AutoMapper;
using CommonDatabase.QuestDatabase.Interfaces;
using CommonDatabase.QuestDatabase.Models;
using CommonDatabase.QuestDatabase.Models.Stages;
using GenerateQuestsService.DataContracts.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.Implements
{
    public class GenerateQuestStorage : IGenerateQuestStorage
    {
        private QuestContext _questContext;
        private IMapper _mapper;

        public GenerateQuestStorage(QuestContext questContext, IMapper mapper)
        {
            _questContext = questContext;
            _mapper = mapper;
        }

        public async Task CreateQuestAsync(CreateQuestContract contract)
        {
            var quest = _mapper.Map<QuestEntity>(contract);
            await _questContext.Quests.AddAsync(quest);
            await _questContext.SaveChangesAsync();
        }

        public async Task<QuestViewModel> GetQuestAsync(GetQuestContract contract)
        {
            var result = await _questContext.Quests.Where(q =>
                q.IsDeleted == false &&
                q.Id == contract.Id).Include(x => x.Stages)
                    .FirstOrDefaultAsync();

            foreach(var stage in result.Stages)
            {
                if(stage is MapStageEntity)
                {
                    var mapStage = (stage as MapStageEntity);
                    var cords = await _questContext.Coordinates.Where(x =>
                    x.IsDeleted == false && 
                    x.MapStage.Id == mapStage.Id)
                        .FirstOrDefaultAsync();
                    mapStage.Coords = cords;
                }
            }
            return _mapper.Map<QuestViewModel>(result);
        }

        public async Task<IList<ShortQuestViewModel>> GetFilteredQuestsAsync(GetFilteredQuestsContract contract)
        {
            var quests = await _questContext.Quests
                .Where(q => q.IsDeleted == false)
                .ToListAsync();

            var result = new List<ShortQuestViewModel>();
            foreach (var quest in quests)
            {
               result.Add(_mapper.Map<ShortQuestViewModel>(quest));
            }
            return result;
        }

    }
}
