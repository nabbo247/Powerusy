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
    public class ManagePortsViewModels : ViewModelBase
    {
        public ManagePortsViewModels()
          : base()
        {
            // Initialize other variables 
            buizcat = new tbl_sea_ports();
            BuizCatL = new List<tbl_sea_ports>();
            List = new List<SelectList>();
            SearchEntity = new tbl_sea_ports();
            Entity = new tbl_sea_ports();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_sea_ports buizcat { get; set; }
        public List<SelectList> List { set; get; }
        public List<tbl_sea_ports> BuizCatL { get; set; }
        public tbl_sea_ports SearchEntity { get; set; }
        public tbl_sea_ports Entity { get; set; }

        protected override void Init()
        {
            BuizCatL = new List<tbl_sea_ports>();
            SearchEntity = new tbl_sea_ports();
            Entity = new tbl_sea_ports();
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
            //Entity = new tbl_sea_ports();
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
        //    Entity = new tbl_sea_ports();
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
            SearchEntity = new tbl_sea_ports();
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
        public List<tbl_sea_ports> Get(tbl_sea_ports entity)
        {
            List<tbl_sea_ports> ret = new List<tbl_sea_ports>();
            // TODO: Add your own data access method here
            ret = CreateData();
            // Do any searching
            if (!string.IsNullOrEmpty(entity.name))
            {
                ret = ret.FindAll(
                  p => p.name.ToLower().
                        StartsWith(entity.name,
                                  StringComparison.CurrentCultureIgnoreCase));
            }
            return ret;
        }

        public tbl_sea_ports Get(string LogID)
        {
            List<tbl_sea_ports> ret =
              new List<tbl_sea_ports>();
            tbl_sea_ports entity = null;
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
        protected tbl_sea_ports GetPOSData()
        {
            tbl_sea_ports ret = new tbl_sea_ports();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.tbl_sea_ports.Where(x => x.ID == buizcat.ID).SingleOrDefault();
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
        public bool Update(tbl_sea_ports entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    string op = " Updated Successfully";
                    var rs = (from info in db.tbl_sea_ports
                              where info.ID == entity.ID
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

        public bool Delete(tbl_sea_ports entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return true;
        }

        public bool Validate(tbl_sea_ports entity)
        {
            ValidationErrors.Clear();
            if (!string.IsNullOrEmpty(entity.name))
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

        public bool Insert(tbl_sea_ports entity)
        {
            bool ret = false;
            ret = Validate(entity);
            if (ret)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    var rs = (from info in db.tbl_sea_ports
                              where info.name == entity.name && info.city == entity.city
&& info.country == entity.country && info.province == entity.province
                              select info).FirstOrDefault();
                    if (rs != null)
                    {
                        ValidationErrors.Add(new KeyValuePair<string, string>("Comment", "already exist."));
                        IsValid = false;
                        return ret;
                    }
                   
                    entity.name = entity.name;
                    entity.city = entity.city;
                    entity.country = entity.country;
                    entity.province = entity.province;
                    entity.timezone = entity.timezone;
                    entity.code = entity.code;
                    //TODO: Create Insert Code here
                    db.tbl_sea_ports.Add(entity);
                    db.SaveChanges();
                    IsValid = true;
                    string op = "New port created Successfully";
                    Msg = op;
                    //SendMail(entity);
                }
            }
            return ret;
        }



        protected List<tbl_sea_ports> CreateData()
        {
            List<tbl_sea_ports> ret = new List<tbl_sea_ports>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    ret = db.tbl_sea_ports.ToList();
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
