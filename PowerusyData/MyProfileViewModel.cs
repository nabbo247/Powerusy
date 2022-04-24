
using PagedList;
using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PowerusyData
{
    public class MyProfileViewModel : ViewModelBase
    {
        public MyProfileViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_users();
            UsrLst = new List<tbl_users>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new tbl_users();
            Entity = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_users UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool IsStep1 { get; set; }
        public bool IsStep2 { get; set; }
        public bool IsStep3 { get; set; }
        public List<tbl_users> UsrLst { get; set; }
        public tbl_users SearchEntity { get; set; }
        public tbl_users Entity { get; set; }
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
        public string NewPassword { get; set; }
        public string Password { get; set; }
        protected override void Init()
        {
            UsrLst = new List<tbl_users>();
            SearchEntity = new tbl_users();
            Entity = new tbl_users();
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
            Entity = new tbl_users();
            Entity.firstname = string.Empty;
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
                //    ret = db.tbl_users.Where(x => x.AcctName.Contains(entity.AcctName)).OrderBy(x => x.ReqDate).ToList();

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
            //if (ret.Count > 0)
            //    usrs = GetUsrData(ret[0].UserID);

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
                    Gadget cyCrypto = new Gadget();
                    var decrypted = cyCrypto.Decrypt(rs.password);
                    if (Password.Trim() != decrypted)
                    {
                        ValidationErrors.Add(new KeyValuePair<string, string>("Password", "Your old Password did not match"));
                        return ret;
                    }
                    rs.password = cyCrypto.enkrypt(rs.password);
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
            if (!string.IsNullOrEmpty(NewPassword))
            {
                if(!Regex.IsMatch(NewPassword.Trim(), "((?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})"))
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Password", "A valid Password MUST contain a combination of atleast one Uppercase, Lowercase, Alphanumeric character and be between 6-20 characters"));
            }
            if (string.IsNullOrEmpty(Password))
            {
                ValidationErrors.Add(new KeyValuePair<string, string>("Password", " Old Password cannot be empty"));
            }
            if (string.IsNullOrEmpty(NewPassword))
            {
                ValidationErrors.Add(new KeyValuePair<string, string>("NewPassword", "New Password cannot be empty"));
            }
            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                ValidationErrors.Add(new KeyValuePair<string, string>("ConfirmPassword", " Confirm Password Cannot be empty"));
            }
            if (NewPassword != ConfirmPassword)
            {
                ValidationErrors.Add(new KeyValuePair<string, string>("ConfirmPassword", "New Password confilcts with Confirm Password, try again"));
            }
            if (!string.IsNullOrEmpty(NewPassword))
            {
                int ab = CheckStrength(NewPassword);
                if (ab <= 4)
                {
                    ValidationErrors.Add(new KeyValuePair<string, string>("Password", "Please enter a strong password"));
                }
            }
            
            
            //TODO
            return (ValidationErrors.Count == 0);
        }
        public int CheckStrength(string password)
        {
            int score = 1;
            if (password.Length < 1)
            {
                return 0;
            }
            if (password.Length < 4)
            {
                return 1;
            }
            if (password.Length >= 6)
            {
                score++;
            }
            if (password.Length >= 12)
            {
                score++;
            }
            if (Regex.IsMatch(password, "\\d+"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            if (Regex.IsMatch(password, "[!@#\\$%\\^&\\*\\?_~\\-\\(\\);\\.\\+:]+"))
            {
                score++;
            }
            return score;
        }
        public bool Insert(tbl_users entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var UserIDI = db.tbl_users.Where(x => x.username == UserId).FirstOrDefault();
                    //UserIDI.id = 1;
                    
                    db.tbl_users.Add(entity);
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