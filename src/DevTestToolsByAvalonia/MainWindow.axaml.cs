using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Tmds.DBus.SourceGenerator;

namespace DevTestToolsByAvalonia
{
    public partial class MainWindow : Window
    {
        private string DataBaseName;
        private IDbConnection conn;
        public List<DataTableInfo> SourceTable;
        public MainWindow()
        {
            InitializeComponent();
            this.filter.IsReadOnly = true;
        }
        public void ButtonClicked(object source, RoutedEventArgs args)
        {
            string dbconnectionstring = ConnectString.Text;
            Regex regex = new Regex("(?<=(database|Database)=).*?(?=;)");
            Match match = regex.Match(dbconnectionstring);
            string databasename = match.Groups[0].Value;
            DataBaseName = databasename;
            string baseSqlText = string.Format(@"select table_name,TABLE_COMMENT from information_schema.tables where table_schema='{0}' and table_type='base table';", databasename);
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
            SourceTable = dataTableInfos;
            DataTableInfoGrid.ItemsSource = SourceTable;
            filter.IsReadOnly = false;
            ConnectString.IsReadOnly = true;
        }
        private IDbConnection GetConnection(string connstr)
        {
            conn = new MySqlConnection(connstr);
            conn.Open();
            return conn;
        }

        private void Sreach_TextChanged(object sender, TextChangedEventArgs e)
        {
            var list = SourceTable.Where(x => x.TableName.Contains(filter.Text.ToLower()) || x.TableComment.Contains(filter.Text.ToLower()));
            DataTableInfoGrid.ItemsSource = list;
        }

        private void DataGrid_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {

            DataTableInfo tbi = DataTableInfoGrid.SelectedItem as DataTableInfo;
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
                    tdiList.Add(new TableDetailInfo { COLUMN_NAME = dr[0].ToString(), DATA_TYPE = dr[1].ToString(), COLUMN_COMMENT = dr[2].ToString(), IS_NULLABLE = dr[3].ToString() });
                }
                TableInfoDetailWindow tidw = new TableInfoDetailWindow(tdiList);
                tidw.Show();
                tidw.Title = tbi.TableName;
                tidw.TableDetailInfoGrid.ItemsSource = tdiList;
            }
        }
    }
}
