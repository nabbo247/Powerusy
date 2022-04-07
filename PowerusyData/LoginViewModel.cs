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

    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_users();
            UsrLst = new List<tbl_users>();
            List = new List<SelectList>();
            SearchEntity = new tbl_users();
            Entity = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_users UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_users> UsrLst { get; set; }
        public tbl_users SearchEntity { get; set; }
        public tbl_users Entity { get; set; }
        public HttpPostedFileBase uploadedImage { get; set; }
        public bool EbizApproval { get; set; }
        protected override void Init()
        {
            UsrLst = new List<tbl_users>();
            SearchEntity = new tbl_users();
            Entity = new tbl_users();
            List = new List<SelectList>();
            GetDropDown();
            base.Init();
        }

        public override void HandleRequest()
        {
            //// This is an example of adding on a new command

            //GetDropDown();
            //base.HandleRequest();
            switch (Mode.ToLower())
            {
                case "validate":
                    try
                    {
                        bool ret = false;
                        ret = ValidateUsr(Entity);
                        string op = "";

                        if (ret)
                        {
                            Gadget ency = new Gadget();
                            //string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                            string passwd = ency.enkrypt(Entity.password);

                            using (var db = new powerusyDBCoreEntities())
                            {
                                var rs = (from info in db.tbl_users
                                          where info.username == Entity.username 
                                          select info).FirstOrDefault();
                                if (rs != null && rs.password == passwd)
                                {
                                    IsValid = true;
                                    Entity = rs;
                                }
                                else
                                {
                                    ValidationErrors.Add(new
                                      KeyValuePair<string, string>("Comment",
                                      "Invalid User, Check your username and password"));
                                    IsValid = false;
                                }
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    break;
            }
            
        }

        protected override void Add()
        {
            IsValid = true;
            // Initialize Entity Object
            Entity = new tbl_users();
            Entity.email = string.Empty;
            Entity.firstname = string.Empty;
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
            Entity = new tbl_users();
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
            SearchEntity = new tbl_users();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<tbl_users> Get(tbl_users entity)
        {
            List<tbl_users> ret = new List<tbl_users>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.firstname))
            {
                //using (var db = new powerusyDBCoreEntities())
                //{
                //    ret = db.tbl_user.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

                //}
                ret = ret.FindAll(
                  p => p.firstname.ToLower().
                        StartsWith(entity.firstname.ToLower().Trim(),
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_users Get(int ReqID)
        {
            List<tbl_users> ret =
              new List<tbl_users>();
            tbl_users entity = null;
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

        public bool Update(tbl_users entity)
        {
            bool ret = false;
            ret = Validate(entity);
            GetDropDown();
            string op = "";

            if (ret)
            {

                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_users where info.id == UsrReq.id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;

                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return ret;
        }

        public bool Delete(tbl_users entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_users entity)
        {
            ValidationErrors.Clear();
            if (string.IsNullOrEmpty(entity.firstname))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your FirstName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.lastname))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your LastName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.email))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Email."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.phonenumber))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your PhoneNumber."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.password))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Password."));
                IsValid = false;
            }
            if (!string.IsNullOrEmpty(entity.password) && ConfirmPassword.Trim() != entity.password)
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Confirm Password and password is not the same."));
                IsValid = false;
            }
            //TODO
            return (ValidationErrors.Count == 0);
        }
        public bool ValidateUsr(tbl_users entity)
        {
            ValidationErrors.Clear();
            
            if (string.IsNullOrEmpty(entity.username) || string.IsNullOrEmpty(entity.password))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Invalid User, Check your username and password"));
                IsValid = false;
            }
            
            //TODO
            return (ValidationErrors.Count == 0);
        }
        public bool Insert(tbl_users entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    Gadget ency = new Gadget();
                    //string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                    string passwd = ency.enkrypt(entity.password);
                    entity.password = passwd;
                    entity.username = entity.email;
                    entity.roleid = 1;
                    //TODO: Create Insert Code here
                    db.tbl_users.Add(entity);
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
                var sta = (from s in db.tbl_role where s.id > 1 orderby s.status ascending select s.name).Distinct();
                foreach (var bn in sta)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    List.Add(item);
                }
            }
        }
        protected tbl_users GetPOSData()
        {
            tbl_users ret = new tbl_users();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_users.Where(x => x.id == UsrReq.id).SingleOrDefault();
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
        protected List<tbl_users> CreateData()
        {
            List<tbl_users> ret = new List<tbl_users>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_users.ToList();
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
