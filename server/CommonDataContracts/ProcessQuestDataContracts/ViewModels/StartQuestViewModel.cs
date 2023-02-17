using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.ViewModels
{
    public class StartQuestViewModel
    {
        /// <summary>
        /// Ид открывшегося прохождения
        /// </summary>
        public string Room { get; set; }

        public QuestProcessViewModel Quest { get; set; }
    }
}
