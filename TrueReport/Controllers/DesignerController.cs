using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Aspose.Pdf;
using Common.Entities;
using Common.Responces;
using TrueReport.Helpers;
using System.Xml;

namespace TrueReport.Controllers
{
    public class DesignerController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult InitReportControls()
        {
            var response = ServiceProxy.Call(s => s.GetDataSourceList());

            AjaxResponse ajaxResponse = response.ResponseStatus == ResponseStatus.Ok
                ? AjaxResponse.Successful(string.Empty, response)
                : AjaxResponse.Failed(response.ErrorMessage, null);

            return Json(ajaxResponse);
        }


        #region DataSource

        public JsonResult GetDataSource(string dataSourceName)
        {
            var response = ServiceProxy.Call(s => s.SimpleGetDataSource(dataSourceName));

            AjaxResponse ajaxResponse;
            if (response.ResponseStatus == ResponseStatus.Ok)
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                response.Entity.WriteTo(tx);
                string strXmlText = sw.ToString();
                ajaxResponse = AjaxResponse.Successful(string.Empty, strXmlText);
            }
            else
            {
                ajaxResponse = AjaxResponse.Failed(response.ErrorMessage, null);

            }

            return Json(ajaxResponse);
        }

        #endregion DataSource

        
        #region Template

        public JsonResult LoadTemplate()
        {
            var response = ServiceProxy.Call(s => s.LoadTemplate());

            AjaxResponse ajaxResponse = response.ResponseStatus == ResponseStatus.Ok
                ? AjaxResponse.Successful(string.Empty, response)
                : AjaxResponse.Failed(response.ErrorMessage, null);

            return Json(ajaxResponse);
        }

        public JsonResult LoadDemoTemplate()
        {
            var response = ServiceProxy.Call(s => s.LoadDemoTemplate());

            AjaxResponse ajaxResponse = response.ResponseStatus == ResponseStatus.Ok
                ? AjaxResponse.Successful(string.Empty, response)
                : AjaxResponse.Failed(response.ErrorMessage, null);

            return Json(ajaxResponse);
        }


        public JsonResult SaveTemplate(List<ElementDto> elements)
        {
            var response = ServiceProxy.Call(s => s.SaveTemplate(elements));

            AjaxResponse ajaxResponse = response.ResponseStatus == ResponseStatus.Ok
                ? AjaxResponse.Successful(string.Empty, response)
                : AjaxResponse.Failed(response.ErrorMessage, null);

            return Json(ajaxResponse);
        }

        #endregion Templeate


        #region Print

        public JsonResult PrintReport(List<ElementDto> elements, string dataSourceName)
        {
            var response = ServiceProxy.Call(s => s.GetReport(elements, dataSourceName));

            AjaxResponse ajaxResponse;
            if (response.ResponseStatus == ResponseStatus.Ok)
            {
                //string reportName;
                //bool saveresult = ReportSaver.SaveReport(response.Entity, out url);
                //ajaxResponse = AjaxResponse.Successful(string.Empty, reportName);


                //Now application deployed in azure, and it requires some additional configuring for store files to disc
                //so instead of this I just store report in session for now.
                Session["Report"] = response.Entity;

                ajaxResponse = AjaxResponse.Successful(string.Empty, "fake_report_name");
            }
            else
            {
                ajaxResponse = AjaxResponse.Failed(response.ErrorMessage, null);
            }

            return Json(ajaxResponse);
        }

        public ActionResult OpenReport(string id)
        {
            //Now application deployed in azure, and it requires some additional configuring for store files to disc
            //so instead of this I just store report in session for now.
            byte[] reportBuffer = Session["Report"] as byte[];

            if (reportBuffer == null)
                return RedirectToAction("Index");

            MemoryStream output = new MemoryStream();
            output.Write(reportBuffer, 0, reportBuffer.Length);
            output.Position = 0;
            return File(output, "application/pdf");
        }

        #endregion Print
    }
}