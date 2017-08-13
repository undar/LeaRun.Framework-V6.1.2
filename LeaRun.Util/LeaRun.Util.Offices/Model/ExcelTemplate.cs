using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.HSSF.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LeaRun.Util.Offices
{
    public class ExcelTemplate
    {
        private string templatePath;

        private string newFileName;
        private string templdateName;
        private string sheetName;
        public string SheetName
        {
            get { return sheetName; }
            set { sheetName = value; }
        }
        public ExcelTemplate(string templdateName, string newFileName)
        {
            this.sheetName = "sheet1";
            templatePath = HttpContext.Current.Server.MapPath("/") + "/Resource/ExcelTemplate/";
            this.templdateName = string.Format("{0}{1}", templatePath, templdateName);
            this.newFileName = newFileName;
        }
        public void ExportDataToExcel(Action<ISheet> actionMethod)
        {
            using (MemoryStream ms = SetDataToExcel(actionMethod))
            {
                byte[] data = ms.ToArray();

                #region response to the client
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + newFileName));
                System.Web.HttpContext.Current.Response.BinaryWrite(data);
                #endregion
            }
        }
        private MemoryStream SetDataToExcel(Action<ISheet> actionMethod)
        {
            //Load template file
            FileStream file = new FileStream(templdateName, FileMode.Open, FileAccess.Read);
            XSSFWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheet(SheetName);

            if (actionMethod != null) actionMethod(sheet);

            sheet.ForceFormulaRecalculation = true;
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                //ms.Position = 0;
                return ms;
            }
        }
    }
}
