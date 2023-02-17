using CommonInfrastructure.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessQuestDataContracts.DataContracts;
using ProcessQuestDataContracts.ViewModels;
using ProcessQuestService.Core.BusinessLogic;

namespace ProcessQuestService.Main.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProcesssQuestController : ControllerBase
    {     
        private ProcessQuestLogic _processQuestLogic;
        
        public ProcesssQuestController(ProcessQuestLogic processQuestLogic) {
            _processQuestLogic = processQuestLogic;
        }

        //[HttpPost]
        //public async Task<CommonHttpResponse<IList<DriverViewModel>>> StartQuestAsync(StartQuestContract contract)
        //{
        //    return await _driverLogic.GetFilteredDriversAsync(filter);
        //}

        [HttpPost]
        public async Task<CommonHttpResponse<StartQuestViewModel>> ConnectToQuestAsync(StartQuestContract contract)
        {
            return await _processQuestLogic.ConnectToQuestAsync(contract);
        }


    }
}
