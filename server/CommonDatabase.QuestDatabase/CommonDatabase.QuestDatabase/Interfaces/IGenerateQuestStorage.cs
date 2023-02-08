using GenerateQuestsService.DataContracts.DataContracts;

namespace CommonDatabase.QuestDatabase.Interfaces
{
    public interface IGenerateQuestStorage
    {
        public Task CreateQuestAsync(CreateQuestContract contract);
    }
}
