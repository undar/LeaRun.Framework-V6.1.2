using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model
{
    public interface ISend<out T>
        where T : OperationResultsBase, new()
    {
        T Send();
    }
}
