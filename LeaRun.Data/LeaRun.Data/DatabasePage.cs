namespace LeaRun.Data
{
    using System;
    using System.Data.Common;
    using System.Text;

    public class DatabasePage
    {
        public StringBuilder MySqlPageSql(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            builder.Append(strSql + str);
            builder.Append(string.Concat(new object[] { " limit ", num, ",", pageSize }));
            return builder;
        }

        public StringBuilder OraclePageSql(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            builder.Append("Select * From (Select ROWNUM,");
            builder.Append(string.Concat(new object[] { " T.* From (", strSql, str, ")  T )  N Where rowNum > ", num, " And rowNum <= ", num2 }));
            return builder;
        }

        public StringBuilder SqlPageSql(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder builder = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num2 = pageIndex * pageSize;
            string str = "";
            if (!string.IsNullOrEmpty(orderField))
            {
                if ((orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC")) > 0)
                {
                    str = " Order By " + orderField;
                }
                else
                {
                    str = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                str = "order by (select 0)";
            }
            builder.Append("Select * From (Select ROW_NUMBER() Over (" + str + ")");
            builder.Append(string.Concat(new object[] { " As rowNum, * From (", strSql, ")  T ) As N Where rowNum > ", num, " And rowNum <= ", num2 }));
            return builder;
        }
    }
}

