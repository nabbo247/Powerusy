using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerusyData
{
    public class IndexViewModel : ViewModelBase
    {
        //public IndexViewModel()
        //{
        //    sav = new IDP();
        //    _menus = new List<MenuModels>();
        //}
        //IDP sav;
        //public List<MenuModels> _menus { get; set; }
        //public List<vw_tblSubmenu> list { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public bool RememberMe { get; set; }
        //public string fname { get; set; }
        //public string sname { get; set; }
        //public string Email { get; set; }
        //public string Role { get; set; }
        //public string branch { get; set; }
        //public int Zname { get; set; }
        //public override void HandleRequest()
        //{
        //    try
        //    {
        //        //To Do insert Branches
        //        ////string url = ConfigurationManager.AppSettings["url"];
        //        ////var client2 = new RestClient(url + "/GetBranchCodes");
        //        ////var request2 = new RestRequest(Method.GET);
        //        ////request2.AddHeader("ContentType", "application/json");
        //        ////request2.RequestFormat = DataFormat.Json;
        //        ////var s2 = client2.Execute(request2);
        //        ////var Xxc2 = JsonConvert.DeserializeObject<dynamic>(s2.Content);
        //        ////var respon2 = JsonConvert.SerializeObject(Xxc2);
        //        ////var json2 = JsonConvert.DeserializeObject<NewPOSViewModel.RootBranch>(respon2);
        //        ////if (json2.Status)
        //        ////{
        //        ////    foreach (var item in json2.Data)
        //        ////    {
        //        ////        sav.SaveBranch(item.BranchName, item.BranchCOde);
        //        ////    }
        //        ////}

        //        //validate with AD             
        //        // Role Manager service

        //        //result = userValidation.ValidateADUser(logModel);

        //        using (var db = new powerusyDBCoreEntities())
        //            {
        //                list = db.vw_tblSubmenu.ToList();
        //                //This App has 4 Roles
        //                foreach (var role in result.roles)
        //                {
        //                    var RoleID = db.tbl_roles.Where(x => x.roleName == role.Trim()).Select(x => x.roleID);

        //                    if (RoleID.FirstOrDefault() == 0)
        //                        continue;
        //                    int inds = Convert.ToInt32(RoleID.First());

        //                    var _menusTemp = list.Where(x => x.RoleID == inds).Select(x => new MenuModels
        //                    {
        //                        MainMenuId = x.MenuID,
        //                        Icon = x.MenuName,
        //                        MainMenuName = x.menu_name,
        //                        SubMenuId = x.SubMenuId,
        //                        SubMenuName = x.SubmenuName,
        //                        ControllerName = x.Controller,
        //                        ActionName = x.Action,
        //                        RoleId = x.RoleID,
        //                        URL = x.Controller + "/" + x.Action,
        //                    }).ToList(); //Get the Menu details from entity and bind it in MenuModels list.

        //                    foreach (var me in _menusTemp)
        //                    {
        //                        var chk = _menus.Where(s => s.SubMenuName == me.SubMenuName && s.ControllerName == me.ControllerName && s.ActionName == me.ActionName);
        //                        if (chk.Count() == 0)
        //                        {
        //                            var add = new MenuModels
        //                            {
        //                                MainMenuId = me.MainMenuId,
        //                                Icon = me.Icon,
        //                                MainMenuName = me.MainMenuName,
        //                                SubMenuId = me.SubMenuId,
        //                                SubMenuName = me.SubMenuName,
        //                                ControllerName = me.ControllerName,
        //                                ActionName = me.ActionName,
        //                                RoleId = me.RoleId,
        //                                URL = me.ControllerName + "/" + me.ActionName
        //                            };
        //                            _menus.Add(add);
        //                        }

        //                    }
        //                }
        //            }
               
        //        //if(Role==null)
        //        //{
        //        //        Msg = "<font color='red'>You are not profile on Role Manager</font>  ";
        //        //        return;
        //        //}
        //        //UserName = "HJ01170";
        //        ////check if user exist 
        //        //IDP sav = new IDP();
        //        //var tn = sav.CheckUser(UserName);

        //        //if (tn == false)
        //        //{                
        //        //    var ret = hep.GetUserDetails(UserName);
        //        //    UserInfo rec = new UserInfo();
        //        //    rec.BranchID = ret.branch;
        //        //    rec.EmailAddress = ret.email;
        //        //    rec.FirstName = ret.first_name;
        //        //    rec.LastName = ret.middle_name;
        //        //    rec.UserId = ret.username;
        //        //    rec.UserStatus = "ACTIVE";
        //        //    rec.AccessID = Convert.ToInt16(Role);
        //        //    rec.RegDate = DateTime.Now;

        //        //    NewPOSViewModel.UsersDetails dat = new NewPOSViewModel.UsersDetails
        //        //    {
        //        //        branch = ret.branch,
        //        //        department = ret.department,
        //        //        email = ret.email,
        //        //        first_name = ret.first_name,
        //        //        fullname = ret.fullname,
        //        //        middle_name = ret.middle_name,
        //        //        supervisor_username = ret.supervisor_username,
        //        //        surname = ret.surname,
        //        //        unit = ret.unit,
        //        //        username = ret.username,
        //        //    };

        //        //}
                
        //        //using (var db = new POSdataDataContext())
        //        //{
        //        //    var retec = hep.GetUserDetails(UserName);
        //        //    branch = retec.branch;
        //        //    Email = retec.email;
        //        //    fname = retec.first_name;
        //        //    sname = retec.middle_name;
        //        //    UserId = retec.username;
        //        //    //rec.AccessID = Convert.ToInt16(Role);
        //        //    //var rec = (from user in db.UserInfo_Vw where user.UserId == UserName select user).FirstOrDefault();
        //        //    //if(rec != null)
        //        //    //{
        //        //    //    //UserId = rec.UserId;
        //        //    //    fname = rec.FirstName;
        //        //    //    sname = rec.LastName;
        //        //    //    Email = rec.EmailAddress;
        //        //    //    Role = Convert.ToString(rec.AccessID);
        //        //    //    branch = rec.BranchID;
        //        //    //    //Zname = rec.Zname;
        //        //    //    //string hopemail = ldp.getHOPEmailAddress(rec.BranchID);
        //        //    //    //Session["HOPEmail"] = hopemail;  
        //        //    //}
        //        //    list = db.vw_tblSubmenu.ToList();
        //        //    _menus = list.Where(x => x.RoleID == Convert.ToInt32(Role)).Select(x => new MenuModels
        //        //    {
        //        //        MainMenuId = x.MenuID,
        //        //        MainMenuName = x.menu_name,
        //        //        SubMenuId = x.SubMenuId,
        //        //        SubMenuName = x.SubmenuName,
        //        //        ControllerName = x.Controller,
        //        //        ActionName = x.Action,
        //        //        RoleId = x.RoleID,
        //        //        URL = x.Controller + "/" + x.Action,
        //        //    }).ToList(); //Get the Menu details from entity and bind it in MenuModels list.

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        ValidationErrors.Add(new
        //              KeyValuePair<string, string>("Error",
        //              "Error Occured"));
        //    }

        //}
    }
}
