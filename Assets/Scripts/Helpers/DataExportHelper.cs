using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataExportHelper
{
    private string folderName = "Report";
    private string fileName = "Report.csv";
    private string reportSeparator = ",";
    private string[] reportHeaders = new string[4] { "Case ID", "Case Title", "Citation", "Score" };
    private string timeStampHeader = "Time stamp";

    public void AppendToReport(string[] strings)
    {
        VerifyDirectory();
        VerifyFile();
        using(StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            foreach(var str in strings)
            {
                if(finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += str;
            }
            finalString += GetTimeStamp();
            sw.WriteLine(finalString);
        }
    }

    public void CreateReport()
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
            finalString += reportSeparator + timeStampHeader;
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

    private string GetTimeStamp()
    {
        return System.DateTime.UtcNow.ToString();
    }
}
