using DevTestTools.UI;
using MySqlConnector;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace DevTestTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string DataBaseName;
        private IDbConnection conn;

        public MainWindow()
        {
            InitializeComponent();
            this.SreachBox.IsReadOnly = true;
        }

        /// <summary>
        /// 单击添加姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddName_Click(object sender, RoutedEventArgs e)
        {
            string dbconnectionstring = txtConnectionString.Text;
            Regex regex = new Regex("(?<=(database|Database)=).*?(?=;)");
            Match match = regex.Match(dbconnectionstring);
            string databasename = match.Groups[0].Value;
            DataBaseName = databasename;
            string baseSqlText = string.Format(@"select table_name,TABLE_COMMENT from information_schema.tables where table_schema='{0}' and table_type='base table';", databasename);
            // string baseSqlText2 = "select TABLE_NAME,TABLE_COMMENT from information_schema.tables";
            DataTable dt = new();
            conn = GetConnection(dbconnectionstring);
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = baseSqlText;
            dt.Load(cmd.ExecuteReader());
            List<DataTableInfo> dataTableInfos = new();
            foreach (DataRow dr in dt.Rows)
            {
                dataTableInfos.Add(new DataTableInfo { TableName = dr[0].ToString(), TableComment = dr[1].ToString() });
            }
            dataGridTableInfo.ItemsSource = dataTableInfos;
            this.SreachBox.IsReadOnly = false;
            txtConnectionString.IsReadOnly =true;
        }

        /// <summary>
        /// 打开一个新的窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOpenNewWindow_Click(object sender, RoutedEventArgs e)
        {
            TableInfoDetailWindow openNewWindow = new TableInfoDetailWindow();
            openNewWindow.Show();
        }

        private IDbConnection GetConnection(string connstr)
        {
            conn = new MySqlConnection(connstr);
            conn.Open();
            return conn;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataTableInfo tbi = dataGridTableInfo.SelectedItem as DataTableInfo;
            DataTable dt = new DataTable();
            if (tbi != null)
            {
                string tablebaseSql = string.Format("select COLUMN_NAME,DATA_TYPE,COLUMN_COMMENT,IS_NULLABLE from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}';", tbi.TableName, DataBaseName);
                var cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = tablebaseSql;
                dt.Load(cmd.ExecuteReader());
                List<TableDetailInfo> tdiList = new List<TableDetailInfo>();
                foreach (DataRow dr in dt.Rows)
                {
                    tdiList.Add(new TableDetailInfo { COLUMN_NAME = dr[0].ToString(),DATA_TYPE = dr[1].ToString(),COLUMN_COMMENT = dr[2].ToString() ,IS_NULLABLE = dr[3].ToString() });
                }
                TableInfoDetailWindow tidw = new TableInfoDetailWindow();
                tidw.Show();
                tidw.Title = tbi.TableName;
                tidw.dataGridTableInfo.ItemsSource = tdiList;
            }

        }

        private void Sreach_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGridTableInfo.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = OnFilterApplied;
            }

        }
        private bool OnFilterApplied(object obj)
        {
            if (obj is DataTableInfo)
            {
                var text = SreachBox.Text.ToLower();
                return (obj as DataTableInfo).TableName.ToLower().Contains(text)
                    || (obj as DataTableInfo).TableComment.ToLower().Contains(text);
            }
            return false;
        }
    }
}