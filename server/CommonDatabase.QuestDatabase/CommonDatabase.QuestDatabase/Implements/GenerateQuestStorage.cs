using AutoMapper;
using CommonDatabase.QuestDatabase.Interfaces;
using CommonDatabase.QuestDatabase.Models;
using GenerateQuestsService.DataContracts.DataContracts;

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

    }
}
