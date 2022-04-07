using PowerusyData.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using Tj.Cryptography;
using ExcelLibrary.Office.Excel;
using System.Data;
using System.IO;
namespace PowerusyData
{
    public class Utility
    {
        public string Value { get; set; }
        public string Text { get; set; }

    }
    public class SelectList
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class IDP
    {
        public string Uniqueid()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssFFF");
        }
        //public bool SaveUser(RootROI req, int access)
        //{
        //    using (var db = new POSdataDataContext())
        //    {
        //        //PosReq_vws
        //        var ret = db.UserInfoes.Where(x => x.UserId == req.Data.username).ToList();
        //        if (ret.Count == 0)
        //        {
        //            UserInfo rec = new UserInfo();
        //            rec.BranchID = req.Data.branch;
        //            rec.EmailAddress = req.Data.email;
        //            rec.FirstName = req.Data.first_name;
        //            rec.LastName = req.Data.surname;
        //            rec.UserId = req.Data.username;
        //            rec.UserStatus = "ACTIVE";
        //            rec.AccessID = access;
        //            rec.RegDate = DateTime.Now;
        //            db.UserInfoes.Add(rec);
        //            db.SaveChanges();
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        //public bool SaveBranch(string BranchName, string BranchCode)
        //{
        //    using (var db = new POSdataDataContext())
        //    {
        //        //PosReq_vws
        //        try
        //        {
        //            //Branch rec = new Branch();
        //            //rec.BranchName = BranchName;
        //            //rec.Branchid = BranchCode;
        //            //rec.Zname = 1;
        //            //db.Branches.Add(rec);
        //            //db.SaveChanges();
        //            tblBranch rec = new tblBranch();
        //            rec.BranchCode = Convert.ToInt16(BranchCode);
        //            rec.BranchName = BranchName;
        //            rec.BranchStatus = 1;
        //            rec.regionCode = "1";
        //            db.tblBranches.Add(rec);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch
        //        {
        //            return false;
        //        }

        //    }
        //}
        //public bool CheckUser(string username)
        //{
        //    using (var db = new POSdataDataContext())
        //    {
        //        //PosReq_vws
        //        var ret = db.UserInfoes.Where(x => x.UserId == username).ToList();
        //        if (ret.Count == 0)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        public string GetUniqueKey(string basevalue)
        {
            string result = "";
            string tchr = basevalue.Substring(2, 2);  //last two xters
            int lasttwo = Convert.ToInt32(tchr);   //READING LAST TWO CHARACTERS
            string thdigit = basevalue.Substring(1, 1); //read 2nd Xchater of the four digit
            string fdigit = basevalue.Substring(0, 1);  // first xter of the 4 digit
            if (lasttwo <= 99)
            {
                lasttwo += 1;   // add 1 to last two digit if less than 99
                tchr = lasttwo.ToString(); // change the value of last two digit to string
                if (Convert.ToInt32(tchr) <= 9)
                {
                    tchr = "0" + Convert.ToInt32(tchr);
                }
            }
            if (lasttwo > 99)
            {
                tchr = "00";     //reset the last two characters to 00

                if (thdigit == "Z")  // last value in the xter array
                {

                    thdigit = "A";
                    fdigit = nextchar(fdigit);  //read next character

                }
                else
                {
                    thdigit = nextchar(thdigit);
                }
            }
            result = fdigit.ToString() + thdigit.ToString() + tchr.ToString();

            return result;
        }
        public string nextchar(string ch)
        {
            string nxt = "";
            string[] chars = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            int len = chars.Length;
            for (int i = 0; i < chars.Length; i++)
            {
                if (ch == chars[i])
                {
                    nxt = chars[i + 1];
                    break;
                }
            }
            return nxt;
        }
        public bool isValidNumber(int k)
        {
            bool flag = false;
            try
            {
                if (Convert.ToInt32(k) > 0)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }

            return flag;
        }

        public void writelog(string userid, string op, string ip)
        {
            try
            {
                using (var db = new powerusyDBCoreEntities())
                {
                    tbl_auditlog dsl = new tbl_auditlog();
                    dsl.user_id = Convert.ToInt32(userid);
                    dsl.operationperformed = op;
                    dsl.ipaddress = ip;
                    dsl.pagevisited = getcurrentpage();
                    dsl.dateaccessed = DateTime.Now;
                    db.tbl_auditlog.Add(dsl);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string getcurrentpage()  //This method returns Current Page Name
        {
            string sPath = "";// System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        public void DataLog(string dataToLog)
        {
            try
            {
                string appLocation = AppDomain.CurrentDomain.BaseDirectory;
                string file = appLocation + "\\ErrorLog.txt";
                if (!File.Exists(file))
                {
                    File.CreateText(file);
                }
                using (StreamWriter writer = File.AppendText(file))
                {
                    string data = "\r\nLog Written at : " + DateTime.Now.ToString() + "\n" + dataToLog;
                    writer.WriteLine(data);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void ErrorLog(string ex)
        {
            try
            {
                string pth = ConfigurationManager.AppSettings["ErrorLog"];
                //string pth =  AppDomain.CurrentDomain.BaseDirectory + "ErrorLog\\" ;
                string err = ex;
                DateTime dt = DateTime.Now;
                string fld = dt.ToString("yyyy") + "_" + dt.ToString("MM") + "_";
                pth += fld + dt.ToString("dd") + ".txt";
                if (!File.Exists(pth))
                {
                    using (StreamWriter sw = File.CreateText(pth))
                    {
                        sw.WriteLine(dt + " : " + err);
                        sw.WriteLine(" ");
                        sw.Close();
                        sw.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(pth))
                    {
                        sw.WriteLine(dt + " : " + err);
                        sw.WriteLine(" ");
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public void ErrorLog(string dataToLog, string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.CreateText(filename);
                }
                using (StreamWriter writer = File.AppendText(filename))
                {
                    string data = "\r\nLog Written at : " + DateTime.Now.ToString() + "\n" + dataToLog;
                    writer.WriteLine(data);
                    writer.WriteLine("==========================================");
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public void LogPOSTran(string dataToLog, string name)
        {
            try
            {
                string filename = AppDomain.CurrentDomain.BaseDirectory + "POSEmail\\" + name;


                if (!File.Exists(filename))
                {
                    using (StreamWriter sw = File.CreateText(filename))
                    {
                        string data = dataToLog;
                        sw.WriteLine(data);
                        sw.Close();
                        sw.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter writer = File.AppendText(filename))
                    {
                        string data = dataToLog;
                        writer.WriteLine(data);
                        writer.Flush();
                        writer.Close();
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

    }
    public class EmailSender
    {
        public int sendEmail(string mailto, string Body, string copy, string subject)
        {

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(mailto));
            msg.From = new MailAddress("no-reply@jaizbankplc.com", "Jaiz Bank Plc");
            msg.Subject = subject;
            msg.CC.Add(new MailAddress(copy));
            msg.Body = Body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("eplatform@jaizbankplc.com", "@ppS0l*12");
            client.Credentials = new NetworkCredential("9a2517fe4b6ec021281ba2c799acf74f", "c068a5db94d7c5b82ccb11243792689d");
            //client.Credentials = new NetworkCredential("cexception@jaizbankplc.com", "Abcd1234");
            //client.Credentials = new NetworkCredential("d9c251a31f99b3bc5a49b1c3d804f7a8", "74e6e3cb82178b5b5483e25ad19c3fe9");
            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
                               //client.Host = "in-v3.mailjet.com";
            client.Host = "in-v3.mailjet.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            //client.Timeout = 200000;

            try
            {
                client.Send(msg);
                return 1;

            }
            catch (Exception ex)
            {
                client.Dispose();
                IDP s = new IDP();
                s.ErrorLog(ex.Message);
                //var err2 = new LogUtility.Error()
                //{
                //    ErrorDescription = ex.Message + Environment.NewLine + ex.InnerException,
                //    ErrorTime = DateTime.Now,
                //    ModulePointer = "Email Blast",
                //    StackTrace = ex.StackTrace
                //};
                //LogUtility.ActivityLogger.WriteErrorLog(err2);
                //Console.WriteLine("Error Sending to email: " + mailto + " " + ex.ToString());
                //throw ex;
                return 0;

            }


        }

        public void sendmail(string EmailAddress, string Body, string Subject)
        {
            //using (var dbn = new NotificationDB())
            //{
            //    EmailBox mail = new EmailBox();
            //    mail.EmailAddress = EmailAddress;
            //    mail.EmailContent = Body;
            //    mail.FromAddress = "";
            //    mail.Platform = "INTBK";
            //    mail.EmailStatus = "N";
            //    mail.EntryDate = DateTime.Now;
            //    mail.Subject = Subject;
            //    dbn.EmailBoxes.Add(mail);
            //    dbn.SaveChanges();
            //}
        }

        public int sendEmailCompliance(string mailto, string CC, string Body, string subject)
        {

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(mailto));

            //msg.From = new MailAddress("eplatform@jaizbankplc.com", "Jaiz Bank Plc");
            //msg.From = new MailAddress("cexception@jaizbankplc.com", "Jaiz Bank Plc");
            msg.From = new MailAddress("no-reply@jaizbankplc.com", "Jaiz Bank Plc");
            //msg.CC.Add(mailto2);
            msg.Subject = @"" + subject;
            msg.Body = Body;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("9a2517fe4b6ec021281ba2c799acf74f", "c068a5db94d7c5b82ccb11243792689d");

            client.Port = 587; // You can use Port 25 if 587 is blocked (mine is!)
            client.Host = "in-v3.mailjet.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Timeout = 200000;

            try
            {
                client.Send(msg);
                return 1;

            }
            catch (Exception ex)
            {
                client.Dispose();
                //var err2 = new LogUtility.Error()
                //{
                //    ErrorDescription = ex.Message + Environment.NewLine + ex.InnerException,
                //    ErrorTime = DateTime.Now,
                //    ModulePointer = "Compliance Email Blast",
                //    StackTrace = ex.StackTrace
                //};
                //LogUtility.ActivityLogger.WriteErrorLog(err2);
                //Console.WriteLine("Error Sending to email: " + mailto + " " + ex.ToString());
                //throw ex;
                return 0;

            }
        }
    }
    public class ErrorLog
    {
        public ErrorLog(Exception ex)
        {
            new ErrorLog(ex.ToString());
        }

        public ErrorLog(string ex)
        {
            try
            {
                string str = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
                DateTime now = DateTime.Now;
                string str2 = now.ToString("yyyy") + "_" + now.ToString("MM") + "_";
                str = str + str2 + now.ToString("dd") + ".txt";
                if (!File.Exists(str))
                {
                    using (StreamWriter streamWriter = File.CreateText(str))
                    {
                        streamWriter.WriteLine(now.ToString() + " : " + ex);
                        streamWriter.WriteLine(" ");
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
                else
                {
                    using (StreamWriter streamWriter = File.AppendText(str))
                    {
                        streamWriter.WriteLine(now.ToString() + " : " + ex);
                        streamWriter.WriteLine(" ");
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }
            catch
            {
            }
        }
    }
    public class Gadget
    {
        public byte[] convertDataSetToExcel(DataSet ds, string sheetname, string pth)
        {
            Workbook wb = new Workbook();
            for (int t = 0; t < ds.Tables.Count; t++)
            {
                DataTable dt = ds.Tables[t];
                Worksheet ws = new Worksheet("table_" + t.ToString());
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[0, i] = new Cell(dt.Columns[i].ColumnName);
                }
                int cols = dt.Columns.Count;
                int rows = dt.Rows.Count;

                int k = 0;
                for (int i = 0; i < rows; i++)
                {
                    k += 1;
                    for (int j = 0; j < cols; j++)
                    {
                        ws.Cells[k, j] = new Cell(dt.Rows[i][j].ToString());
                    }
                }
                for (int i = 0; i < 200; i++)
                {
                    k += 1;
                    for (int j = 0; j < cols; j++)
                    {
                        ws.Cells[k, j] = new Cell(" ");
                    }
                }
                wb.Worksheets.Add(ws);
            }

            // string pth = Server.MapPath("~/Upload/");
            string fle = DateTime.Now.ToString("yyyyMMddHHmmss") + GenerateRndNumber(4) + ".xls";
            new ErrorLog(pth + fle);
            wb.Save(pth + fle);
            byte[] bytes = new byte[1];
            try
            {
                using (FileStream fsSource = new FileStream(pth + fle, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);
                        if (n == 0)
                        {
                            fsSource.Close();
                            fsSource.Dispose();
                            break;
                        }
                        numBytesRead += n;
                        numBytesToRead -= n;
                    }
                    //numBytesToRead = bytes.Length;
                }
                FileInfo fi = new FileInfo(pth + fle);
                fi.Delete();
            }
            catch
            {

            }

            return bytes;
        }
        public string GenerateRndNumber(int cnt)
        {
            string[] key2 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random rand1 = new Random();
            string txt = "";
            for (int j = 0; j < cnt; j++)
                txt += key2[rand1.Next(0, 9)];
            return txt;
        }
        public string AppSetting(string p)
        {
            string result = "";
            try
            {
                result = ConfigurationManager.AppSettings[p];
            }
            catch
            {
            }
            return result;
        }

        public int GetInt(string key)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(HttpContext.Current.Request.Params[key]);
            }
            catch
            {
            }
            return result;
        }

        public string GetString(string key)
        {
            string result = "";
            try
            {
                result = Convert.ToString(HttpContext.Current.Request.Params[key]);
            }
            catch
            {
            }
            return result;
        }

        public long GetLong(string key)
        {
            long result = 0L;
            try
            {
                result = Convert.ToInt64(HttpContext.Current.Request.Params[key]);
            }
            catch
            {
            }
            return result;
        }

        public string enkrypt(string strEnk)
        {
            SymCryptography symCryptography = new SymCryptography();
            symCryptography.Key = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
            return symCryptography.Encrypt(strEnk);
        }
        public string Decrypt(string strEnk)
        {
            SymCryptography symCryptography = new SymCryptography();
            symCryptography.Key = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
            return symCryptography.Decrypt(strEnk);
        }
        public bool IsEmail(string emailAddress)
        {
            string pattern = "^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        public string dekrypt(string strDek)
        {
            SymCryptography symCryptography = new SymCryptography();
            symCryptography.Key = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
            return symCryptography.Decrypt(strDek);
        }

       

       
        public static void formatMobile_to_234(ref string mobile)
        {
            if (mobile.StartsWith("0"))
            {
                mobile = mobile.Remove(0, 1);
                mobile = string.Format("234{0}", mobile);
            }
            if (!mobile.StartsWith("0") && !mobile.StartsWith("234"))
            {
                mobile = string.Format("234{0}", mobile);
            }
        }

        public static void formatMobile_to_080(ref string mobile)
        {
            if (mobile.StartsWith("234"))
            {
                mobile = mobile.Remove(0, 3);
                mobile = string.Format("0{0}", mobile);
            }
            if (!mobile.StartsWith("0") && !mobile.StartsWith("234"))
            {
                mobile = string.Format("0{0}", mobile);
            }
        }
    }
}
