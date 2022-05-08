using PagedList;
using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowerusyData
{
    public class JobdetailViewModel : ViewModelBase
    {
        public JobdetailViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_bidding();
            UsrLst = new List<tbl_bidding>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new tbl_bidding();
            Entity = new tbl_bidding();
            JobBid = new tbl_bidding_jobs();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_bidding UsrReq { get; set; }
        public tbl_bidding_jobs JobBid { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool IsStep1 { get; set; }
        public bool IsStep2 { get; set; }
        public bool Agree { get; set; }
        public List<tbl_bidding> UsrLst { get; set; }
        public tbl_bidding SearchEntity { get; set; }
        public tbl_bidding Entity { get; set; }
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
        public bool Owner { get; set; }
        protected override void Init()
        {
            UsrLst = new List<tbl_bidding>();
            SearchEntity = new tbl_bidding();
            Entity = new tbl_bidding();
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
                case "continue":
                    if (Validate(Entity))
                    {
                        IsStep2 = true;
                        IsStep1 = false;
                    }
                    else
                    {
                        IsStep2 = false;
                        IsStep1 = true;
                    }
                    break;
                case "previous":
                    IsStep2 = false;
                    IsStep1 = true;
                    break;
                case "bid":
                    SubmitBid();
                    Get();
                    break;
                case "savejob":
                    savejob();
                    Get();
                    break;
            }
            GetDropDown();
            base.HandleRequest();
        }

        private void savejob()
        {
            using (var db = new powerusyDBCoreEntities())
            {
                tbl_bidding_bookmark ent = new tbl_bidding_bookmark();
                int ID = Convert.ToInt32(UserId);
                ent.bidding_id = JobBid.BidID;
                ent.bookmarked_by_id = ID;
                ent.status = 1;
                ent.Date = DateTime.Now;
                db.tbl_bidding_bookmark.Add(ent);
                db.SaveChanges();
                IsValid = true;
                Msg = "Saved successful";
                Entity = Get( Convert.ToInt32(EventArgument));
            }
            //throw new NotImplementedException();
        }

        private bool SubmitBid()
        {
            bool ret = false;
            ret = ValidateBid(JobBid);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    int ID = Convert.ToInt32(UserId);
                    var rs = (from info in db.tbl_shipper where info.userid == ID && info.statusid==0 select info).FirstOrDefault();
                    if (rs == null)
                    {
                        ValidationErrors.Add(new KeyValuePair<string, string>("Comment", "Update your company profile to bid for jobs."));
                        IsValid = false;
                        Entity = Get( Convert.ToInt32(EventArgument));
                        return ret;
                    }
                    JobBid.AgentID = ID;
                    JobBid.Date = DateTime.Now;
                    db.tbl_bidding_jobs.Add(JobBid);
                    db.SaveChanges();
                    IsValid = true;
                    Msg = "Bid created successful";
                }
            }else
                Entity = Get(Convert.ToInt32(EventArgument));
            return ret;
        }
        //savejob
        public bool ValidateBid(tbl_bidding_jobs entity)
        {
            ValidationErrors.Clear();
            if (string.IsNullOrEmpty(entity.Comment))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply comment."));
                IsValid = false;
            }
            if (entity.Amount<=0)
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your amount."));
                IsValid = false;
            }
            if (Agree != true)
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "You must agree to the policy."));
                IsValid = false;
            }
            
            return (ValidationErrors.Count == 0);
        }
        protected override void Add()
        {
            IsValid = true;
            // Initialize Entity Object
            Entity = new tbl_bidding();
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
                Update(Entity);
            }
            // Set any validation errors
            ValidationErrors = ValidationErrors;
            // Set mode based on validation errors
            base.Save();
        }

        protected override void Delete()
        {
            // Create new entity
            Entity = new tbl_bidding();
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
        public void BookmarkJob(int bidId)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                var bid = db.tbl_bidding.SingleOrDefault(b => b.id == bidId);

                string viewerId = $"{HttpContext.Current.Session[SessionKeys.UserId]}";

                int bookmarkedById = string.IsNullOrEmpty(viewerId) ? 0 : Convert.ToInt32(viewerId);

                //User is not a logged in user do not attempt to save/bookmark this bidding
                if (bookmarkedById == 0)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("UnauthenticatedUser", "Please Login to bookmark(save) this job."));
                    IsValid = false;
                    return;

                }

                var bookmarkBid = db.tbl_bidding_bookmark.SingleOrDefault(b => b.bidding_id == bidId && b.bid_owner_id == bid.UserID && b.bookmarked_by_id == bookmarkedById);

                //Bookmark already exist remove it
                if (bookmarkBid != null)
                {
                    db.tbl_bidding_bookmark.Remove(bookmarkBid);
                    Msg = "Removed successfully";
                }
                else
                {
                    var bookmarkJob = new tbl_bidding_bookmark
                    {
                        bidding_id = bidId,
                        bid_owner_id = bid.UserID.Value,
                        bookmarked_by_id = bookmarkedById,
                    };

                    db.tbl_bidding_bookmark.Add(bookmarkJob);
                    Msg = "Saved Successfully";
                }
                IsValid = true;

                db.SaveChanges();
            }
        }
        protected override void ResetSearch()
        {
            SearchEntity = new tbl_bidding();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<tbl_bidding> Get(tbl_bidding entity)
        {
            List<tbl_bidding> ret = new List<tbl_bidding>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.Name))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_bidding.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                ret = ret.FindAll(
                  p => p.Name.ToLower().
                        StartsWith(entity.Name.ToLower().Trim(),
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_bidding Get(int ReqID)
        {
            List<tbl_bidding> ret =
              new List<tbl_bidding>();
            tbl_bidding entity = null;
            // TODO: Add data access method here

            ret = CreateData();

            UsrReq.id = ReqID;
            UsrReq = GetPOSData();
            if (ReqID > 0)
                ret = ret.Where(x => x.id == ReqID).ToList();

            GetDropDown();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return ret[0] == null ? null : ret[0];
        }

        public bool Update(tbl_bidding entity)
        {
            bool ret = false;
            ret = Validate(entity);
            GetDropDown();
            string op = "";

            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_bidding where info.id == UsrReq.id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;

                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return ret;
        }
        
        public bool Delete(tbl_bidding entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_bidding entity)
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

        public bool Insert(tbl_bidding entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var UserIDI = db.tbl_users.Where(x => x.username == UserId).FirstOrDefault();
                    //UserIDI.id = 1;
                    int userID = UserIDI.id;
                    if (!string.IsNullOrEmpty(BillofLadingName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Bill of Lading";
                        string path = BillofLadingName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(PackagingListsName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Packaging Lists";
                        string path = PackagingListsName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ProformainvoiceName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Proforma invoice";
                        string path = ProformainvoiceName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(OthersName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Others Name";
                        string path = OthersName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ItemPixName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Item Pix";
                        string path = ItemPixName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }

                    entity.UserID = userID;
                    entity.GoodsType = ActionTypeId;
                    entity.statusid = 1;
                    entity.Date = DateTime.Now;
                    db.tbl_bidding.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "created job order successful";
                    Msg = op;
                    //IsStep3 = true;
                }
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
        protected tbl_bidding GetPOSData()
        {
            tbl_bidding ret = new tbl_bidding();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_bidding.Where(x => x.id == UsrReq.id).SingleOrDefault();
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
        protected List<tbl_bidding> CreateData()
        {
            List<tbl_bidding> ret = new List<tbl_bidding>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_bidding.ToList();
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
