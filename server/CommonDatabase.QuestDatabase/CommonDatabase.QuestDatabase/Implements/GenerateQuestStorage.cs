using CommonDatabase.QuestDatabase.Interfaces;
using GenerateQuestsService.DataContracts.Models;

namespace CommonDatabase.QuestDatabase.Implements
{
    public class GenerateQuestStorage : IGenerateQuestStorage
    {
        private QuestContext _questContext;
        public GenerateQuestStorage(QuestContext questContext)
        {
            _questContext = questContext;
        }

        public async Task CreateQuestAsync(Quest quest)
        {
            await _questContext.Quests.AddAsync(quest);
            await _questContext.SaveChangesAsync();
        }

    }
}
