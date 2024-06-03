using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using DevTestToolsByAvalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Collections.Generic;
using System.Text;
using Tmds.DBus.Protocol;

namespace DevTestToolsByAvalonia;

public partial class TableInfoDetailWindow : Window
{
    private WindowNotificationManager? _manager;

    public TableInfoDetailWindow()
    {
        InitializeComponent();
    }

    private void btnCreatModel_Click(object sender, RoutedEventArgs e)
    {
        Tools tools = new Tools();
        List<TableDetailInfo>? dtlist = TableDetailInfoGrid.ItemsSource as List<TableDetailInfo>;
        StringBuilder builder = new StringBuilder();
        builder.Append("\r using System;");
        builder.Append("\r\n using Newtonsoft.Json;");
        builder.Append("\r\n using System.Linq;");
        builder.Append("\r\n using System.Text;");
        builder.Append("\r\n namespace " + "你的命名空间");
        builder.Append("\r\n {");
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
        builder.Append("\r\n }");
        Clipboard?.SetTextAsync(builder.ToString());
        MessageBoxManager.GetMessageBoxStandard("生成成功", "代码已经复制到剪贴板！", ButtonEnum.Ok).ShowAsync();
    }

    private void btnCreatJsonWithComment_Click(object sender, RoutedEventArgs e)
    {
        List<TableDetailInfo>? dtlist = this.TableDetailInfoGrid.ItemsSource as List<TableDetailInfo>;
        StringBuilder builder = new StringBuilder();
        builder.Append("{");
        foreach (var item in dtlist)
        {
            builder.Append("\r\n \"" + item.COLUMN_NAME + "\":");
            builder.Append(" \"" + (string)item.COLUMN_COMMENT + "\",");
        }
        builder.Remove(builder.Length - 1, 1); // 去除最后一个逗号
        builder.Append("\r}");
        Clipboard?.SetTextAsync(builder.ToString());
        MessageBoxManager.GetMessageBoxStandard("生成成功", "代码已经复制到剪贴板！", ButtonEnum.Ok).ShowAsync();
    }
}