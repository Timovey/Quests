using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestService.Core.HelperModels
{
    public class RedisSetting
    {
        public string RoomKey { get; set; }

        public string Host { get; set;}

        public string InstanceName { get; set; }
    }
}
