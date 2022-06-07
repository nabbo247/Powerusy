using PagedList;
using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PowerusyData
{
    public class BiddingViewModel : ViewModelBase
    {
        public BiddingViewModel()
          : base()
        {
            // Initialize other variables
            UsrReq = new View_tbl_bidding();
            UsrLst = new List<View_tbl_bidding>();
            List = new List<SelectList>();
            CountryList = new List<SelectList>(); 
            ListPOL = new List<SelectList>();
            ListPOD = new List<SelectList>();
            SearchEntity = new View_tbl_bidding();
            Entity = new tbl_bidding();
            bidjos = new List<View_bid_jobs>();
            usrs = new tbl_users();
            ValidationErrors = new List<KeyValuePair<string, string>>();
        }
        public string ActionTypeId { get; set; }
        public string SeletedCountry { get; set; }
        public IPagedList PageList { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public View_tbl_bidding UsrReq { get; set; }
        public List<View_bid_jobs> bidjos { get; set; }
        public string ConfirmPassword { get; set; }
        public List<SelectList> ListPOL { get; set; }
        public List<SelectList> ListPOD { get; set; }
        public List<SelectList> List { set; get; }
        public List<SelectList> CountryList { set; get; }
        public bool IsStep1 { get; set; }
        public bool IsStep2 { get; set; }
        public bool IsStep3 { get; set; }
        public List<View_tbl_bidding> UsrLst { get; set; }
        public Dictionary<int, int> BiddingsApplied { get; set; }
        public View_tbl_bidding SearchEntity { get; set; }
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
            bidjos = new List<View_bid_jobs>();
            UsrLst = new List<View_tbl_bidding>();
            BiddingsApplied = new Dictionary<int, int>();
            SearchEntity = new View_tbl_bidding();
            Entity = new tbl_bidding();
            usrs = new tbl_users();
            List = new List<SelectList>();
            CountryList = new List<SelectList>();
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
            Entity = Get(Convert.ToInt32(EventArgument));
            ItemPixName = Entity.IconPath;

            foreach (var document in Entity.tbl_importation_document)
            {
                var oldPath = document.documentpath;

                //if file is pdf convert file to base64
                //if (Path.GetExtension(oldPath) == ".pdf")
                //{
                //    oldPath = Utility.ReadFileAsBase64String(oldPath);
                //}

                switch (document.documentname)
                {
                    case Utility.BillOfLadingName:
                        BillofLadingName = oldPath;
                        break;
                    case Utility.PackagingListName:
                        PackagingListsName = oldPath;
                        break;
                    case Utility.ProformaInvoiceName:
                        ProformainvoiceName = oldPath;
                        break;
                    case Utility.OthersName:
                        OthersName = oldPath;
                        break;
                    case Utility.ItemPixName:
                        ;
                        break;
                }
            }

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
            SearchEntity = new View_tbl_bidding();
            base.ResetSearch();
        }

        protected override void Get()
        {
            UsrLst = Get(SearchEntity);
            base.Get();
        }

        public List<View_tbl_bidding> Get(View_tbl_bidding entity)
        {
            List<View_tbl_bidding> ret = new List<View_tbl_bidding>();
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
            List<View_tbl_bidding> ret =
              new List<View_tbl_bidding>();
            tbl_bidding entity = new tbl_bidding();
            // TODO: Add data access method here

            ret = CreateData();
            
            UsrReq.id = ReqID;
            UsrReq = GetPOSData();
            if (ReqID > 0)
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    entity = db.tbl_bidding.Where(x => x.id == ReqID).FirstOrDefault();
                }
            }
                
            if(ret.Count>0)
             usrs = GetUsrData(ret[0].UserID);

            GetDropDown();
            // Find the specific product
            //entity = ret.Find(p =>
            //   p.MccCode == MccCode);
            return entity;
        }

        public bool Update(tbl_bidding entity)
        {

            try
            {
                bool ret = false;
                ret = Validate(entity);
                if (ret)
                {
                    using (var db = new powerusyDBCoreEntities())
                    {

                        var arg = Convert.ToInt32(EventArgument);
                        var isEntityValid = db.tbl_bidding.Any(x => x.id == arg);

                        if (!isEntityValid)
                        {
                            ValidationErrors.Add(new KeyValuePair<string, string>("NotFound", "Invalid or Nonexistent Bidding..."));
                            return ValidationErrors.Count == 0;
                        }

                        Entity.id = arg;
                        //Get retrieve previously uploaded documents for this bidding
                        var importationDocuments = db.tbl_importation_document.Where(t => t.bid_id == arg).ToList();

                        var documents = new List<string> { Utility.BillOfLadingName, Utility.PackagingListName, Utility.ProformaInvoiceName, Utility.OthersName };


                        //Get document for modified bidding
                        //Get document path in temp variable
                        //Update the document path for each document in entityToEdit.tbl_importation_document
                        //Save new document
                        //Execute: db.Entry(importation_document instance).State = EntityState.Modified; for each modified document
                        //Update modified bidding props in entityToEdit
                        //Execute db.Entry(entityToEdit).State = EntityState.Modified; for modified entity to update
                        //Call save changes.

                        var oldUploadedFileNames = new List<string>();

                        // Don't use for each loop on this list to avoid the exception:
                        // "Collection was modified enumeration operation may not execute".
                        for (var i = 0; i < importationDocuments.Count; i++)
                        {
                            var oldPath = importationDocuments[i].documentpath;
                            oldUploadedFileNames.Add(oldPath);
                            switch (importationDocuments[i].documentname)
                            {
                                case Utility.BillOfLadingName:
                                    Utility.SaveAttachment(string.Empty, BillofLading, out var billOfLadingFileName);
                                    if (!string.IsNullOrEmpty(billOfLadingFileName))
                                    {
                                        BillofLadingName = billOfLadingFileName;
                                        importationDocuments[i].documentpath = billOfLadingFileName;
                                        db.Entry(importationDocuments[i]).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        if (BillofLading == null && string.IsNullOrEmpty(BillofLadingName))
                                        {
                                            importationDocuments[i].documentpath = string.Empty;
                                            db.tbl_importation_document.Remove(importationDocuments[i]);
                                        }
                                    }
                                    break;
                                case Utility.PackagingListName:
                                    Utility.SaveAttachment(string.Empty, PackagingLists, out var packagingListFileName);
                                    if (!string.IsNullOrEmpty(packagingListFileName))
                                    {
                                        PackagingListsName = packagingListFileName;
                                        importationDocuments[i].documentpath = packagingListFileName;
                                        db.Entry(importationDocuments[i]).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        if (PackagingLists == null && string.IsNullOrEmpty(PackagingListsName))
                                        {
                                            importationDocuments[i].documentpath = string.Empty;
                                            db.tbl_importation_document.Remove(importationDocuments[i]);
                                        }
                                    }
                                    break;
                                case Utility.ProformaInvoiceName:
                                    Utility.SaveAttachment(string.Empty, Proformainvoice, out var invoiceFileName);
                                    if (!string.IsNullOrEmpty(invoiceFileName))
                                    {
                                        ProformainvoiceName = invoiceFileName;
                                        importationDocuments[i].documentpath = invoiceFileName;
                                        db.Entry(importationDocuments[i]).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        if (Proformainvoice == null && string.IsNullOrEmpty(ProformainvoiceName))
                                        {
                                            importationDocuments[i].documentpath = string.Empty;
                                            db.tbl_importation_document.Remove(importationDocuments[i]);
                                        }
                                    }
                                    break;
                                case Utility.OthersName:
                                    Utility.SaveAttachment(string.Empty, Others, out var othersFileName);
                                    if (!string.IsNullOrEmpty(othersFileName))
                                    {
                                        OthersName = othersFileName;
                                        importationDocuments[i].documentpath = othersFileName;
                                        db.Entry(importationDocuments[i]).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        if (Others == null && string.IsNullOrEmpty(OthersName))
                                        {
                                            importationDocuments[i].documentpath = string.Empty;
                                            db.tbl_importation_document.Remove(importationDocuments[i]);
                                        }
                                    }
                                    break;
                            }

                        }

                        //Add newly uploaded document to bidding if any.
                        var newDocs = documents.Where(s => !importationDocuments.Select(d => d.documentname).Contains(s)).ToList();
                        ProcessNewFilesOnEdit(newDocs, db);

                        Utility.SaveAttachment(string.Empty, ItemPix, out var itemPixFileName);
                        if (!string.IsNullOrEmpty(itemPixFileName))
                        {
                            ItemPixName = itemPixFileName;
                            Entity.IconPath = itemPixFileName;
                        }
                        else
                        {
                            if (ItemPix == null && string.IsNullOrEmpty(ItemPixName))
                            {
                                Entity.IconPath = string.Empty;
                            }
                        }

                        db.Entry(Entity).State = EntityState.Modified;
                        db.SaveChanges();

                        //Clean up:  Delete previously uploaded files
                        foreach (var oldUploadedFileName in oldUploadedFileNames)
                        {
                            Utility.DeleteFile(oldUploadedFileName, Utility.UploadDirectory);
                        }
                        IsValid = true;
                        string op = "Job order updated successfully.";
                        Msg = op;
                    }
                }

                return ret;

            }
            catch (Exception exception)
            {
                //If any saving fails, delete any uploaded file from upload folder

                var uploadFolder = Utility.UploadDirectory;

                if (!string.IsNullOrEmpty(BillofLadingName))
                    Utility.DeleteFile(BillofLadingName, uploadFolder);

                if (!string.IsNullOrEmpty(PackagingListsName))
                    Utility.DeleteFile(PackagingListsName, uploadFolder);

                if (!string.IsNullOrEmpty(OthersName))
                    Utility.DeleteFile(OthersName, uploadFolder);

                if (!string.IsNullOrEmpty(ItemPixName))
                    Utility.DeleteFile(ItemPixName, uploadFolder);
            }

            return false;
        }
        private void ProcessNewFilesOnEdit(List<string> newDocs, powerusyDBCoreEntities context)
        {
            foreach (var newDocument in newDocs)
            {
                switch (newDocument)
                {
                    case Utility.BillOfLadingName:
                        Utility.SaveAttachment(string.Empty, BillofLading, out var billOfLandingFileName);
                        if (!string.IsNullOrEmpty(billOfLandingFileName))
                        {
                            BillofLadingName = billOfLandingFileName;

                            tbl_importation_document at = new tbl_importation_document
                            {
                                bid_id = Convert.ToInt32(EventArgument),
                                dateadded = DateTime.Now,
                                documentname = Utility.BillOfLadingName,
                                documentpath = billOfLandingFileName,
                                statusid = 1
                            };
                            //string path = billOfLandingFileName;
                            //at.statusid = userID;
                            //db.tbl_importation_document.Add(at);
                            context.tbl_importation_document.Add(at);
                        }

                        break;
                    case Utility.PackagingListName:

                        Utility.SaveAttachment(string.Empty, PackagingLists, out var packagingListFileName);
                        if (!string.IsNullOrEmpty(packagingListFileName))
                        {
                            PackagingListsName = packagingListFileName;

                            tbl_importation_document at = new tbl_importation_document
                            {
                                bid_id = Convert.ToInt32(EventArgument),
                                dateadded = DateTime.Now,
                                documentname = Utility.PackagingListName,
                                documentpath = packagingListFileName,
                                statusid = 1
                            };
                            //string path = PackagingListsName;
                            //at.statusid = userID;
                            //db.tbl_importation_document.Add(at);
                            context.tbl_importation_document.Add(at);
                        }

                        break;
                    case Utility.ProformaInvoiceName:
                        Utility.SaveAttachment(string.Empty, Proformainvoice, out var invoiceFileName);
                        if (!string.IsNullOrEmpty(invoiceFileName))
                        {
                            ProformainvoiceName = invoiceFileName;

                            tbl_importation_document at = new tbl_importation_document
                            {
                                bid_id = Convert.ToInt32(EventArgument),
                                dateadded = DateTime.Now,
                                documentname = Utility.ProformaInvoiceName,
                                documentpath = invoiceFileName,
                                statusid = 1
                            };
                            //string path = ProformainvoiceName;
                            //at.statusid = userID;
                            //db.tbl_importation_document.Add(at);
                            context.tbl_importation_document.Add(at);
                        }

                        break;
                    case Utility.OthersName:
                        Utility.SaveAttachment(string.Empty, Others, out var othersFileName);
                        if (!string.IsNullOrEmpty(othersFileName))
                        {
                            OthersName = othersFileName;

                            tbl_importation_document at = new tbl_importation_document
                            {
                                bid_id = Convert.ToInt32(EventArgument),
                                dateadded = DateTime.Now,
                                documentname = Utility.OthersName,
                                documentpath = othersFileName,
                                statusid = 1
                            };
                            //string path = OthersName;
                            //at.statusid = userID;
                            //db.tbl_importation_document.Add(at);
                            context.tbl_importation_document.Add(at);
                        }

                        break;
                }
            }
        }

        public bool Delete(tbl_bidding entity)
        {
            using (var db = new powerusyDBCoreEntities())
            {
                var fileNames = entity.tbl_importation_document.Select(s => s.documentpath).ToList();
                db.Entry(entity).State = EntityState.Deleted;
                db.SaveChanges();

                //Clear corresponding document files for deleted bidding
                foreach (var fileName in fileNames)
                {
                    Utility.DeleteFile(fileName, Utility.UploadDirectory);
                }
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
            if ((entity.EstBudget<=0))
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
            if (entity.PortDischarge==0)
            {
                ValidationErrors.Add(new
                  KeyValuePair<string, string>("Comment",
                  "Please Supply your Port of Discharge."));
                IsValid = false;
            }
            if (entity.PortLoading==0)
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

            Utility.ValidateAttachment(BillofLading, "Bill Of Landing", ValidationErrors);
            Utility.ValidateAttachment(Proformainvoice, "Proforma Invoice", ValidationErrors);

            //TODO
            return (ValidationErrors.Count == 0);
        }

        public bool Insert(tbl_bidding entity)
        {
            try
            {
                bool ret = false;
                ret = Validate(entity);
                if (ret)
                {
                    using (var db = new powerusyDBCoreEntities())
                    {
                        var UserIDI = db.tbl_users.FirstOrDefault(x => x.username == UserId);
                        var importationDocs = new List<tbl_importation_document>();
                        //UserIDI.id = 1;
                        int userID = UserIDI.id;

                        ProcessAttachmentOnAdd(entity, importationDocs);
                        entity.UserID = userID;
                        entity.GoodsType = ActionTypeId;
                        entity.statusid = 1;
                        entity.Date = DateTime.Now;

                        if (importationDocs.Count > 0)
                        {
                            entity.tbl_importation_document = importationDocs;
                        }

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
            catch (Exception exception)
            {
                //If any saving fails, delete any uploaded file from upload folder

                var uploadFolder = Utility.UploadDirectory;

                if (!string.IsNullOrEmpty(BillofLadingName))
                    Utility.DeleteFile(BillofLadingName, uploadFolder);

                if (!string.IsNullOrEmpty(PackagingListsName))
                    Utility.DeleteFile(PackagingListsName, uploadFolder);

                if (!string.IsNullOrEmpty(OthersName))
                    Utility.DeleteFile(OthersName, uploadFolder);

                if (!string.IsNullOrEmpty(ItemPixName))
                    Utility.DeleteFile(ItemPixName, uploadFolder);
            }

            return false;
            //bool ret = false;
            //ret = Validate(entity);
            //if (ret)
            //{
            //    using (var db = new powerusyDBCoreEntities())
            //    {
            //        var UserIDI = db.tbl_users.Where(x => x.username == UserId).FirstOrDefault();
            //        //UserIDI.id = 1;
            //        int userID = UserIDI.id;

            //        entity.IconPath = ItemPixName;
            //        entity.UserID = userID;
            //        entity.GoodsType = ActionTypeId;
            //        entity.statusid = 1;
            //        entity.Date = DateTime.Now;
            //        db.tbl_bidding.Add(entity);
            //        db.SaveChanges();

            //        if (!string.IsNullOrEmpty(BillofLadingName))
            //        {
            //            tbl_importation_document at = new tbl_importation_document();
            //            at.dateadded = DateTime.Now;
            //            at.documentname = "Bill of Lading";
            //            string path = BillofLadingName;
            //            at.documentpath = path;
            //            at.statusid = 1;
            //            at.bid_id = entity.id;
            //            db.tbl_importation_document.Add(at);
            //        }
            //        if (!string.IsNullOrEmpty(PackagingListsName))
            //        {
            //            tbl_importation_document at = new tbl_importation_document();
            //            at.dateadded = DateTime.Now;
            //            at.documentname = "Packaging Lists";
            //            string path = PackagingListsName;
            //            at.documentpath = path;
            //            at.statusid = 1;
            //            at.bid_id = entity.id;
            //            db.tbl_importation_document.Add(at);
            //        }
            //        if (!string.IsNullOrEmpty(ProformainvoiceName))
            //        {
            //            tbl_importation_document at = new tbl_importation_document();
            //            at.dateadded = DateTime.Now;
            //            at.documentname = "Proforma invoice";
            //            string path = ProformainvoiceName;
            //            at.documentpath = path;
            //            at.statusid = 1;
            //            at.bid_id = entity.id;
            //            db.tbl_importation_document.Add(at);
            //        }
            //        if (!string.IsNullOrEmpty(OthersName))
            //        {
            //            tbl_importation_document at = new tbl_importation_document();
            //            at.dateadded = DateTime.Now;
            //            at.documentname = "Others Name";
            //            string path = OthersName;
            //            at.documentpath = path;
            //            at.statusid = 1;
            //            at.bid_id = entity.id;
            //            db.tbl_importation_document.Add(at);
            //        }
            //        db.SaveChanges();
            //        IsValid = true;
            //        string op = "created job order successful";
            //        Msg = op;
            //        //IsStep3 = true;
            //    }
            //}
            //return ret;
        }
        private void ProcessAttachmentOnAdd(tbl_bidding entity, List<tbl_importation_document> importationDocs)
        {
            Utility.SaveAttachment(string.Empty, BillofLading, out var billOfLandingFileName);
            if (!string.IsNullOrEmpty(billOfLandingFileName))
            {
                BillofLadingName = billOfLandingFileName;

                tbl_importation_document at = new tbl_importation_document
                {
                    dateadded = DateTime.Now,
                    documentname = Utility.BillOfLadingName,
                    documentpath = billOfLandingFileName,
                    statusid = 1
                };
                //string path = billOfLandingFileName;
                //at.statusid = userID;
                //db.tbl_importation_document.Add(at);
                importationDocs.Add(at);
            }

            Utility.SaveAttachment(string.Empty, PackagingLists, out var packagingListFileName);
            if (!string.IsNullOrEmpty(packagingListFileName))
            {
                PackagingListsName = packagingListFileName;

                tbl_importation_document at = new tbl_importation_document
                {
                    dateadded = DateTime.Now,
                    documentname = Utility.PackagingListName,
                    documentpath = packagingListFileName,
                    statusid = 1
                };
                //string path = PackagingListsName;
                //at.statusid = userID;
                //db.tbl_importation_document.Add(at);
                importationDocs.Add(at);
            }

            Utility.SaveAttachment(string.Empty, Proformainvoice, out var invoiceFileName);
            if (!string.IsNullOrEmpty(invoiceFileName))
            {
                ProformainvoiceName = invoiceFileName;

                tbl_importation_document at = new tbl_importation_document
                {
                    dateadded = DateTime.Now,
                    documentname = Utility.ProformaInvoiceName,
                    documentpath = invoiceFileName,
                    statusid = 1
                };
                //string path = ProformainvoiceName;
                //at.statusid = userID;
                //db.tbl_importation_document.Add(at);
                importationDocs.Add(at);
            }

            Utility.SaveAttachment(string.Empty, Others, out var othersFileName);
            if (!string.IsNullOrEmpty(othersFileName))
            {
                OthersName = othersFileName;

                tbl_importation_document at = new tbl_importation_document
                {
                    dateadded = DateTime.Now,
                    documentname = Utility.OthersName,
                    documentpath = othersFileName,
                    statusid = 1
                };
                //string path = OthersName;
                //at.statusid = userID;
                //db.tbl_importation_document.Add(at);
                importationDocs.Add(at);
            }

            Utility.SaveAttachment(string.Empty, ItemPix, out var itemPixFileName);
            if (!string.IsNullOrEmpty(itemPixFileName))
            {
                entity.IconPath = ItemPixName = itemPixFileName;
            }
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
                var SeaPorts = db.tbl_sea_ports.ToList().Distinct().OrderBy(x=>x.name);
               // var SeaPorts = (from s in db.tbl_sea_ports select s).Distinct();
                foreach (var bn in SeaPorts)
                {
                    SelectList item = new SelectList();
                    item.Text = bn.name + " - " + bn.country;
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
        protected View_tbl_bidding GetPOSData()
        {
            View_tbl_bidding ret = new View_tbl_bidding();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    ret = db.View_tbl_bidding.Where(x => x.id == UsrReq.id).SingleOrDefault();
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
        public string GetSeaPort(string ID)
        {
            string name = "";
            using (var db = new powerusyDBCoreEntities())
            {
                int MID = Convert.ToInt32(ID);
                var bn = db.tbl_sea_ports.Where(x => x.ID == MID).FirstOrDefault();
                name = bn.name + " - " + bn.country;
            }
            return name;
        }
        protected List<View_tbl_bidding> CreateData()
        {
            List<View_tbl_bidding> ret = new List<View_tbl_bidding>();
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    //PosReq_vws
                    int UID = Convert.ToInt32(UserId);
                    ret = db.View_tbl_bidding.OrderByDescending(x => x.id).Where(x=>x.UserID== UID).ToList();
                    var seaList = db.tbl_sea_ports.ToList();

                    bidjos = db.View_bid_jobs.ToList();

                    var applied = db.tbl_bidding_jobs.GroupBy(x => x.BidID).Select(s => new { key = s.Key.Value, value = s.Count() });

                    foreach (var job in applied)
                    {
                        BiddingsApplied.Add(job.key, job.value);
                    }

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
