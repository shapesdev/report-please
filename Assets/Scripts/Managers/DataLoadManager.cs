using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataLoadManager : MonoBehaviour, IManager
{
    Dictionary<int, List<IScenario>> days;

    List<IScenario> scenarios;

    public void Initialize()
    {
        days = new Dictionary<int, List<IScenario>>();

        InitializeDayOne(1);

        DayChecker(6);
    }

    public void FixedManagerUpdate()
    {

    }

    public void ManagerUpdate()
    {

    }

/*    private void EmailChecker()
    {
        var value = days[1][2];
        Debug.Log(value);
        if (value.GetReportType() == ReportType.Response)
        {
            var response = (Response)value;

            if(response.GetEmail() == response.GetTester().GetEmail())
            {
                Debug.Log("Its correct");
            }
            else
            {
                Debug.Log("It's not");
            }
        }
    }*/

    private void DayChecker(int resp)
    {
        var value = days[1][resp - 1];
        
        if(value.GetReportType() == ReportType.Response)
        {
            var response = (Response)value;

            var chars = response.GetEmail().ToCharArray();

            int count = 0;

            foreach(var ch in chars)
            {
                if(ch == ' ')
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                if(count >= 2)
                {
                    Debug.Log("There are double spaces in day:" + resp);
                    break;
                }
            }
        }
    }

    private void InitializeDayOne(int day)
    {
        scenarios = new List<IScenario>();

        Tester tester1 = new Tester("Ramunas", "Pondukas", "ramunas.pondukas@unity3d.com", new DateTime(2020, 10, 3), new DateTime(2020, 12, 25));
        Tester tester2 = new Tester("Sergej", "Aleksej", "sergej.aleksej@unity3d.com", new DateTime(2020, 5, 5), new DateTime(2020, 11, 25));

        Response response1 = new Response(1122777, new DateTime(2020, 11, 3), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "We haven't received a response from you on the issue.\n\n" +
        "Please let us know if you have more information.\n\n" +
        "For now, this case will be closed. If we hear from you in the future, we'll reopen it for further investigation.\n\n" +
        "Thanks\n"
        + tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1);

        Response response2 = new Response(1122778, new DateTime(2020, 11, 9), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thank you for getting in touch!\n\n" +
        "We no longer support Unity versions lower than Unity 2019.4 except for Unity 2018.4 LTS(long - term support) version.\n\n" +
        "Please update to the newest stable version here:https://unity3d.com/get-unity/update\n\n" +
        "If your problem still exists after updating, please submit a new bug report and we'll investigate for you.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1);

        Response response3 = new Response(1122779, new DateTime(2020, 11, 8), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thanks for reporting the issue.\n\n" +
        "Could you please give us more details by doing the following steps:\n\n" +
        "1.Attach an example project\n" +
        "2.Briefly describe the issue\n" +
        "3.Define the exact steps needed to reproduce the issue\n" +
        "4.Describe the expected and actual results\n\n" +
        "Thanks,\n" +
        "Petras\n" +
        "Customer QA Team\n", tester1.GetEmail(), tester1);

        Response response4 = new Response(1122780, new DateTime(2020, 11, 7), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thank you for submitting this feature request. We really appreciate it when our users contribute to how Unity should look in the future.\n\n" +
        "Unfortunately, feature requests are no longer being handled via bug reports.Now our primary feedback channel is Unity Forums, https://forum.unity.com/. The forums are a great place for discussion, ideation, and inspiration between community members and Unity team members.\n\n" +
        "If you have any further questions, feel free to contact our team.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1);

        Response response5 = new Response(1122781, new DateTime(2020, 11, 8), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thank you for contacting us\n\n." +
        "The issue is related to the 3rd party package.Currently, Unity does not support 3rd party packages, thus, we recommend contacting the developer of that package for further assistance.\n\n" +
        "Regards,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1);

        Response response6 = new Response(1244581, new DateTime(2020, 11, 5), new DateTime(2020, 11, 10), "Hi ,  \n\n" +
        "Thank  you  for  contacting Unity  about  your  issue.\n" +
        "Unfortunately, we are not able to reproduce it.Are you still experiencing this problem or was it a one time issue ?" +
        "If you're still facing this problem, could you please provide us with more information - project and the exact steps needed to reproduce on our side?" +
        "Thanks," +
        tester2.GetName() +
        "Customer QA Team", tester2.GetEmail(), tester2);

        Response response7 = new Response(1244582, new DateTime(2020, 11, 6), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thanks for getting in touch!\n\n" +
        "Could you please attach a small project with step - by - step directions ? We can then reproduce it on our side for further investigation.\n\n" +
        "Thanks," +
        tester2.GetName() +
        "Customer QA Team", tester2.GetName() + "." + tester2.GetSurname() + "@assetstore.unity.com", tester2);

        Response response8 = new Response(1244583, new DateTime(2020, 11, 7), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thank you for contacting Unity about your issue.\n\n" +
        "Unfortunately, we are not able to reproduce it.Are you still experiencing this problem or was it a one time issue ?\n\n" +
        "If you're still facing this problem, could you please provide us with more information - project and the exact steps needed to reproduce on our side?\n" +
        "Thanks,\n" +
        tester2.GetName() +
        "\nCustomer QA Team", tester2.GetEmail(), tester2);

        Response response9 = new Response(1244584, new DateTime(2020, 11, 8), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "We successfully reproduced this issue, it will be possible to follow the progress on a chosen resolution in our public Issue Tracker, once the report is processed: https://issuetracker.unity3d.com/1244583\n\n" +
        "We highly appreciate your contribution.If you have further questions, feel free to contact us.\n\n" +
        "Thanks,\n" +
        tester2.GetName() +
        "\nCustomer QA Team", tester2.GetEmail(), tester2);

        Response response10 = new Response(1244585, new DateTime(2020, 11, 9), new DateTime(2020, 11, 10), "Hi,\n\n" +
        "Thanks for reporting the issue.We have fixed this problem and it should not appear in Probuilder 4.5.0 and above. This package version is available in Unity 2019.4.12f1 and above.\n\n" +
        "If you are still able to reproduce this issue using the mentioned package version, please respond to this email and the case will be reopened for further investigation.\n\n" +
        "Thanks,\n" +
        tester2.GetName() +
        "\nCustomer QA Team", tester2.GetEmail(), tester2);

        scenarios.Add(response1);
        scenarios.Add(response2);
        scenarios.Add(response3);
        scenarios.Add(response4);
        scenarios.Add(response5);
        scenarios.Add(response6);
        scenarios.Add(response7);
        scenarios.Add(response8);
        scenarios.Add(response9);
        scenarios.Add(response10);

        days.Add(day, scenarios);
    }
}
