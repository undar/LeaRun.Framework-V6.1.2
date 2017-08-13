using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    public class MediaGetResult :OperationResultsBase
    {
        public string FileType { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }
    }
}
