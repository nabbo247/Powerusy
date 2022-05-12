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
    public class SearchjobViewModel : ViewModelBase
    {
        public SearchjobViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new tbl_bidding();
            UsrLst = new List<tbl_bidding>();
            List = new List<SelectList>();
            ListVehileType = new List<SelectList>();
            ListMake = new List<SelectList>();
            ListModel = new List<SelectList>();
            ListYear = new List<SelectList>();
            ListPOL = new List<SelectList>();
            ListPOD = new List<SelectList>();
            CountryList = new List<SelectList>();
            SearchEntity = new tbl_bidding();
            Entity = new tbl_bidding();
            usrs = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }

        
        public List<SelectList> GetModel(string state)
        {
            List<SelectList> Item = new List<SelectList>();
            using (var db = new powerusyDBCoreEntities())
            {
                //var lg = from g in db.LGAs where g.StateName == state select g.LGA1;
                //foreach (var bn in lg)
                //{
                //    SelectList item = new SelectList();
                //    item.Text = bn;
                //    item.Value = bn;
                //    Item.Add(item);
                //}
                var Model = db.tbl_cars_models.Where(x=>x.CarMake == state).Select(s => s.Model).ToList().Distinct().OrderBy(x => x);
                //var Model = (from s in db.tbl_cars_models orderby s.Model select s.Model).Distinct();
                foreach (var bn in Model)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    Item.Add(item);
                }
            }
            return Item;
        }
        public string ActionTypeId { get; set; }
        public string VechileType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }

        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public tbl_bidding UsrReq { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> ListVehileType { set; get; }
        public List<SelectList> ListPOL { set; get; }
        public List<SelectList> ListPOD { set; get; }
        public List<SelectList> ListMake { set; get; }
        public List<SelectList> ListModel { set; get; }
        public List<SelectList> ListYear { set; get; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool GridView { get; set; }
        public bool ListView { get; set; }
        public bool IsStep3 { get; set; }
        public List<tbl_bidding> UsrLst { get; set; }
        public tbl_bidding SearchEntity { get; set; }
        public tbl_bidding Entity { get; set; }
        public tbl_users usrs { get; set; }
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
        protected override void Init()
        {
            UsrLst = new List<tbl_bidding>();
            SearchEntity = new tbl_bidding();
            Entity = new tbl_bidding();
            usrs = new tbl_users();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
            ListVehileType = new List<SelectList>();
            ListMake = new List<SelectList>();
            ListModel = new List<SelectList>();
            ListYear = new List<SelectList>();
            ListPOL = new List<SelectList>();
            ListPOD = new List<SelectList>();
            GetDropDown();
            base.Init();
        }

        public override void HandleRequest()
        {
            //// This is an example of adding on a new command
            switch (EventCommand.ToLower())
            {
                case "listview":
                    ListView = true;
                    Get();
                    break;
                case "gridview":
                    GridView = true;
                    Get();
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
            if (ret.Count > 0)
                usrs = GetUsrData(ret[0].UserID);

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
            if (string.IsNullOrEmpty(entity.VesselName))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your VesselName."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.BookingNo))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your BookingNo."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.BLNumber))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your BLNumber."));
                IsValid = false;
            }
            if ((entity.EstBudget <= 0))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply Est Budget."));
                IsValid = false;
            }

            if (string.IsNullOrEmpty(entity.Consignee))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Consignee."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.PortDischarge))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Port of Discharge."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.PortLoading))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Port of Loading."));
                IsValid = false;
            }
            if (string.IsNullOrEmpty(entity.GoodDescription))
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Good Description."));
                IsValid = false;
            }
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
                    if (!string.IsNullOrEmpty(BillofLadingName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Bill of Lading";
                        string path = BillofLadingName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(PackagingListsName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Packaging Lists";
                        string path = PackagingListsName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ProformainvoiceName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Proforma invoice";
                        string path = ProformainvoiceName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(OthersName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Others Name";
                        string path = OthersName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }
                    if (!string.IsNullOrEmpty(ItemPixName))
                    {
                        tbl_importation_document at = new tbl_importation_document();
                        at.dateadded = DateTime.Now;
                        at.documentname = "Item Pix";
                        string path = ItemPixName;
                        at.documentpath = path;
                        at.statusid = 1;
                        at.statusid = userID;
                        db.tbl_importation_document.Add(at);
                    }

                    entity.UserID = userID;
                    entity.GoodsType = ActionTypeId;
                    entity.statusid = 1;
                    entity.Date = DateTime.Now;
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
                var Category = db.tbl_cars_models.Select(s => s.Category).ToList().Distinct().OrderBy(x => x);
                //var Category = (from s in db.tbl_cars_models orderby s.Category select s.Category).Distinct();
                foreach (var bn in Category)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    ListVehileType.Add(item);
                }
                var CarMake = db.tbl_cars_models.Select(s => s.CarMake).ToList().Distinct().OrderBy(x => x);
                //var CarMake = db.tbl_cars_models.Select(s=>s.CarMake).ToList().Distinct().OrderBy(x=>x);
                foreach (var bn in CarMake)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    ListMake.Add(item);
                }
                var Year = db.tbl_cars_models.Select(s => s.Year).ToList().Distinct().OrderBy(x => x);
                //var Year = (from s in db.tbl_cars_models orderby s.Year select s.Year ).Distinct();
                foreach (var bn in Year)
                {
                    SelectList item = new SelectList();
                    item.Text = bn.ToString();
                    item.Value = bn.ToString();
                    ListYear.Add(item);
                }
                var Model = db.tbl_cars_models.Select(s => s.Model).ToList().Distinct().OrderBy(x => x);
                //var Model = (from s in db.tbl_cars_models orderby s.Model select s.Model).Distinct();
                foreach (var bn in Model)
                {
                    SelectList item = new SelectList();
                    item.Text = bn;
                    item.Value = bn;
                    ListModel.Add(item);
                }

                var SeaPorts = (from s in db.tbl_sea_ports  select s).Distinct();
                foreach (var bn in SeaPorts)
                {
                    SelectList item = new SelectList();
                    item.Text = bn.name +" - "+bn.country;
                    item.Value = bn.ID.ToString();
                    ListPOD.Add(item);
                }
                
                foreach (var bn in SeaPorts)
                {
                    SelectList item = new SelectList();
                    item.Text = bn.name + " - " + bn.country;
                    item.Value = bn.ID.ToString();
                    ListPOL.Add(item);
                }
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

