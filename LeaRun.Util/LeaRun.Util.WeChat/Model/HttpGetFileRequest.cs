using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LeaRun.Util;
using LeaRun.Util.WeChat.Helper;

namespace LeaRun.Util.WeChat.Model
{
    class HttpGetFileRequest : IHttpSend
    {
        public string Send(string url, string path)
        {
            Dictionary<string, string> header;

            var bytes = new HttpHelper().GetFile(url, out header);

            if (header["Content-Type"].Contains("application/json"))
            {
                return Encoding.UTF8.GetString(bytes);
            }
            else
            {
                Regex regImg = new Regex("\"(?<fileName>.*)\"", RegexOptions.IgnoreCase);

                MatchCollection matches = regImg.Matches(header["Content-disposition"]);

                string fileName = matches[0].Groups["fileName"].Value;

                string filepath = path.TrimEnd('\\') + "\\" + fileName;

                System.IO.Stream so = new System.IO.FileStream(filepath, System.IO.FileMode.Create);

                so.Write(bytes, 0, bytes.Length);

                so.Close();
            }

            return header.ToJson();
        }
    }
}
