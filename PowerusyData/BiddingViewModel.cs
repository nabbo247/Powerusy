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
    public class BiddingViewModel : ViewModelBase
    {
        public BiddingViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_bidding();
            UsrLst = new List<tbl_bidding>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new tbl_bidding();
            Entity = new tbl_bidding();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_bidding UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool IsStep1 { get; set; }
        public bool IsStep2 { get; set; }
        public bool IsStep3 { get; set; }
        public List<tbl_bidding> UsrLst { get; set; }
        public tbl_bidding SearchEntity { get; set; }
        public tbl_bidding Entity { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public HttpPostedFileBase BillofLading { get; set; }
        public HttpPostedFileBase Proformainvoice { get; set; }
        public HttpPostedFileBase PackagingLists { get; set; }
        public HttpPostedFileBase Others { get; set; }
        public string uploadedImageName { get; set; }
        public string BillofLadingName { get; set; }
        public string ProformainvoiceName { get; set; }
        public string PackagingListsName { get; set; }
        public string OthersName { get; set; }
        public bool EbizApproval { get; set; }
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

            }
            GetDropDown();
            base.HandleRequest();
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
            //if (string.IsNullOrEmpty(entity.bankname))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your BankName."));
            //    IsValid = false;
            //}
            //if (string.IsNullOrEmpty(entity.address))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your CompanyAddress."));
            //    IsValid = false;
            //}

            //if (string.IsNullOrEmpty(entity.companyname))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your CompanyName."));
            //    IsValid = false;
            //}
            //if (string.IsNullOrEmpty(entity.description))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply Description of the commercial activity."));
            //    IsValid = false;
            //}

            //if (string.IsNullOrEmpty(entity.workingdays))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your WorkingDays."));
            //    IsValid = false;
            //}
            //if (string.IsNullOrEmpty(entity.workinghours))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your WorkingHours."));
            //    IsValid = false;
            //}
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

                    entity.UserID = userID;
                    //entity.location = SeletedCountry;
                    //entity.statusid = 1;
                    //entity.dateadded = DateTime.Now;
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
