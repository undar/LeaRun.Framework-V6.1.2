using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model
{
    public class IsNotNullAttribute : System.Attribute, IVerifyAttribute
    {
        bool IsNotNull { get; set; }

        string Message { get; set; }

        public IsNotNullAttribute()
        {
            IsNotNull = true;
            Message = "不能为空";
        }

        public IsNotNullAttribute(bool isNotNull)
        {
            IsNotNull = isNotNull;
            Message = "不能为空";
        }
        public IsNotNullAttribute(bool isNull, string message)
        {
            IsNotNull = isNull;
            Message = message;
        }

        public bool Verify(Type type, object obj, out string message)
        {
            message = "";

            if (IsNotNull == false)
            {
                return true;
            }

            if (obj == null)
            {
                message = Message;
                return false;
            }

            if (obj is IList)
            {

                if ((obj as IList).Count <= 0)
                {
                    message = Message;
                    return false;
                }
            }

            return true;
        }
    }
}
