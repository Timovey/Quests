using GenerateQuestsService.DataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.Models
{
    public class Question
    {
        public QuestionType Type { get; set; }

        public string Title { get; set; }

        public string[] Answers { get; set; }

        public string[] RightAnswers { get; set; }
    }
}
