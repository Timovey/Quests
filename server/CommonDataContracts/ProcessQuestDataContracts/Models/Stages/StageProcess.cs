using ProcessQuestDataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestDataContracts.Models.Stages
{
    public class StageProcess
    {
        public int Order { get; set; }

        public string Title { get; set; }

        public virtual StageProcessType Type { get; }
    }
}
