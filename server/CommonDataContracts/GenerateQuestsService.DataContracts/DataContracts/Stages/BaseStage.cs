using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateQuestsService.DataContracts.DataContracts.Stages
{
    public abstract class BaseStage
    {
        public int Type { get; set; }

        public string Value { get; set; }
    }
}
