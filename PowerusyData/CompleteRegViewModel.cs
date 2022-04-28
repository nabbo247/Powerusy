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
    public class CompleteRegViewModel : ViewModelBase
    {
        public CompleteRegViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_shipper();
            UsrLst = new List<tbl_shipper>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new tbl_shipper();
            Entity = new tbl_shipper();
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
        public bool IsStep1 { get; set; }
        public bool IsStep2 { get; set; }
        public bool IsStep3 { get; set; }
        public List<tbl_shipper> UsrLst { get; set; }
        public tbl_shipper SearchEntity { get; set; }
        public tbl_shipper Entity { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public HttpPostedFileBase CertificateOfIncorporation { get; set; }
        public HttpPostedFileBase MemorandumofAssociation { get; set; }
        public HttpPostedFileBase ArticlesofAssociation { get; set; }
        public HttpPostedFileBase PowerofAttorney { get; set; }
        public HttpPostedFileBase Validbusinesslicense { get; set; }
        public HttpPostedFileBase Auditedfinancial { get; set; }
        public HttpPostedFileBase Taxclearance { get; set; }
        public HttpPostedFileBase Others { get; set; }
        public string uploadedImageName { get; set; }
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
            UsrLst = new List<tbl_shipper>();
            SearchEntity = new tbl_shipper();
            Entity = new tbl_shipper();
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
            Entity = new tbl_shipper();
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
            Entity = new tbl_shipper();
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
            SearchEntity = new tbl_shipper();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<tbl_shipper> Get(tbl_shipper entity)
        {
            List<tbl_shipper> ret = new List<tbl_shipper>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.companyname))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_shipper.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                ret = ret.FindAll(
                  p => p.companyname.ToLower().
                        StartsWith(entity.companyname.ToLower().Trim(),
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            if (!string.IsNullOrEmpty(UserId))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_shipper.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();
                int uid = Convert.ToInt32(UserId);
                //}
                Entity = ret.Where(x=>x.userid==uid).FirstOrDefault();
            }
            return ret;
        }

        public tbl_shipper Get(int ReqID)
        {
            List<tbl_shipper> ret =
              new List<tbl_shipper>();
            tbl_shipper entity = null;
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

        public bool Update(tbl_shipper entity)
        {
            bool ret = false;
            ret = Validate(entity);
            GetDropDown();
            string op = "";

            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_shipper where info.id == UsrReq.id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;

                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return ret;
        }

        public bool Delete(tbl_shipper entity)
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

        public bool Insert(tbl_shipper entity)
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
                    tbl_importation_document at = new tbl_importation_document();
                    if (!string.IsNullOrEmpty(CertificateOfIncorporationName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Certificate Of Incorporation";
                        at.documentpath = CertificateOfIncorporationName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(MemorandumofAssociationName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Memorandum of Association";
                        at.documentpath = MemorandumofAssociationName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ArticlesofAssociationName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Articles of AssociationName";
                        at.documentpath = ArticlesofAssociationName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(PowerofAttorneyName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Power of AttorneyName";
                        at.documentpath = PowerofAttorneyName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ValidbusinesslicenseName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Valid business licenseName";
                        at.documentpath = ValidbusinesslicenseName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(AuditedfinancialName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Audited financialName";
                        at.documentpath = AuditedfinancialName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(TaxclearanceName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Tax clearanceName";
                        at.documentpath = TaxclearanceName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    if (Others != null && !string.IsNullOrEmpty(Others.FileName))
                    {
                        at.dateadded = DateTime.Now;
                        at.documentname = "Others";
                        at.documentpath = OthersName;
                        at.statusid = 1;
                        at.importationid = userID.ToString();
                        db.tbl_importation_document.Add(at);
                    }
                    
                    entity.userid = userID;
                    entity.location = SeletedCountry;
                    entity.statusid = 1;
                    entity.dateadded = DateTime.Now;
                    db.tbl_shipper.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "Registration completed, request is pending verification";
                    Msg = op;
                    IsStep3 = true;
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
                var sta = (from s in db.tbl_servicetype where s.isdeleted ==false orderby s.id ascending select s.service).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
                }

                var Countrysta = (from s in db.tbl_counntries  select s.CountryName).Distinct();
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
        protected List<tbl_shipper> CreateData()
        {
            List<tbl_shipper> ret = new List<tbl_shipper>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_shipper.ToList();
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
