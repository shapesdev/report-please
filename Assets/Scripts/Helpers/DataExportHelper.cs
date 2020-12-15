using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataExportHelper : MonoBehaviour
{
    public static DataExportHelper instance;

    private string folderName = "Report";
    private string fileName = "Report.csv";
    private string reportSeparator = ",";
    private string[] reportHeaders = new string[5] { "Day", "Case ID", "Case Title", "Citation", "Score" };

    private void Start()
    {
        instance = this;
    }

    public void Export()
    {
        List<string> data;

        var playerData = PlayerData.instance.GetPlayerData();

        for(int i = 0; i < playerData.Count; i++)
        {
            for(int j = 0; j < playerData[i].scenarios.Count; j++)
            {
                data = new List<string>();
                data.Add(playerData[i].day);
                data.Add(playerData[i].scenarios[j].caseID.ToString());
                data.Add(playerData[i].scenarios[j].caseTitle);
                data.Add(playerData[i].scenarios[j].citation);
                data.Add(playerData[i].scenarios[j].score.ToString());

                var arr = data.ToArray();
                AppendToReport(arr);
            }
        }
        AppendToReport(new string[2] { "Total Reports: ", PlayerData.instance.maxReports.ToString() });
        AppendToReport(new string[2] { "Correct Reports: ", PlayerData.instance.correctReports.ToString() });
        AppendToReport(new string[2] { "Citations Received: ", PlayerData.instance.citationsReceived.ToString() });
        AppendToReport(new string[2] { "Current HighScore: ", PlayerData.instance.currentHighScore.ToString() });
        AppendToReport(new string[2] { "Possible HighScore: ", PlayerData.instance.maxHighScore.ToString() });
        AppendToReport(new string[2] { "Your Win Rate: ", PlayerData.instance.correctReports * 100 / PlayerData.instance.maxReports + "%" });
    }

    public void ResetFile()
    {
        CreateReport();
    }

    private void AppendToReport(string[] data)
    {
        VerifyDirectory();
        VerifyFile();
        using(StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            foreach(var str in data)
            {
                if(finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += str;
            }
            finalString += reportSeparator;
            sw.WriteLine(finalString);
        }
    }

    private void CreateReport()
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";

            foreach(var header in reportHeaders)
            {
                if(finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += header;
            }
            finalString += reportSeparator;
            sw.WriteLine(finalString);
        }
    }

    private void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    private void VerifyFile()
    {
        string file = GetFilePath();
        if(!File.Exists(file))
        {
            CreateReport();
        }
    }

    private string GetDirectoryPath()
    {
        return Application.dataPath + "/" + folderName;
    }

    private string GetFilePath()
    {
        return GetDirectoryPath() + "/" + fileName;
    }
}
