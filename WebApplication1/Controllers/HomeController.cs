using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DbLayer;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        DataTier DtObj = new DataTier();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Get_Details()
        {

            try
            {
                DataSet ds = new DataSet();
                ds = DtObj.get_record();
                List<Records> dtrecords = new List<Records>();
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    dtrecords.Add(new Records
                    {
                        Name=dr["Name"].ToString(),
                        Email=dr["Email"].ToString(),
                        Experience=int.Parse(dr["Experience"].ToString()),
                        Id=int.Parse(dr["Id"].ToString())
                    });
                }
                return Json(dtrecords, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public JsonResult Add_Record(Records rs)
        {
            string captchaText = rs.CaptchaText;
            string r = string.Empty;
            try
            {
                if (TempData["CaptchaText"] == null || captchaText != TempData["CaptchaText"].ToString())
                {
                    return Json("cAPTCHA INCORRECT");
                }
                else
                {
                    DtObj.Add_details(rs);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(rs.Email, "Registered Successfully");
                    r = "Inserted";
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Show_Details()
        {
            return View();
        }

        public ActionResult Update_Details()
        {
            return View();
        }
        public JsonResult delete_record(int id)

        {

            string res = string.Empty;

            try

            {

                DtObj.deletedata(id);

                res = "data deleted";

            }

            catch (Exception)

            {

                res = "failed";

            }

            return Json(res, JsonRequestBehavior.AllowGet);



        }

        public JsonResult update_record(Records rs)

        {

            string res = string.Empty;

            try

            {

                DtObj.update_record(rs);

                res = "Updated";

            }

            catch (Exception)

            {

                res = "failed";

            }

            return Json(res, JsonRequestBehavior.AllowGet);



        }

        public JsonResult Get_DetailsById(int id)

        {

            DataSet ds = DtObj.get_recordbyid(id);

            List<Records> listrs = new List<Records>();

            foreach (DataRow dr in ds.Tables[0].Rows)

            {

                listrs.Add(new Records

                {

                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),

                    Email = dr["Email"].ToString(),

                    Experience = Convert.ToInt32(dr["Experience"])

                });

            }

            return Json(listrs, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GenerateCaptcha()
        {
            string captchaText = string.Empty;
            Bitmap captchaImage = WebApplication1.Models.CaptchaHelper.GetCaptchaImage(out captchaText);
            byte[] byteArray = captchaImage.ConvertToByteArray();
            TempData["CaptchaText"] = captchaText;
            return File(byteArray, "image/gif");
        }


        [HttpGet]
        public ActionResult ReGenerateCaptcha()
        {
            string captchaText = string.Empty;
            Bitmap captchaImage = WebApplication1.Models.CaptchaHelper.GetCaptchaImage(out captchaText);
            byte[] byteArray = captchaImage.ConvertToByteArray();
            TempData["CaptchaText"] = captchaText;
            return File(byteArray, "image/gif");
        }
    }
}