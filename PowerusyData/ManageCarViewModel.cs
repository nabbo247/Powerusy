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
    public class ManageCarViewModel : ViewModelBase
    {
        public ManageCarViewModel()
          : base()
        {
            // Initialize other variables 
            buizcat = new tbl_cars_models();
            BuizCatL = new List<tbl_cars_models>();
            List = new List<SelectList>();
            SearchEntity = new tbl_cars_models();
            Entity = new tbl_cars_models();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_cars_models buizcat { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_cars_models> BuizCatL { get; set; }
        public tbl_cars_models SearchEntity { get; set; }
        public tbl_cars_models Entity { get; set; }

        protected override void Init()
        {
            BuizCatL = new List<tbl_cars_models>();
            SearchEntity = new tbl_cars_models();
            Entity = new tbl_cars_models();
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
            //Entity = new tbl_cars_models();
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
        //    Entity = new tbl_cars_models();
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
            SearchEntity = new tbl_cars_models();
            base.ResetSearch();
        }

        protected override void Get()
        {
            BuizCatL = Get(SearchEntity);
            base.Get();
        }
        //new 
        //public List<PosReq_vw> Get()
        //{
        //    return Get(new PosReq_vw());
        //}
        public List<tbl_cars_models> Get(tbl_cars_models entity)
        {
            List<tbl_cars_models> ret = new List<tbl_cars_models>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.Model))
            {
                ret = ret.FindAll(
                  p => p.Model.ToLower().
                        StartsWith(entity.Model,
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_cars_models Get(string LogID)
        {
            List<tbl_cars_models> ret =
              new List<tbl_cars_models>();
            tbl_cars_models entity = null;
            // TODO: Add data access method here
            ret = CreateData();
            //GetDropDown();
            buizcat.ID = Convert.ToInt32(LogID);
            Entity = GetPOSData();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return Entity;
        }
        protected tbl_cars_models GetPOSData()
        {
            tbl_cars_models ret = new tbl_cars_models();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_cars_models.Where(x => x.ID == buizcat.ID).SingleOrDefault();
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
        public bool Update(tbl_cars_models entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    string op = " Updated Successfully";
                    var rs = (from info in db.tbl_cars_models where info.ID == entity.ID 
                              select info).FirstOrDefault();
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

        public bool Delete(tbl_cars_models entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_cars_models entity)
        {
            ValidationErrors.Clear();
            if (!string.IsNullOrEmpty(entity.Model))
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

        public bool Insert(tbl_cars_models entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_cars_models where info.Model == entity.Model && info.CarMake == entity.CarMake
                              && info.Category == entity.Category && info.Year == entity.Year 
                              select info).FirstOrDefault();
                    if (rs != null)
                    {
                        ValidationErrors.Add(new KeyValuePair<string, string>("Comment", "already exist."));
                        IsValid = false;
                        return ret;
                    }
                    //if (!string.IsNullOrEmpty(_FileName))
                    //{
                    //    entity.Logo = _FileName;
                    //}
                    Gadget ency = new Gadget();
                    //string ssss = ency.dekrypt("3H0h8gr44jrBbJ3SXaQdSQ==");
                    entity.Model = entity.Model;
                    entity.Year = entity.Year;
                    entity.CarMake = entity.CarMake;
                    entity.Category = entity.Category;
                    //TODO: Create Insert Code here
                    db.tbl_cars_models.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "New car model created Successfully";
                    Msg = op;
                    //SendMail(entity);
                }
            }
            return ret;
        }

        

        protected List<tbl_cars_models> CreateData()
        {
            List<tbl_cars_models> ret = new List<tbl_cars_models>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_cars_models.ToList();
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
