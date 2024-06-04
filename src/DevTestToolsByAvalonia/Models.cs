using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTestToolsByAvalonia
{
    public class Models
    {
    }
    public class DataTableInfo
    {
        public string? TableName { get; set; }
        public string? TableComment { get; set; }
     
    }
    public class TableDetailInfo
    {

        public string COLUMN_NAME { get; set; }
        public string DATA_TYPE { get; set; }
        public string COLUMN_COMMENT { get; set; }
        public string IS_NULLABLE { get; set; }
    }
    public class JsonModel {

        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}
