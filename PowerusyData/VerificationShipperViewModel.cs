using PagedList;
using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerusyData
{
    public class VerificationShipperViewModel : ViewModelBase
    {
        public VerificationShipperViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_shipper();
            UsrLst = new List<View_Shipper>();
            Attach = new List<tbl_importation_document>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new View_Shipper();
            Entity = new View_Shipper();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_shipper UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public List<View_Shipper> UsrLst { get; set; }
        public List<tbl_importation_document> Attach { get; set; }
        public View_Shipper SearchEntity { get; set; }
        public View_Shipper Entity { get; set; }
        //public HttpPostedFileBase uploadedImage { get; set; }
        //public HttpPostedFileBase CertificateOfIncorporation { get; set; }
        //public HttpPostedFileBase MemorandumofAssociation { get; set; }
        //public HttpPostedFileBase ArticlesofAssociation { get; set; }
        //public HttpPostedFileBase PowerofAttorney { get; set; }
        //public HttpPostedFileBase Validbusinesslicense { get; set; }
        //public HttpPostedFileBase Auditedfinancial { get; set; }
        //public HttpPostedFileBase Taxclearance { get; set; }
        //public HttpPostedFileBase Others { get; set; }
        //public string uploadedImageName { get; set; }
        public string CertificateOfIncorporationName { get; set; }
        public string MemorandumofAssociationName { get; set; }
        public string ArticlesofAssociationName { get; set; }
        public string PowerofAttorneyName { get; set; }
        public string ValidbusinesslicenseName { get; set; }
        public string AuditedfinancialName { get; set; }
        public string TaxclearanceName { get; set; }
        public string OthersName { get; set; }
        public bool EbizApproval { get; set; }
        protected override void Init()
        {
            UsrLst = new List<View_Shipper>();
            Attach = new List<tbl_importation_document>();
            SearchEntity = new View_Shipper();
            Entity = new View_Shipper();
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
                case "reject":
                    Update(Entity, 2);
                    break;
            }
            GetDropDown();
            base.HandleRequest();
        }

        protected override void Add()
        {
            IsValid = true;
            // Initialize Entity Object
            Entity = new View_Shipper();
            Entity.companyname = string.Empty;
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
                Update(Entity,0);
            }
            // Set any validation errors
            ValidationErrors = ValidationErrors;
            // Set mode based on validation errors
            base.Save();
        }

        protected override void Delete()
        {
            // Create new entity
            Entity = new View_Shipper();
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
            SearchEntity = new View_Shipper();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<View_Shipper> Get(View_Shipper entity)
        {
            List<View_Shipper> ret = new List<View_Shipper>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.companyname))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_shipper.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                //ret = ret.FindAll(
                //  p => p.companyname.ToLower().
                //        StartsWith(entity.companyname.ToLower().Trim(),
                //                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public View_Shipper Get(int ReqID)
        {
            List<View_Shipper> ret = new List<View_Shipper>();
            tbl_shipper ret3 = new tbl_shipper();
            View_Shipper entity = null;
            // TODO: Add data access method here
            
            ret = CreateData();

            UsrReq.id = ReqID;
            UsrReq = GetPOSData();
            if (ReqID > 0)
                ret = ret.Where(x => x.TID == ReqID).ToList();

            GetDropDown();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            if(ret[0]!=null)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    string userid = ret[0].userid.ToString();
                    //var UserIDI = db.tbl_users.Where(x => x.username == UserId).FirstOrDefault();
                    Attach = db.tbl_importation_document.Where(x => x.importationid == userid).ToList();
                    if (pageNumber > 0 && pageSize > 0)
                    {
                        PageList = ret.ToPagedList(pageNumber, pageSize);
                        ret = ret.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    }
                }
            }
            
            return ret[0] == null ? null : ret[0];
        }

        public bool Update(View_Shipper entity, int st)
        {
            bool ret = false;
            ret = Validate2(entity);
            //GetDropDown();
            string op = "";

            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_shipper where info.id == entity.TID select info).FirstOrDefault();
                    rs.comment = entity.comment;
                    int usrid = Convert.ToInt32(UserId);
                    rs.approvedby = usrid;
                    rs.statusid = st;
                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                    if (st == 0)
                        Msg = "Successfully approved request";
                    else
                        Msg = "Return successful";
                    IsValid = true;
                }
            }
            return ret;
        }

        public bool Delete(View_Shipper entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_shipper entity)
        {
            ValidationErrors.Clear();
            if (string.IsNullOrEmpty(SeletedCountry))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply country."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.bankname))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your BankName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.address))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your CompanyAddress."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.companyname))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your CompanyName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.description))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply Description of the commercial activity."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.workingdays))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your WorkingDays."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.workinghours))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your WorkingHours."));
                IsValid = false;
            }
            //TODO
            return (ValidationErrors.Count == 0);
        }
        public bool Validate2(View_Shipper entity)
        {
            ValidationErrors.Clear();
            
            if (string.IsNullOrEmpty(entity.comment))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your comment."));
                IsValid = false;
            }
            //TODO
            return (ValidationErrors.Count == 0);
        }
        public bool Insert(View_Shipper entity)
        {
            bool ret = true;
          //  ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //var UserIDI = db.tbl_users.Where(x => x.username == UserId).FirstOrDefault();
                    ////UserIDI.id = 1;
                   
                    //entity.userid = 1;
                    //entity.location = SeletedCountry;
                    //entity.statusid = 1;
                    //entity.dateadded = DateTime.Now;
                    //db.tbl_shipper.Add(entity);
                    //db.SaveChanges();
                    IsValid = true;
                    string op = "Registration completed, request is pending verification";
                    Msg = op;
                   
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
                var sta = (from s in db.tbl_servicetype where s.isdeleted == false orderby s.id ascending select s.service).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
                }

                var Countrysta = (from s in db.tbl_counntries select s.CountryName).Distinct();
                foreach (var bn in Countrysta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    CountryList.Add(item);
                }
            }
        }
        protected tbl_shipper GetPOSData()
        {
            tbl_shipper ret = new tbl_shipper();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_shipper.Where(x => x.id == UsrReq.id).SingleOrDefault();
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
        protected List<View_Shipper> CreateData()
        {
            List<View_Shipper> ret = new List<View_Shipper>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.View_Shipper.Where(x=>x.roleid!=1 && x.statusid == 1).ToList();
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
