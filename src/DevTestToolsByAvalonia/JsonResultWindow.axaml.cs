using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;



namespace DevTestToolsByAvalonia;

public partial class JsonResultWindow : Window
{
    public JsonResultWindow()
    {
        InitializeComponent();
    }
    /// <summary>
    /// µã»÷°´Å¥
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    private void Creat_Json_btn_C(object source, RoutedEventArgs args)
    {
        List<JsonModel> resultModel = new List<JsonModel>();
        List<JsonModel> feedModel = new List<JsonModel>();
        var jsonFeed = JsonTextInPut.Text;
        var jsonText = JsonTextOutPut.Text;
        var obj = (JObject)JsonConvert.DeserializeObject(jsonText);
        var feedjson = JsonConvert.DeserializeObject(jsonFeed);
        foreach (var x in feedjson as JObject)
        {
            feedModel.Add(new JsonModel { Key = x.Key, Value = x.Value.ToString() });
        }
        foreach (var x in obj as JObject)
        {
            if (x.Value.Type.ToString() == "Object")
            {
                break;
            }
            else
                obj[x.Key] = feedModel.FirstOrDefault(y => y.Key == x.Key) == null ? obj[x.Key].ToString() : feedModel.FirstOrDefault(y => y.Key == x.Key).Value;
        }
        JsonTextOutPut.Text = Convert.ToString(obj);

    }
}