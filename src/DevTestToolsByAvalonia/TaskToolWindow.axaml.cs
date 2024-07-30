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
        Process[] myProcesses = Process.GetProcesses();//��ȡ��ǰ��������
        foreach (Process myProcess in myProcesses)//��������
        {
            if (myProcess.MainWindowTitle.Length > 0)//������̴����û��������
                Debug.Print(myProcess.MainWindowTitle);
              
        }


        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        IPEndPoint[] endpoints = properties.GetActiveTcpListeners();
        string s = "";

    }
}