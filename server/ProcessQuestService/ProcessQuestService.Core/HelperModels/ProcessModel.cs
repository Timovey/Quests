using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessQuestService.Core.HelperModels
{
    public class ProcessModel
    {
        public ProcessModel() {
            UserProcessing = new Dictionary<int,int>();
        }
        public string Key { get; set; }
        public IDictionary<int, int> UserProcessing { get; set; }

        public bool IsHaveUser(int userId)
        {
            return UserProcessing.ContainsKey(userId);
        }
    }
}
