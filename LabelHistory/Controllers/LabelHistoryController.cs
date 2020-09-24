using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabelHistory.Models;
using Microsoft.AspNetCore.Mvc;


namespace LabelHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelHistoryController : ControllerBase
    {
        [HttpGet()] //api/LabelHistory?barcode=
        public async Task<IActionResult> GetDetailsAsync(
            [FromQuery] string barcode)
        {
            if (barcode.Contains("~"))
            {
                Part part = new Part();
                part = await GetPartDataFromPaintLabel(barcode);

                return Ok(part);
            }
            else 
            {
                Part part = new Part();
                part = await GetPartDataFromMoldLabel(barcode);

                return Ok(part);
            }
        }

        private async Task<Part> GetPartDataFromMoldLabel(string moldBarcode)
        {
            Part part = new Part();
            part.MoldBarcode = moldBarcode;
            Task<MoldDataModel> getMoldDataTask = Task.Run(() => DataAccess.DB.GetMoldData(part.MoldBarcode));
            Task<FinesseDataModel> getFinesseDataTask = Task.Run(() => DataAccess.DB.GetFinesseData(part.MoldBarcode, "Mold"));
            Task<RackDataModel> getRackDataTask = Task.Run(() => DataAccess.DB.GetRackDetails(part.MoldBarcode, "Mold"));

            part.MoldData = await getMoldDataTask;
            Task<PartMasterData> getPartMasterDataTask = Task.Run(() => DataAccess.DB.GetPartMasterData(part.MoldData.PartNumber));

            part.FinesseData = await getFinesseDataTask;
            Task<List<Defect>> getDefectsTask = Task.Run(() => DataAccess.DB.GetDefects(part.FinesseData.RejectID));

            part.RackData = await getRackDataTask;
            string rackID = Convert.ToString(part.RackData.ID);
            Task<List<RackItem>> getRackContentsTask = Task.Run(() => DataAccess.DB.GetRackContents(rackID));
            Task<List<RackActivityItem>> getRackHistoryTask = Task.Run(() => DataAccess.DB.GetRackActivityLog(rackID));


            part.FinesseData.Defects = await getDefectsTask;
            part.RackData.RackContents = await getRackContentsTask;

            if (part.FinesseData.PaintBarcode != null)
            {
                part.PaintBarcode = part.FinesseData.PaintBarcode;
            }
            else if (part.RackData.RackContents != null)
            {
                foreach (var item in part.RackData.RackContents)
                {
                    if (item.MoldScan == part.MoldBarcode)
                    {
                        part.PaintBarcode = item.PaintScan;
                    }
                }
            }
            Task<PaintDataModel> getPaintDataTask = Task.Run(() => DataAccess.DB.GetPaintData(part.PaintBarcode));

            part.RackData.RackActivityLog = await getRackHistoryTask;
            part.PartMasterData = await getPartMasterDataTask;

            part.PaintData = await getPaintDataTask;
            return part;
        }

        private async Task<Part> GetPartDataFromPaintLabel(string paintBarcode)
        {
            Part part = new Part();
            part.PaintBarcode = paintBarcode;
            Task<PaintDataModel> getPaintDataTask = Task.Run(() => DataAccess.DB.GetPaintData(part.PaintBarcode));
            Task<FinesseDataModel> getFinesseDataTask = Task.Run(() => DataAccess.DB.GetFinesseData(part.PaintBarcode, "Paint"));
            Task<RackDataModel> getRackDataTask = Task.Run(() => DataAccess.DB.GetRackDetails(part.PaintBarcode, "Paint"));
            part.PaintData = await getPaintDataTask;
            Task<PartMasterData> getPartMasterDataTask = Task.Run(() => DataAccess.DB.GetPartMasterData(part.PaintData.RawPartNumber));
            part.FinesseData = await getFinesseDataTask;

            Task<List<Defect>> getDefectsTask = Task.Run(() => DataAccess.DB.GetDefects(part.FinesseData.RejectID));


            part.RackData = await getRackDataTask;
            string rackID = Convert.ToString(part.RackData.ID);
            Task<List<RackItem>> getRackContentsTask = Task.Run(() => DataAccess.DB.GetRackContents(rackID));
            Task<List<RackActivityItem>> getRackHistoryTask = Task.Run(() => DataAccess.DB.GetRackActivityLog(rackID));


            part.PartMasterData = await getPartMasterDataTask;
            part.FinesseData.Defects = await getDefectsTask;

            part.RackData.RackContents = await getRackContentsTask;
            if (part.FinesseData.MoldBarcode != null)
            {
                part.MoldBarcode = part.FinesseData.MoldBarcode;
            }
            else if (part.RackData.RackContents != null)
            {
                foreach (var item in part.RackData.RackContents)
                {
                    if (item.PaintScan == part.PaintBarcode)
                    {
                        part.MoldBarcode = item.MoldScan;
                    }
                }
            }
            Task<MoldDataModel> getMoldDataTask = Task.Run(() => DataAccess.DB.GetMoldData(part.MoldBarcode));
            part.RackData.RackActivityLog = await getRackHistoryTask;
            part.MoldData = await getMoldDataTask;
            return part;
        }

    }
}
