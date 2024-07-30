using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DevTestToolsByAvalonia;

public partial class TableInfoDetailWindow : Window
{
    public List<TableDetailInfo> SourceTable;

    public TableInfoDetailWindow(List<TableDetailInfo> detailInfos)
    {
        InitializeComponent();
        SourceTable = detailInfos;
    }

    private void btnCrateModel_Click(object sender, RoutedEventArgs e)
    {
        Tools tools = new Tools();
        List<TableDetailInfo>? dtlist = TableDetailInfoGrid.ItemsSource as List<TableDetailInfo>;
        StringBuilder builder = new StringBuilder();
        builder.Append("using System;");
        builder.Append("\rusing Newtonsoft.Json;");
        builder.Append("\rusing System.Linq;");
        builder.Append("\rusing System.Text;");
        builder.Append("\rnamespace " + "��������ռ�");
        builder.Append("\r\n\n {");
        builder.Append("\r\n/// <summary>");
        builder.Append("\r\n///");
        builder.Append("\r\n/// </summary>");
        builder.Append("\r\n public class " + Title.ToLower());
        builder.Append("\r\n {");
        foreach (var item in dtlist)
        {
            builder.Append("\r\n/// <summary>");
            builder.Append("\r\n/// " + item.COLUMN_COMMENT);
            builder.Append("\r\n/// </summary>");
            string csType = tools.ConvertDataType(item.DATA_TYPE, item.IS_NULLABLE);
            if (csType == "DateTime" || csType == "DateTime?")
                builder.Append("\r\n[JsonConverter(typeof(DateTimeFormat))]");
            builder.Append("\r\n public " + csType + " " + item.COLUMN_NAME);
            builder.Append(" { get; set; }");
            builder.Append("\r\n");
        }
        builder.Append("\r\n\n }");
        builder.Append("\r\n }");
        Clipboard?.SetTextAsync(builder.ToString());
        MessageBoxManager.GetMessageBoxStandard("���ɳɹ�", "�����Ѿ����Ƶ������壡", ButtonEnum.Ok).ShowAsync();
    }

    private void btnCrateJsonWithComment_Click(object sender, RoutedEventArgs e)
    {
        List<TableDetailInfo>? dtlist = this.TableDetailInfoGrid.ItemsSource as List<TableDetailInfo>;
        StringBuilder builder = new StringBuilder();
        builder.Append("{");
        foreach (var item in dtlist)
        {
            builder.Append("\r\n \"" + item.COLUMN_NAME + "\":");
            builder.Append(" \"" + (string)item.COLUMN_COMMENT + "\",");
        }
        builder.Remove(builder.Length - 1, 1); // ȥ�����һ������
        builder.Append("\r}");
        Clipboard?.SetTextAsync(builder.ToString());
        MessageBoxManager.GetMessageBoxStandard("���ɳɹ�", "�����Ѿ����Ƶ������壡", ButtonEnum.Ok).ShowAsync();
    }

    private void Search_column_TextChanged(object sender, TextChangedEventArgs e)
    {
        var list = SourceTable.Where(x =>
            x.COLUMN_NAME.Contains(Search_column.Text, System.StringComparison.CurrentCultureIgnoreCase) ||
            x.COLUMN_COMMENT.Contains(Search_column.Text, System.StringComparison.CurrentCultureIgnoreCase));

        TableDetailInfoGrid.ItemsSource = list;
    }

}