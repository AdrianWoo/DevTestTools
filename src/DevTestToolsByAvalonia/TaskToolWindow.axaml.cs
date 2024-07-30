using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DevTestToolsByAvalonia;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;

namespace DevTestToolsByAvalonia;

public partial class TaskToolWindow : Window
{
    public TaskToolWindow()
    {
        InitializeComponent();
        Process[] myProcesses = Process.GetProcesses();//获取当前进程数组
        foreach (Process myProcess in myProcesses)//遍历数组
        {
            if (myProcess.MainWindowTitle.Length > 0)//如果进程存在用户界面标题
                Debug.Print(myProcess.MainWindowTitle);
              
        }


        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] endpoints = properties.GetActiveTcpListeners();
        string s = "";

    }
}