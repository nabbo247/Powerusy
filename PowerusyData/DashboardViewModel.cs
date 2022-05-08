using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerusyData
{
    public class DashboardViewModel : ViewModelBase
    {

        public int TotalJobs { get; set; }
        public int TotalJobsApplied { get; set; }
        public int TotalJobsViewed { get; set; }
        public int TotalJobsBookmarked { get; set; }
        public string Username { get; set; }


        protected override void Init()
        {
            base.Init();
        }

        public override void HandleRequest()
        {
            base.HandleRequest();
        }

        protected override void Get()
        {

            using (var db = new powerusyDBCoreEntities())
            {
                var rs = (from info in db.tbl_users
                          where info.username == Username
                          select info).FirstOrDefault();
                if (rs != null)
                {
                    IsValid = true;

                    TotalJobs = db.tbl_bidding.Count(b => b.UserID == rs.id);

                    TotalJobsApplied = db.tbl_bidding.Where(b => b.UserID == rs.id)
                        .Join(db.tbl_bidding_jobs, bidding => bidding.id, job => job.BidID, (bidding, jobs) => jobs)
                        .Count();

                    var views = db.tbl_bidding_view.Where(b => b.user_id == rs.id).Select(b => b.view_count);

                    if (views.Count() > 0)
                        TotalJobsViewed = views.Sum();

                    TotalJobsBookmarked = db.tbl_bidding_bookmark.Count(b => b.bid_owner_id == rs.id);
                }
                else
                {
                    ValidationErrors.Add(new
                      KeyValuePair<string, string>("Comment",
                      "Invalid User"));
                    IsValid = false;
                }
            }
            base.Get();
        }




    }
}
