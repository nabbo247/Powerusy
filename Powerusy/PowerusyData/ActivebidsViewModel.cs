using PagedList;
using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowerusyData
{
    public class ActivebidsViewModel : ViewModelBase
    {
        public ActivebidsViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new View_bid_jobs();
            UsrLst = new List<View_bid_jobs>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new View_bid_jobs();
            Entity = new View_bid_jobs();
            usrs = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public View_bid_jobs UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool GridView { get; set; }
        public bool ListView { get; set; }
        public bool IsStep3 { get; set; }
        public List<View_bid_jobs> UsrLst { get; set; }
        public View_bid_jobs SearchEntity { get; set; }
        public View_bid_jobs Entity { get; set; }
        public tbl_users usrs { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public HttpPostedFileBase BillofLading { get; set; }
        public HttpPostedFileBase Proformainvoice { get; set; }
        public HttpPostedFileBase PackagingLists { get; set; }
        public HttpPostedFileBase ItemPix { get; set; }
        public HttpPostedFileBase Others { get; set; }
        public string uploadedImageName { get; set; }
        public string BillofLadingName { get; set; }
        public string ProformainvoiceName { get; set; }
        public string PackagingListsName { get; set; }
        public string ItemPixName { get; set; }
        public string OthersName { get; set; }
        public bool EbizApproval { get; set; }
        protected override void Init()
        {
            UsrLst = new List<View_bid_jobs>();
            SearchEntity = new View_bid_jobs();
            Entity = new View_bid_jobs();
            usrs = new tbl_users();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            GetDropDown();
            base.Init();
        }

        public override void HandleRequest()
        {
            //// This is an example of adding on a new command
            switch (EventCommand.ToLower())
            {
                case "listview":
                    ListView = true;
                    Get();
                    break;
                case "gridview":
                    GridView = true;
                    Get();
                    break;
            }
            GetDropDown();
            base.HandleRequest();
        }

        protected override void Add()
        {
            IsValid = true;
            // Initialize Entity Object
            Entity = new View_bid_jobs();
            Entity.Name = string.Empty;
            //Entity. = string.Empty;
            //Entity.Price = 0;
            base.Add();
        }

        protected override void Edit()
        {
            //ListOfPOSManager mgr =
            // new ListOfPOSManager();
            // Get Product Data
            Entity = Get(
              Convert.ToInt32(EventArgument));

            base.Edit();
        }

        protected override void Save()
        {
            if (Mode == "Add")
            {
                Insert(Entity);
            }
            else
            {
                Delete(Entity);
            }
            // Set any validation errors
            ValidationErrors = ValidationErrors;
            // Set mode based on validation errors
            base.Save();
        }

        protected override void Delete()
        {
            // Create new entity
            Entity = new View_bid_jobs();
            // Get primary key from EventArgument
            Entity.id = Convert.ToInt32(EventArgument);
            //EventArgument.Trim();
            // Convert.ToInt32(EventArgument);
            // Call data layer to delete record
            Delete(Entity);
            // Reload the Data
            Get();
            base.Delete();
        }

        protected override void ResetSearch()
        {
            SearchEntity = new View_bid_jobs();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<View_bid_jobs> Get(View_bid_jobs entity)
        {
            List<View_bid_jobs> ret = new List<View_bid_jobs>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.Name))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.View_bid_jobs.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                ret = ret.FindAll(
                  p => p.Name.ToLower().
                        StartsWith(entity.Name.ToLower().Trim(),
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public View_bid_jobs Get(int ReqID)
        {
            List<View_bid_jobs> ret =
              new List<View_bid_jobs>();
            View_bid_jobs entity = null;
            // TODO: Add data access method here

            ret = CreateData();

            UsrReq.id = ReqID;
            UsrReq = GetPOSData();
            if (ReqID > 0)
                ret = ret.Where(x => x.id == ReqID).ToList();
            if (ret.Count > 0)
                usrs = GetUsrData(ret[0].UserID);

            //GetDropDown();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return ret[0] == null ? null : ret[0];
        }

        public bool Update(View_bid_jobs entity)
        {
            bool ret = false;
            ret = Validate(entity);
            GetDropDown();
            string op = "";

            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.View_bid_jobs where info.id == UsrReq.id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;

                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return ret;
        }

        public bool Delete(View_bid_jobs entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                int ID = Convert.ToInt32(EventArgument);
                var retEnt = db.tbl_bookmarked.Where(x => x.BidID == ID).SingleOrDefault();
                db.Entry(retEnt).State = EntityState.Deleted;
                db.SaveChanges();
                IsValid = true;
                string op = "Booked marked remove successful";
                Msg = op;
            }
            return true;
        }

        public bool Validate(View_bid_jobs entity)
        {
            ValidationErrors.Clear();
            //if (string.IsNullOrEmpty(SeletedCountry))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply country."));
            //    IsValid = false;
            //}
            if (string.IsNullOrEmpty(entity.VesselName))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your VesselName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.BookingNo))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your BookingNo."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.BLNumber))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your BLNumber."));
                IsValid = false;
            }
            if ((entity.EstBudget <= 0))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply Est Budget."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.Consignee))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Consignee."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.PortDischarge))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Port of Discharge."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.PortLoading))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Port of Loading."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.GoodDescription))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Good Description."));
                IsValid = false;
            }
            //TODO
            return (ValidationErrors.Count == 0);
        }

        public bool Insert(View_bid_jobs entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {

            }
            return ret;
        }
        protected void GetDropDown()
        {
            //SelectList ItemE2 = new SelectList();
            //ItemE2.Text = "Review & Approve"; ItemE2.Value = "4";
            //List.Add(ItemE2);
            //SelectList ItemE = new SelectList();
            //ItemE.Text = "Review & Reject"; ItemE.Value = "2";
            //List.Add(ItemE);
            //Entity.BusOcpCode
            using (var db = new powerusyDBCoreEntities())
            {
                var sta = (from s in db.tbl_services orderby s.id ascending select s.Name).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
                }

                //var Countrysta = (from s in db.tbl_counntries select s.CountryName).Distinct();
                //foreach (var bn in Countrysta)
                //{
                //    SelectList item = new SelectList();
                //    item.Text = bn;
                //    item.Value = bn;
                //    CountryList.Add(item);
                //}
            }
        }
        protected View_bid_jobs GetPOSData()
        {
            View_bid_jobs ret = new View_bid_jobs();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.View_bid_jobs.Where(x => x.id == UsrReq.id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ValidationErrors.Add(new
                      KeyValuePair<string, string>("POS",
                      ex.Message));
            }
            finally
            {
                //db.Configuration.Close();
            }
            return ret;
        }
        protected tbl_users GetUsrData(int? Id)
        {
            tbl_users ret = new tbl_users();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_users.Where(x => x.id == Id).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                ValidationErrors.Add(new
                      KeyValuePair<string, string>("POS",
                      ex.Message));
            }
            finally
            {
                //db.Configuration.Close();
            }
            return ret;
        }
        protected List<View_bid_jobs> CreateData()
        {
            List<View_bid_jobs> ret = new List<View_bid_jobs>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    int UsrID = Convert.ToInt32(UserId);
                    ret = db.View_bid_jobs.Where(z => z.AgentID == UsrID).ToList();
                    if (pageNumber > 0 && pageSize > 0)
                    {
                        PageList = ret.ToPagedList(pageNumber, pageSize);
                        ret = ret.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationErrors.Add(new
                      KeyValuePair<string, string>("POS",
                      ex.Message));
            }
            finally
            {
                //db.Configuration.Close();
            }
            return ret;
        }

    }
}

