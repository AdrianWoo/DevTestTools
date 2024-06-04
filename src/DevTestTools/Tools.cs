using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DevTestTools.UI
{
    public class Tools
    {
        public string ConvertDataType(string orginDataType,string IsNull ) {
            string result = "";
            switch (orginDataType.ToLower())
            {
                case "varchar":
                case "varchar2":
                case "char":
                case "mediumtext":
                    result = "string";
                    break;
                case "decimal":
                    result = "decimal";
                    break;
                case "int":
                case "integer":
                case "smallint":
                    result = "int";
                    break;
                case "datetime":
                case "date":
                    result = "DateTime";
                    break;
                case "time":
                    result = "DateTime";
                    break;
                case "tinyint":
                    result = "int";
                    break;
                case "text":
                    result = "string";
                    break;
            }
            if (string.IsNullOrEmpty(result))
                MessageBox.Show(new Notification("错误信息标题", "错误信息的内容！", NotificationType.Error));
            if (result != "string")
                if (IsNull.ToLower() == "yes")
                    result += "?";
            return result;

        }
    }
}
