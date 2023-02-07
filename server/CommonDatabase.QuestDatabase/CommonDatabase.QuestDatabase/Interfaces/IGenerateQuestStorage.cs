using GenerateQuestsService.DataContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.QuestDatabase.Interfaces
{
    public interface IGenerateQuestStorage
    {
        public Task CreateQuestAsync(Quest quest);
    }
}
