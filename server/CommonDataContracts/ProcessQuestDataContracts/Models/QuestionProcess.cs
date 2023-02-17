using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Models
{
    public class QuestionProcess
    {
        public QuestionProcessType Type { get; set; }

        public string Title { get; set; }

        public string[] Answers { get; set; }
    }
}
