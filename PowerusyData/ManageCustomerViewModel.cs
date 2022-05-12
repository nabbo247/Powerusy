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
    public class ManageCustomerViewModel : ViewModelBase
    {
        public ManageCustomerViewModel()
          : base()
        {
            // Initialize other variables 
            usrInfo = new tbl_users();
            usrList = new List<tbl_users>();
            List = new List<SelectList>();
            SearchEntity = new tbl_users();
           
            Entity = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_users usrInfo { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_users> usrList { get; set; }

        
        public tbl_users SearchEntity { get; set; }
        public tbl_users Entity { get; set; }

        protected override void Init()
        {
            usrList = new List<tbl_users>();
            SearchEntity = new tbl_users();
            Entity = new tbl_users();
            List = new List<SelectList>();
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
            base.HandleRequest();
        }

        protected override void Add()
        {
            IsValid = true;
            Entity = Get(EventArgument);
            // Initialize Entity Object
            //Entity = new tbl_users();
            base.Add();
        }

        protected override void Edit()
        {
            // Get Product Data
            Entity = Get(EventArgument);
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

        //protected override void Delete()
        //{
        //    // Create new entity
        //    Entity = new tbl_users();
        //    // Get primary key from EventArgument
        //    Entity.BusCatName = EventArgument.Trim();
        //    // Convert.ToInt32(EventArgument);
        //    // Call data layer to delete record
        //    Delete(Entity);
        //    // Reload the Data
        //    Get();
        //    base.Delete();
        //}

        protected override void ResetSearch()
        {
            SearchEntity = new tbl_users();
            base.ResetSearch();
        }

        protected override void Get()
        {
            usrList = Get(SearchEntity);
            base.Get();
        }
        //new 
        //public List<PosReq_vw> Get()
        //{
        //    return Get(new PosReq_vw());
        //}
        public List<tbl_users> Get(tbl_users entity)
        {
            List<tbl_users> ret = new List<tbl_users>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.email))
            {
                ret = ret.FindAll(
                  p => p.email.ToLower().
                        StartsWith(entity.email,
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_users Get(string LogID)
        {
            List<tbl_users> ret =
              new List<tbl_users>();
            tbl_users entity = null;
            // TODO: Add data access method here
            ret = CreateData();
            //GetDropDown();
            usrInfo.id = Convert.ToInt32(LogID);
            Entity = GetPOSData();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return Entity;
        }
        protected tbl_users GetPOSData()
        {
            tbl_users ret = new tbl_users();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_users.Where(x => x.id == usrInfo.id).SingleOrDefault();
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
        public bool Update(tbl_users entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    string op = " Updated Successfully";
                    var rs = (from info in db.tbl_users where info.id == entity.id select info).FirstOrDefault();
                    //rs.TermOwnerCode = TextBox6.Text;

                    db.Entry(rs).State = EntityState.Modified;
                    db.SaveChanges();
                    // op += ". from " + " " + HidPrev.Value + " to " + " " + txtcatName.Text;
                    ldp.writelog(UserId, op, "IP");
                    Msg = op;
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
            if (!string.IsNullOrEmpty(entity.email))
            {
                //if (entity.MccName.ToLower() ==
                //    entity.MccName)
                //{
                //    ValidationErrors.Add(new
                //      KeyValuePair<string, string>("MccName",
                //      "MccName must not be all lower case."));
                //}
            }
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
                    var rs = (from info in db.tbl_users where info.email == entity.email select info).FirstOrDefault();
                    if (rs != null)
                    {
                        ValidationErrors.Add(new KeyValuePair<string, string>("Comment", "User already exist."));
                        IsValid = false;
                        return ret;
                    }
                    //if (!string.IsNullOrEmpty(_FileName))
                    //{
                    //    entity.Logo = _FileName;
                    //}
                    Gadget ency = new Gadget();
                    //string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                    string passwd = ency.enkrypt(entity.password);
                    entity.password = passwd;
                    entity.username = entity.email;
                    entity.dateadded = DateTime.Now;
                    Random rm = new Random();
                    entity.authcode = rm.Next(100009, 999999).ToString();
                    entity.roleid = Convert.ToInt32(ActionTypeId);
                    //TODO: Create Insert Code here
                    db.tbl_users.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "New profile created Successfully";
                    Msg = op;
                    //SendMail(entity);
                }
            }
            return ret;
        }

        //protected void GetDropDown()
        //{
        //    using (var db = new POSdataDataContext())
        //    {
        //        var rsfault = from p in db.FaultCategoryTabs select p;

        //        foreach (var bn in rsfault)
        //        {
        //            SelectList item = new SelectList();
        //            item.Text = bn.FaultCategory;
        //            item.Value = Convert.ToString(bn.sn);
        //            List.Add(item);
        //        }
        //    }
        //}

        protected List<tbl_users> CreateData()
        {
            List<tbl_users> ret = new List<tbl_users>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_users.Where(x=>x.roleid==2).ToList();
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
