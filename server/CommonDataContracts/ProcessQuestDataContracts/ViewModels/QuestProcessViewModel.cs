using ProcessQuestDataContracts.Models.Stages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.ViewModels
{
    public class QuestProcessViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Img { get; set; }

        public IList<StageProcess> Stages { get; set; }

        /// <summary>
        /// ИД Автора квеста
        /// </summary>
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
