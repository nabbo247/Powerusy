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
            SearchEntity = new tbl_shipper();
            Entity = new tbl_shipper();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_shipper UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_shipper> UsrLst { get; set; }
        public tbl_shipper SearchEntity { get; set; }
        public tbl_shipper Entity { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public bool EbizApproval { get; set; }
        protected override void Init()
        {
            UsrLst = new List<tbl_shipper>();
            SearchEntity = new tbl_shipper();
            Entity = new tbl_shipper();
            List = new List<SelectList>();
            GetDropDown();
            base.Init();
        }

        public override void HandleRequest()
        {
            //// This is an example of adding on a new command
            //switch (EventCommand.ToLower())
            //{
            //    case "newcommand":
            //        break;
            //}
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
            if (string.IsNullOrEmpty(entity.location))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your AccountNumber."));
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
            //if (string.IsNullOrEmpty(entity.description))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your Description."));
            //    IsValid = false;
            //}
            //if (string.IsNullOrEmpty(entity.PostAddress))
            //{
            //    ValidationErrors.Add(new
            //      KeyValuePair<string, string>("Comment",
            //      "Please Supply your PostAddress."));
            //    IsValid = false;
            //}
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
                    //Gadget ency = new Gadget();
                    ////string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                    //string passwd = ency.enkrypt(entity.Password);
                    //entity.Password = passwd;
                    //entity.username = entity.Email;
                    //entity.RoleID = 1;
                    //TODO: Create Insert Code here
                    db.tbl_shipper.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "New profile created Successfully";
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
                var sta = (from s in db.tbl_servicetype where s.isdeleted ==false orderby s.id ascending select s.service).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
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
