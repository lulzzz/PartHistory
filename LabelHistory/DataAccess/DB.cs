using LabelHistory.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LabelHistory.DataAccess
{
    public class DB
    {
        private static string DECOSQL = "Data Source=decosql;Persist Security Info=True;User ID=PartHistoryUser;Password=PartHistoryUser";

        //TODO: get pictures FROM [PaintRecords].[dbo].[ImagesStyles]

        public static PaintDataModel GetPaintData(string paintBarcode)
        {
            string _err = string.Empty;
            string sql = "[Production].[dbo].[GetDetailsFromPaintLabel]";
            PaintDataModel paintData = new PaintDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaintLabel", paintBarcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //from paint exit
                                paintData.PaintExitID = reader.GetInt64(reader.GetOrdinal("pnt_id"));
                                paintData.PaintLabelCreatedAt = reader.GetDateTime(reader.GetOrdinal("pnt_create_datetime"));
                                paintData.PaintBarcode = reader.GetString(reader.GetOrdinal("pnt_barcode"));
                                paintData.PaintCarrier = reader.GetString(reader.GetOrdinal("pnt_carrier"));
                                paintData.PaintCarrierPosition = reader.GetInt32(reader.GetOrdinal("pnt_carrier_pos"));
                                paintData.PaintRound = reader.GetInt32(reader.GetOrdinal("pnt_round"));
                                paintData.ColorNumber = reader.GetInt32(reader.GetOrdinal("pnt_color"));
                                paintData.ColorDescription = reader.GetString(reader.GetOrdinal("ColorDescription"));
                                paintData.StyleNumber = reader.GetInt32(reader.GetOrdinal("pnt_style"));
                                paintData.PaintExitScanStatus = reader.GetString(reader.GetOrdinal("pnt_scan_status"));
                                paintData.PaintExitStatusChangedAt = reader.GetDateTime(reader.GetOrdinal("pnt_scan_status_change"));

                                // from paint schedule legend
                                paintData.RawPartNumber = reader.GetString(reader.GetOrdinal("Raw P/N")); // use this to get Master part data
                                paintData.Program = reader.GetString(reader.GetOrdinal("Program"));
                                paintData.Set = reader.GetString(reader.GetOrdinal("Set"));
                                paintData.PartsPerCarrier = reader.GetInt32(reader.GetOrdinal("PartsPerCar"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (paintData);
        }
        public static PaintDataModel GetPaintLegendData(string paintBarcode)
        {
            string _err = string.Empty;
            string sql = "[Production].[dbo].[GetDetailsFromPaintLabel]";
            PaintDataModel paintData = new PaintDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaintLabel", paintBarcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                //from paint exit
                                paintData.PaintExitID = reader.GetInt64(reader.GetOrdinal("pnt_id"));
                                paintData.PaintLabelCreatedAt = reader.GetDateTime(reader.GetOrdinal("pnt_create_datetime"));
                                paintData.PaintBarcode = reader.GetString(reader.GetOrdinal("pnt_barcode"));
                                paintData.PaintCarrier = reader.GetString(reader.GetOrdinal("pnt_carrier"));
                                paintData.PaintCarrierPosition = reader.GetInt32(reader.GetOrdinal("pnt_carrier_pos"));
                                paintData.PaintRound = reader.GetInt32(reader.GetOrdinal("pnt_round"));
                                paintData.ColorNumber = reader.GetInt32(reader.GetOrdinal("pnt_color"));
                                paintData.ColorDescription = reader.GetString(reader.GetOrdinal("ColorDescription"));
                                paintData.StyleNumber = reader.GetInt32(reader.GetOrdinal("pnt_style"));
                                paintData.PaintExitScanStatus = reader.GetString(reader.GetOrdinal("pnt_scan_status"));
                                paintData.PaintExitStatusChangedAt = reader.GetDateTime(reader.GetOrdinal("pnt_scan_status_change"));

                                // from paint schedule legend
                                paintData.RawPartNumber = reader.GetString(reader.GetOrdinal("Raw P/N")); // use this to get Master part data
                                paintData.Program = reader.GetString(reader.GetOrdinal("Program"));
                                paintData.Set = reader.GetString(reader.GetOrdinal("Set"));
                                paintData.PartsPerCarrier = reader.GetInt32(reader.GetOrdinal("PartsPerCar"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (paintData);
        }
        public static PartMasterData GetPartMasterData(string rawPartNumber)
        {
            string _err = string.Empty;
            string sql = "[Decostar].[dbo].[GetPTMasterDetails]";
            PartMasterData masterData = new PartMasterData();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PartNumber", rawPartNumber);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                
                                masterData.PartNumber = reader.GetString(reader.GetOrdinal("pt_part"));
                                masterData.PartDescription1 = reader.GetString(reader.GetOrdinal("pt_desc1"));
                                masterData.PartDescription2 = reader.GetString(reader.GetOrdinal("pt_desc2"));
                                masterData.ProdLine = reader.GetString(reader.GetOrdinal("pt_prod_line"));
                                masterData.PartGroup = reader.GetString(reader.GetOrdinal("pt_group"));
                                masterData.PartType = reader.GetString(reader.GetOrdinal("pt_part_type"));
                                masterData.PartStatus = reader.GetString(reader.GetOrdinal("pt_status"));
                                masterData.PartLocation = reader.GetString(reader.GetOrdinal("pt_loc"));
                                masterData.PartSite = reader.GetString(reader.GetOrdinal("pt_site"));
                                masterData.FullDescription = reader.GetString(reader.GetOrdinal("full_desc"));
                                masterData.OptionDescription = reader.GetString(reader.GetOrdinal("opt_desc"));
                                masterData.PartTypeDescription = reader.GetString(reader.GetOrdinal("part_type"));
                                masterData.Position = reader.GetString(reader.GetOrdinal("pt_group"));
                                masterData.CustomerPartNumber = reader.GetString(reader.GetOrdinal("cp_cust_part"));
                                masterData.CPComment = reader.GetString(reader.GetOrdinal("cp_comment"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (masterData);
        }
        public static MoldDataModel GetMoldData(string moldLabel)
        {
            if (moldLabel == null)
            {
                return null;
            }
            string _err = string.Empty;
            string sql = "[Production].[dbo].[GetUniqueFromMold]";
            MoldDataModel moldData = new MoldDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@moldLabel", moldLabel);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                moldData.UniqueRefenceID = reader.GetInt64(reader.GetOrdinal("REFERENCE_ID"));

                                moldData.MoldBarcode = reader.GetString(reader.GetOrdinal("SerialNumber"));
                                moldData.PartNumber = reader.GetString(reader.GetOrdinal("PartNumber"));

                                moldData.CreatedAt = reader.GetDateTime(reader.GetOrdinal("DateTime"));

                                moldData.Press = reader.GetString(reader.GetOrdinal("Line"));

                                moldData.StatusCode = reader.GetInt32(reader.GetOrdinal("STATUS_CODE"));

                                moldData.MachineId = reader.GetString(reader.GetOrdinal("MachineID"));

                                moldData.TranVersion = reader.GetGuid(reader.GetOrdinal("msrepl_tran_version"));
                                moldData.RowGuid = reader.GetGuid(reader.GetOrdinal("rowguid"));


                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (moldData);
        }
        public static FinesseDataModel GetFinesseData(string barcode, string barcodeType)
        {
            string _err = string.Empty;
            string sql = "";
            if (barcodeType == "Mold")
            {
                sql = "[Finesse].[dbo].[GetFinesseDataFromMold]";
            }
            else
            {
                sql = "[Finesse].[dbo].[GetFinesseDataFromPaint]";
            }

            FinesseDataModel finesseData = new FinesseDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                //finesse data
                                finesseData.RejectID = reader.GetInt32(reader.GetOrdinal("RejectID"));
                                finesseData.FinesseStatus = reader.GetString(reader.GetOrdinal("Status"));
                                finesseData.FinesseOperatorBadge = reader.GetString(reader.GetOrdinal("Operator"));
                                finesseData.FinneseOperatorName = reader.GetString(reader.GetOrdinal("OperatorName"));
                                finesseData.Time = reader.GetDateTime(reader.GetOrdinal("Time"));
                                finesseData.FinesseWorkArea = reader.GetString(reader.GetOrdinal("WorkArea"));
                                finesseData.FinessePrinter = reader.GetString(reader.GetOrdinal("Printer"));
                                finesseData.PaintBarcode = reader.GetString(reader.GetOrdinal("PaintLabel"));
                                if (!reader.IsDBNull(reader.GetOrdinal("MoldLabel")))
                                    { finesseData.MoldBarcode = reader.GetString(reader.GetOrdinal("MoldLabel")); }
                                finesseData.PartNumber = reader.GetString(reader.GetOrdinal("PartNumber"));
                                finesseData.StyleNumber = reader.GetInt32(reader.GetOrdinal("Style"));
                                finesseData.ColorNumber = reader.GetInt32(reader.GetOrdinal("Color"));
                                finesseData.ColorDescription = reader.GetString(reader.GetOrdinal("ColorDescription"));


                                //part.PartDescription1 = reader.GetString(reader.GetOrdinal("pt_desc1"));
                                //part.PartDescription2 = reader.GetString(reader.GetOrdinal("pt_desc2"));
                                //part.PartPMCode = reader.GetString(reader.GetOrdinal("pt_pm_code"));
                                //part.ProdLine = reader.GetString(reader.GetOrdinal("pt_prod_line"));
                                //part.PartGroup = reader.GetString(reader.GetOrdinal("pt_group"));
                                //part.PartType = reader.GetString(reader.GetOrdinal("pt_part_type"));
                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (finesseData);
        }
        
        public static RackDataModel GetRackDetailsFromID(string rackID)
        {
            string _err = string.Empty;
            //string sql = "[Production].[dbo].[GetRackDetails]";
            string sql = "SELECT TOP (1000) [RACK_ID],[ENTRY_DATE],[RACK_STYLE_ID] ,[PART_NUMBER] ,[QUANTITY] ,[COMPLETE] ,[STATUS], [LAST_LOCATION], [APPLICATION] FROM[Production].[dbo].[RackHistory] WHERE RACK_ID = @rackID";
            RackDataModel rack = new RackDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rackID", rackID);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rack.ID = reader.GetInt64(reader.GetOrdinal("RACK_ID"));
                                rack.CreatedAt = reader.GetDateTime(reader.GetOrdinal("ENTRY_DATE"));
                                rack.PartNumber = reader.GetString(reader.GetOrdinal("PART_NUMBER"));
                                rack.Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));
                                rack.Complete = reader.GetBoolean(reader.GetOrdinal("COMPLETE"));
                                rack.Status = reader.GetInt32(reader.GetOrdinal("STATUS")); // TODO: Update this to get the string meaning
                                rack.LastLocation = reader.GetString(reader.GetOrdinal("LAST_LOCATION"));
                                if (!reader.IsDBNull(reader.GetOrdinal("SHIP_LOCATION")))
                                    { rack.ShipLocation = reader.GetString(reader.GetOrdinal("SHIP_LOCATION")); }
                                rack.AppUsed = reader.GetString(reader.GetOrdinal("APPLICATION"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (rack);
        }
        public static RackDataModel GetRackDetails(string barcode, string barcodeType)
        {
            string _err = string.Empty;
            string sql = "";
            if (barcodeType == "Mold")
            {
                sql = "[Production].[dbo].[GetRackDataFromMold]";
            }
            else
            {
                sql = "[Production].[dbo].[GetRackDataFromPaint]";
            }
            RackDataModel rack = new RackDataModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@paintBarcode ", barcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                rack.ID = reader.GetInt64(reader.GetOrdinal("RACK_ID"));
                                rack.CreatedAt = reader.GetDateTime(reader.GetOrdinal("ENTRY_DATE"));
                                rack.PartNumber = reader.GetString(reader.GetOrdinal("PART_NUMBER"));
                                rack.Quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));
                                rack.Complete = reader.GetBoolean(reader.GetOrdinal("COMPLETE"));
                                rack.Status = reader.GetInt32(reader.GetOrdinal("STATUS")); // TODO: Update this to get the string meaning
                                rack.LastLocation = reader.GetString(reader.GetOrdinal("LAST_LOCATION"));
                                if (!reader.IsDBNull(reader.GetOrdinal("SHIP_LOCATION")))
                                { rack.ShipLocation = reader.GetString(reader.GetOrdinal("SHIP_LOCATION")); }
                                rack.AppUsed = reader.GetString(reader.GetOrdinal("APPLICATION"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (rack);
        }
        public static List<RackItem> GetRackContents(string rackID)
        {
            string _err = string.Empty;
            //string sql = "[Production].[dbo].[GetRackDetails]";
            string sql = "SELECT * FROM [RackRecords].[dbo].[Paint_Rack_XRef] WHERE RACK_ID = @rackID order by ID desc";

            List<RackItem> rackItems = new List<RackItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rackID", rackID);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rackItem = new RackItem();

                                rackItem.RackItemID = reader.GetInt64(reader.GetOrdinal("ID"));
                                rackItem.RackID = reader.GetInt32(reader.GetOrdinal("rack_id"));
                                rackItem.DateTimeAdded = reader.GetDateTime(reader.GetOrdinal("date"));
                                rackItem.PaintScan = reader.GetString(reader.GetOrdinal("paintscan"));
                                if (!reader.IsDBNull(reader.GetOrdinal("moldscan")))
                                    { rackItem.MoldScan = reader.GetString(reader.GetOrdinal("moldscan")); }
                                rackItems.Add(rackItem);
                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (rackItems);
        }
        public static List<RackActivityItem> GetRackActivityLog(string rackID)
        {
            string _err = string.Empty;
            //string sql = "[Production].[dbo].[Get..]";
            string sql = "SELECT * FROM [Production].[dbo].[RackHistory_ActivityLog] WHERE RACK_ID = @rackID order by Action_DateTime desc";

            List<RackActivityItem> rackActivityItems = new List<RackActivityItem>();
            var id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rackID", rackID);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rackActivityItem = new RackActivityItem();
                                rackActivityItem.ID = id;
                                
                                rackActivityItem.RackID = reader.GetString(reader.GetOrdinal("Rack_ID"));
                                rackActivityItem.ActionDateTime = reader.GetDateTime(reader.GetOrdinal("Action_DateTime"));
                                rackActivityItem.Action = reader.GetString(reader.GetOrdinal("Transaction_Type"));
                                rackActivityItem.Username = reader.GetString(reader.GetOrdinal("Username"));
                                rackActivityItem.Message = reader.GetString(reader.GetOrdinal("Message"));

                                rackActivityItems.Add(rackActivityItem);
                                id++;
                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (rackActivityItems);
        }
        public static string GetPaintFromRackedMold(string moldBarcode)
        {
            string paintBarcode = "";
            string _err = string.Empty;
            //string sql = "[Production].[dbo].[GetRackDetails]";
            string sql = "SELECT [paintscan] FROM [RackRecords].[dbo].[Paint_Rack_XRef] where [moldscan] = @moldScan";

            List<RackItem> rackItems = new List<RackItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@moldScan", moldBarcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                paintBarcode = reader.GetString(reader.GetOrdinal("paintscan"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (paintBarcode);
        }
        public static string GetPaintFromFinesseMold(string moldBarcode)
        {
            string paintBarcode = "";
            string _err = string.Empty;
            //string sql = "[Production].[dbo].[]";
            string sql = "SELECT [PaintLabel] FROM [Finesse].[dbo].[Reject] where [MoldLabel] = @moldScan";

            List<RackItem> rackItems = new List<RackItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(DECOSQL))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@moldScan", moldBarcode);
                        cmd.Connection = conn;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                paintBarcode = reader.GetString(reader.GetOrdinal("PaintLabel"));

                            }
                        }

                        reader.Close();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                _err = ex.Message;
            }

            return (paintBarcode);
        }

        public static List<Defect> GetDefects(int rejectID)
        {
            List<Defect> defects = new List<Defect>();
            int count = 1;
            string sql = "[Finesse].[dbo].[GetDefectsFromRejectID]";
            try
            {
                using (SqlConnection cn = new SqlConnection(DECOSQL))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@RejectID", rejectID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Defect defect = new Defect();
                                defect.Count = count;
                                defect.DefectID = reader.GetInt32(reader.GetOrdinal("ID"));
                                defect.X = reader.GetInt32(reader.GetOrdinal("X"));
                                defect.Y = reader.GetInt32(reader.GetOrdinal("Y"));
                                defect.Description = reader.GetString(reader.GetOrdinal("Description"));
                                defect.Category = reader.GetString(reader.GetOrdinal("Category"));

                                defects.Add(defect);
                                count++;
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return defects;
        }
    }
}
