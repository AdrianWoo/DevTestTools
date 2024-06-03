using Avalonia.Controls.Notifications;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTestToolsByAvalonia
{
    public class Tools
    {
        private WindowNotificationManager? _manager;

        public string ConvertDataType(string orginDataType, string IsNull)
        {
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

                case "bigint":
                    result = "long";
                    break;

                case "text":
                    result = "string";
                    break;
            }
            if (string.IsNullOrEmpty(result))
                MessageBoxManager.GetMessageBoxStandard("错误", "出现未知类型！" + orginDataType, ButtonEnum.Ok).ShowAsync();

            if (result != "string")
                if (IsNull.ToLower() == "yes")
                    result += "?";
            return result;
        }
    }
}