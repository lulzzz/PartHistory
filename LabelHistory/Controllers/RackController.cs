using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabelHistory.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabelHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RackController : ControllerBase
    {
        [HttpGet()] //api/Rack?rackID=
        public async Task<IActionResult> GetRackDetailsAsync(
            [FromQuery] string rackID)
        {
            RackDataModel rack = new RackDataModel();
            Task<RackDataModel> getRackDetailsTask = Task.Run(() => DataAccess.DB.GetRackDetailsFromID(rackID));
            Task<List<RackItem>> getRackContentsTask = Task.Run(() => DataAccess.DB.GetRackContents(rackID));
            Task<List<RackActivityItem>> getRackHistoryTask = Task.Run(() => DataAccess.DB.GetRackActivityLog(rackID));
            rack = await getRackDetailsTask;
            rack.RackContents = await getRackContentsTask;
            rack.RackActivityLog = await getRackHistoryTask;
            return Ok(rack);
        }
    }
}
