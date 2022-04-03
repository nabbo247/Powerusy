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
    public class RegistrationViewModel : ViewModelBase
    {
        public RegistrationViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_user();
            UsrLst = new List<tbl_user>();
            List = new List<SelectList>();
            SearchEntity = new tbl_user();
            Entity = new tbl_user();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_user UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_user> UsrLst { get; set; }
        public tbl_user SearchEntity { get; set; }
        public tbl_user Entity { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public bool EbizApproval { get; set; }
        protected override void Init()
        {
            UsrLst = new List<tbl_user>();
            SearchEntity = new tbl_user();
            Entity = new tbl_user();
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
            Entity = new tbl_user();
            Entity.Email = string.Empty;
            Entity.FirstName = string.Empty;
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
            Entity = new tbl_user();
            // Get primary key from EventArgument
            Entity.Id = Convert.ToInt32(EventArgument); 
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
            SearchEntity = new tbl_user();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<tbl_user> Get(tbl_user entity)
        {
            List<tbl_user> ret = new List<tbl_user>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.FirstName))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_user.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                ret = ret.FindAll(
                  p => p.FirstName.ToLower().
                        StartsWith(entity.FirstName.ToLower().Trim(),
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_user Get(int ReqID)
        {
            List<tbl_user> ret =
              new List<tbl_user>();
            tbl_user entity = null;
            // TODO: Add data access method here
            
            ret = CreateData();

            UsrReq.Id = ReqID;
            UsrReq = GetPOSData();
            if (ReqID > 0)
                ret = ret.Where(x => x.Id == ReqID).ToList();

            GetDropDown();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return ret[0] == null ? null : ret[0];
        }

        public bool Update(tbl_user entity)
        {
            bool ret = false;
            ret = Validate(entity);
            GetDropDown();
            string op = "";

            if (ret)
            {

                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_user where info.Id == UsrReq.Id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;
                   
                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
            }
            return ret;
        }
       
        public bool Delete(tbl_user entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_user entity)
        {
            ValidationErrors.Clear();
            if (string.IsNullOrEmpty(entity.FirstName))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your FirstName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.LastName))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your LastName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.Email))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Email."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.PhoneNumber))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your PhoneNumber."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.Password))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Password."));
                IsValid = false;
            }
            if (!string.IsNullOrEmpty(entity.Password) && ConfirmPassword.Trim() !=entity.Password)
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Confirm Password and password is not the same."));
                IsValid = false;
            }
            //TODO
            return (ValidationErrors.Count == 0);
        }

        public bool Insert(tbl_user entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    Gadget ency = new Gadget();
                    //string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                    string passwd = ency.enkrypt(entity.Password);
                    entity.Password = passwd;
                    entity.username = entity.Email;
                    entity.RoleID = 1;
                    //TODO: Create Insert Code here
                    db.tbl_user.Add(entity);
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
                var sta = (from s in db.tbl_role where s.Id >1 orderby s.Status ascending select s.Name).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
                }
            }
        }
        protected tbl_user GetPOSData()
        {
            tbl_user ret = new tbl_user();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_user.Where(x => x.Id == UsrReq.Id).SingleOrDefault();
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
        protected List<tbl_user> CreateData()
        {
            List<tbl_user> ret = new List<tbl_user>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_user.ToList();
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
