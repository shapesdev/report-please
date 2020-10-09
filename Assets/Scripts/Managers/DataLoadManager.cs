using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct AreasNGrabbags
{
    public string area;
    public string grabbag;
}

public class DataLoadManager : MonoBehaviour, IManager
{
    private Dictionary<int, List<IScenario>> days;

    private List<IScenario> scenarios;

    private List<AreasNGrabbags> areas;

    public void Initialize()
    {
        days = new Dictionary<int, List<IScenario>>();

        InitializeAreasAndGrabbags();

        InitializeDayOne(1);
        InitializeDayTwo(2);
        InitializeDayThree(3);

        //DayChecker(1);
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

    /*    private void SpacesChecker(int resp)
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
        }*/

    /*    private void CloseChecker(int resp)
        {
            var value = days[2][resp - 1];

            if(value.GetReportType() == ReportType.Response)
            {
                var response = (Response)value;

                Debug.Log(response.WasItClosedCorrectly());
            }
        }*/

    private void InitializeAreasAndGrabbags()
    {
        areas = new List<AreasNGrabbags>();

        AreasNGrabbags webgl = new AreasNGrabbags { area = "WebGL", grabbag = "WebGL Grabbag" };
        AreasNGrabbags terrain = new AreasNGrabbags { area = "Terrain", grabbag = "Terrain Grabbag" };
        AreasNGrabbags desktop = new AreasNGrabbags { area = "Window Management", grabbag = "Desktop Grabbag" };
        AreasNGrabbags imgui = new AreasNGrabbags { area = "IMGUI", grabbag = "Editor-External Grabbag" };
        AreasNGrabbags probuilder = new AreasNGrabbags { area = "Probuilder", grabbag = "Scene Tooling Grabbag" };
        AreasNGrabbags packman = new AreasNGrabbags { area = "Packman", grabbag = "Packman Grabbag" };
        AreasNGrabbags texture = new AreasNGrabbags { area = "Texture", grabbag = "Graphics - Texturing and Streaming Grabbag" };
        AreasNGrabbags physics2d = new AreasNGrabbags { area = "Physics2D", grabbag = "John the 2D Guy" };
        AreasNGrabbags assetImport = new AreasNGrabbags { area = "Asset Import Pipeline", grabbag = "Asset Pipeline V2 Grabbag" };

        AreasNGrabbags shader = new AreasNGrabbags { area = "Shader Graph", grabbag = "ShaderGraph Grabbag" };
        AreasNGrabbags uiToolkit = new AreasNGrabbags { area = "UI Toolkit", grabbag = "UI Toolkit Grabbag" };
        AreasNGrabbags addressables = new AreasNGrabbags { area = "Addressables Assets", grabbag = "Addressables Grabbag" };
        AreasNGrabbags windows = new AreasNGrabbags { area = "Windows", grabbag = "Desktop Grabbag" };
        AreasNGrabbags text = new AreasNGrabbags { area = "Text", grabbag = "John the Text Guy" };
        AreasNGrabbags scripting = new AreasNGrabbags { area = "Scripting", grabbag = "Scripting Grabbag" };

        areas.Add(webgl);
        areas.Add(terrain);
        areas.Add(desktop);
        areas.Add(imgui);
        areas.Add(probuilder);
        areas.Add(packman);
        areas.Add(texture);
        areas.Add(physics2d);
        areas.Add(assetImport);
        areas.Add(shader);
        areas.Add(uiToolkit);
        areas.Add(addressables);
        areas.Add(windows);
        areas.Add(text);
        areas.Add(scripting);
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

    private void InitializeDayTwo(int day)
    {
        scenarios = new List<IScenario>();

        Tester tester1 = new Tester("Justinas", "Paulauskas", "j.paulauskas@unity3d.com", new DateTime(2019, 5, 25), new DateTime(2021, 5, 25));
        Tester tester2 = new Tester("Kristijonas", "Dadutis", "k.dadutis@unity3d.com", new DateTime(2020, 3, 15), new DateTime(2020, 11, 10));

        Response response1 = new Response(1212956, new DateTime(2020, 11, 9), new DateTime(2020, 11, 11), "Hi,\n\n" +
        "Thanks for getting in touch, we actually know about this issue and are tracking progress here: https://issuetracker.unity3d.com/product/unity/issues/guid/1212956\n\n" +
        "Please reach out to me if I can answer any questions or be of further help.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1, CloseType.Duplicate, CloseType.Duplicate);

        Response response2 = new Response(1212957, new DateTime(2020, 11, 9), new DateTime(2020, 11, 11), "Hi,\n\n" +
        "We haven't received a response from you on the issue.\n\n" +
        "Please let us know if you have more information.\n\n" +
        "For now, this case will be closed.If we hear from you in the future, we'll reopen it for further investigation.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1, CloseType.NotQualified, CloseType.NotQualified);

        Response response3 = new Response(1212958, new DateTime(2020, 11, 4), new DateTime(2020, 11, 11), "Hi,\n\n" +
        "Thanks for getting in touch!\n\n" +
        "Could you please attach a small project with step - by - step directions ? We can then reproduce it on our side for further investigation.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1, CloseType.Responded, CloseType.NotQualified);

        Response response4 = new Response(1212959, new DateTime(2020, 11, 10), new DateTime(2020, 11, 11), "Hi,\n\n" +
        "Thank you for submitting this feature request. We really appreciate it when our users contribute to how Unity should look in the future.\n\n" +
        "Unfortunately, feature requests are no longer being handled via bug reports.Now our primary feedback channel is Unity Forums, https://forum.unity.com/.\n" +
        "The forums are a great place for discussion, ideation, and inspiration between community members and Unity team members.\n\n" +
        "If you have any further questions, feel free to contact our team.\n\n" +
        "Thanks,\n" +
        tester1.GetName() +
        "\nCustomer QA Team", tester1.GetEmail(), tester1, CloseType.Responded, CloseType.Responded);

        Response response5 = new Response(1212145, new DateTime(2020, 11, 11), new DateTime(2020, 11, 11), "Hi,\n\n" +
        "Thank you for contacting us\n\n." +
        "The issue is related to the 3rd party package.Currently, Unity does not support 3rd party packages, thus, we recommend contacting the developer of that package for further assistance.\n\n" +
        "Regards,\n" +
        tester2.GetName() +
        "\nCustomer QA Team", tester2.GetEmail(), tester2);

        scenarios.Add(response1);
        scenarios.Add(response2);
        scenarios.Add(response3);
        scenarios.Add(response4);
        scenarios.Add(response5);

        days.Add(day, scenarios);
    }

    private void InitializeDayThree(int day)
    {
        scenarios = new List<IScenario>();

        Tester tester1 = new Tester("Donny", "Vaichio", "d.vaichio@unity3d.com", new DateTime(2018, 1, 19), new DateTime(2022, 6, 9));
        Tester tester2 = new Tester("Kazys", "Binkis", "k.binkis@unity3d.com", new DateTime(2020, 8, 18), new DateTime(2022, 7, 10));

        EditorBug editor1 = new EditorBug("Particles are not visible in WebGL Build", 1230101, tester1.GetName(), new AreasNGrabbags { area = "WebGL", grabbag = "WebGL Grabbag" },
        "How to reproduce:" +
        "1.Open the attached " + "repro.zip" + " project" +
        "2.Go to File->Build And Run" +
        "3.Observe the application in the browser",
        "Expected result: Particles are visible in the browser" +
        "Actual result: Particles are not visible in the browser",
        "Reproducible with: 2019.2.0a7, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3" +
        "Not reproducible with: 2018.4.27f1, 2019.2.0a6", true, "FAV: 2019.2.0a7, 2019.4, 2020.1, 2020.2",
        true, 5, 2, 2, tester1);

        EditorBug editor2 = new EditorBug("Terrain's 'Pixel Error' value has no effect when Camera's Z position is set to 0",
        1540012, tester1.GetName(), new AreasNGrabbags { area = "Terrain", grabbag = "Terrain Grabbag" },
        "How to reproduce:" +
        "1.Open the attached 'repro.zip' project" +
        "2.Enter Play Mode" +
        "3.Select the Terrain GameObject and change the Pixel Error property" +
        "4.Observe the Tris value in the Statistics window in the Game view as you adjust the Pixel Error value",
        "Expected result: Tris value is changing accordingly" +
        "Actual result: Tris value is not updated",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3", false, "FAV: 2018.4, 2020.2",
        true, 3, 3, 2, tester1);

        EditorBug editor3 = new EditorBug("Editor Window loses focus when Color Picker is closed with a keybaord",
        1531214, tester1.GetName(), new AreasNGrabbags { area = "Window Management", grabbag = "Desktop Grabbag" },
        "How to reproduce:" +
        "1.Open user's attached 'OmgWhatIsThisBug.zip' project" +
        "2.Go to Window->TestUIElements and press Repro" +
        "3.Press Tab until you reach the Color property and press Enter" +
        "4.When Color Picker is opened press Enter" +
        "5.Press Tab two times and observe the Test window",
        "Expected result: Focus is on the Repro window" +
        "Actual result: Focus is no longer on the Repro window",
        "Reproducible with: 2019.4.11f1 , 2020.1.6f1, 2020.2.0b3" +
        "Could not test with: 2018.4.27f1(UIElements not supported)",
        false, "FAV: 2018.4.27f1, 2019.4, 2020.1, 2020.2", true, 3, 3, 2, tester1);

        EditorBug editor4 = new EditorBug("Selection pop-up windows are not focused when opened",
        1675467, tester1.GetName(), new AreasNGrabbags { area = "IMGUI", grabbag = "Editor-External Grabbag" },
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
        "How to reproduce:" +
        "1.Open user's attached 'UnrealEngineBetterNGL.zip' project" +
        "2.Select the 'Image' GameObject" +
        "3.In the Inspector press Add Component" +
        "4.When dropdown is opened enter any text" +
        "5.Click outside the window",
        "Expected result: The entered text is selected and the window is focused " +
        "Actual result: The entered text is not selected and the window is not focused",
        false, "FAV: 2018.4, 2020.2", true, 4, 3, 3, tester1);

        PackageBug package1 = new PackageBug("Probuilder objects receive distorted lighting in the Windows/Mac Player when using Realtime GI",
        1575123, tester1.GetName(), new AreasNGrabbags { area = "Probuilder", grabbag = "Scene Tooling Grabbag" },
        "How to reproduce:" +
        "1.Open user's attached 'OMG JUST FIX IT.zip' project" +
        "2.Open 'SampleScene' Scene" +
        "3.Enter Play Mode" +
        "4.Observe the Game view" +
        "5.Go to File->Build And Run" +
        "6.Observe the application in the Player",
        "Expected result: Objects are illuminated the same as in the Editor, the lighting on the objects is not distorted" +
        "Actual result: Objects are not illuminated the same as in the Editor, the lighting on the objects is distorted",
        "Reproducible with: 4.3.0-preview.6 (2019.4.2f1), 4.3.1 (2020.1.6f1), 4.4.0-preview.1 (2020.2.0b3)" +
        "Not reproducible with: 4.2.3(2018.4.24f1), 4.3.0 - preview.4(2019.4.2f1)",
        true, "FAV: 2019.4, 2020.2", false, 2, 2, 2, "Shader Graph", "4.3.0-preview.6", tester1);

        EditorBug editor5 = new EditorBug("Keyboard input is not detected in the Input Field when built on WebGL",
        1467154, tester1.GetName(), new AreasNGrabbags { area = "WebGL", grabbag = "WebGL Grabbag" },
        "How to reproduce:" +
        "1.Open user's attached 'ThisInputWow.zip' project" +
        "2.Go to File->Build Settings" +
        "3.Make sure the platform is set to WebGL" +
        "4.Press Build" +
        "5.Write some text in the Input Field in the browser",
        "Expected result: Text appears in the Input Field, keyboard input (eg. CTRL+V) is registered" +
        "Actual result: No text appears in the Input Field, keyboard input(eg.CTRL + V) is not registered",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
        false, "FAV: 2018.4, 2020.2", true, 3, 4, 2, tester1);

        EditorBug editor6 = new EditorBug("NullReferenceException error is thrown if a sample in package.json does not exist",
         1644567, tester2.GetName(), new AreasNGrabbags { area = "Packman", grabbag = "Packman Grabbag" },
         "How to reproduce:" +
         "1.Open user's attached 'PacBugs.zip' project" +
         "2.Go to Window->Package Manager" +
         "3.Expand 'Level Creator' in the Package Manager" +
         "4.Select the 'Level Creator v2' package" +
         "5.Observe the Console log",
         "Expected result: No errors are thrown when selecting a package, which doesn't have 'displayName' or 'path' fields in the samples attribute in the package.json file" +
         "Actual result: 'NullReferenceException: Object reference not set to an instance of an object' error is thrown in the Console log when a 'displayName' field is not in the samples attribute in the package.json file",
         "Reproducible with: 2020.2.0a12, 2020.2.0b3" +
         "Not reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0a11",
         true, "FAV: 2020.2.0a12", true, 3, 3, 2, tester2);

        EditorBug editor7 = new EditorBug("Textures become black when they are converted with Graphics.ConvertTexture",
         1754664, tester2.GetName(), new AreasNGrabbags { area = "Texture", grabbag = "Graphics - Texturing and Streaming Grabbag" },
         "How to reproduce:" +
         "1.Open user's attached 'BugReport.zip' project" +
         "2.Open Assets / Textures folder" +
         "3.Observe the textures in the Textures folder" +
         "4.Go to BugReport->Convert Textures" +
         "5.Open Assets / Textures_Converted folder" +
         "6.Observe the textures in the Textures_Converted folder",
         "Expected result: Converted textures have a colorful pattern as the original textures" +
         "Actual result: Converted textures become black",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 3, 3, 2, tester2);

        EditorBug editor8 = new EditorBug("Collision geometry is not applied correctly to a Tilemap when Composite Collider's 2D 'Geometry Type' is set to Polygon",
         1542164, tester2.GetName(), new AreasNGrabbags { area = "Physics2D", grabbag = "Desktop Grabbag" },
         "How to reproduce:" +
         "1.Open the user's attached 'TileCollisionBug.zip' project " +
         "2.Open the 'SampleScene' Scene(Assets folder)" +
         "3.Make sure the Scene view is visible" +
         "4.Select the 'Tilemap' GameObject in the Hierarchy window" +
         "5.Observe the Scene view",
         "Expected result: Correct collision geometry is applied to the Tilemap, no tiles are left without a collider and there are no holes in the collider" +
         "Actual result: Two colliders appear on the Tilemap, as the Tilemap gets wider the collider gets more inconsistent",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 3, 3, 2, tester2);

        EditorBug editor9 = new EditorBug("ScriptableObjects are shown as MonoBehaviours in the Inspector's Narrow Selection section",
         1670414, tester2.GetName(), new AreasNGrabbags { area = "Asset Import Pipeline", grabbag = "Asset Pipeline V2 Grabbag" },
         "How to reproduce:" +
         "1.Open user's attached 'inspectorSelections.zip' project" +
         "2.Select all Assets from the Assets folder in the Project window" +
         "3.Observe the Inspector window's Narrow the Selection section",
         "Expected result: '4 Scriptable Objects' are in the Narrow the Selection section" +
         "Actual result: '4 Mono Behaviours' are in the Narrow the Selection section",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 1, 3, 2, tester2);

        scenarios.Add(editor1);
        scenarios.Add(editor2);
        scenarios.Add(editor3);
        scenarios.Add(editor4);
        scenarios.Add(editor5);
        scenarios.Add(editor6);
        scenarios.Add(editor7);
        scenarios.Add(editor8);
        scenarios.Add(editor9);
        scenarios.Add(package1);

        days.Add(day, scenarios);
    }
}
