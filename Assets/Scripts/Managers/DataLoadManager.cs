using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct AreasNGrabbags
{
    public string area;
    public string grabbag;
}

public enum ReportType
{
    Response, EditorBug, PackageBug
}

public class DataLoadManager
{
    private Dictionary<int, List<IScenario>> days;

    private List<IScenario> scenarios;

    private List<AreasNGrabbags> areas;

    public DataLoadManager()
    {
        Initialize();
    }

    private void Initialize()
    {
        days = new Dictionary<int, List<IScenario>>();

        InitializeAreasAndGrabbags();
        InitializeDayOne(10);
        InitializeDayTwo(11);
        InitializeDayThree(12);
        InitializeDayFour(13);
    }

    public Tuple<List<AreasNGrabbags>, Dictionary<int, List<IScenario>>> GetAreaAndDayData()
    {
        return Tuple.Create(areas, days);
    }

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

        AreasNGrabbags shader = new AreasNGrabbags { area = "ShaderGraph", grabbag = "ShaderGraph Grabbag" };
        AreasNGrabbags uiToolkit = new AreasNGrabbags { area = "UI Toolkit", grabbag = "UI Toolkit Grabbag" };
        AreasNGrabbags addressables = new AreasNGrabbags { area = "Addressables Assets", grabbag = "Addressables Grabbag" };
        AreasNGrabbags windows = new AreasNGrabbags { area = "Windows", grabbag = "Desktop Grabbag" };
        AreasNGrabbags text = new AreasNGrabbags { area = "Text", grabbag = "John the Text Guy" };
        AreasNGrabbags scripting = new AreasNGrabbags { area = "Scripting", grabbag = "Scripting Grabbag" };
        AreasNGrabbags il2cpp = new AreasNGrabbags { area = "IL2CPP", grabbag = "VM-IL2CPP Grabbag" };
        AreasNGrabbags scene = new AreasNGrabbags { area = "Scene Management", grabbag = "Scene Management Grabbag" };

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
        areas.Add(il2cpp);
        areas.Add(scene);
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
        "If you have any further questions, feel free to contact our team.\n\n\n" +
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
        "Unfortunately, we are not able to reproduce it. Are you still experiencing this problem or was it a one time issue ?" +
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
        "Unfortunately, we are not able to reproduce it. Are you still experiencing this problem or was it a one time issue ?\n\n" +
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
        "Thank you for contacting us.\n\n" +
        "The issue is related to the 3rd party package.\nCurrently, Unity does not support 3rd party packages, thus, we recommend contacting the developer of that package for further assistance.\n\n" +
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
        Tester tester2 = new Tester("Nicholas", "Creator", "n.creator@unity3d.com", new DateTime(2020, 8, 18), new DateTime(2022, 7, 10));

        EditorBug editor1 = new EditorBug("Particles are not visible in WebGL Build", 1230101, tester1.GetName(), new AreasNGrabbags { area = "WebGL", grabbag = "WebGL Grabbag" },
        "How to reproduce:\n" +
        "1.Open the attached 'repro.zip' project\n" +
        "2.Go to File->Build And Run\n" +
        "3.Observe the application in the browser\n",
        "Expected result: Particles are visible in the browser\n" +
        "Actual result: Particles are not visible in the browser\n",
        "Reproducible with: 2019.2.0a7, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3\n" +
        "Not reproducible with: 2018.4.27f1, 2019.2.0a6", true, "FAV: 2019.2.0a7, 2019.4, 2020.1, 2020.2",
        true, 2, 1, 2, tester1);

        EditorBug editor2 = new EditorBug("Terrain's 'Pixel Error' value has no effect when Camera's Z position is set to 0",
        1540012, tester1.GetName(), new AreasNGrabbags { area = "Terrain", grabbag = "Terrain Grabbag" },
        "How to reproduce:\n" +
        "1.Open the attached 'repro.zip' project\n" +
        "2.Enter Play Mode\n" +
        "3.Select the Terrain GameObject and change the Pixel Error property\n" +
        "4.Observe the Tris value in the Statistics window in the Game view as you adjust the Pixel Error value\n",
        "Expected result: Tris value is changing accordingly\n" +
        "Actual result: Tris value is not updated\n",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3", false, "FAV: 2018.4, 2020.2",
        true, 3, 3, 2, tester1);

        EditorBug editor3 = new EditorBug("Editor Window loses focus when Color Picker is closed with a keybaord",
        1531214, tester1.GetName(), new AreasNGrabbags { area = "Window Management", grabbag = "Desktop Grabbag" },
        "How to reproduce:\n" +
        "1.Open user's attached 'OmgWhatIsThisBug.zip' project\n" +
        "2.Go to Window->TestUIElements and press Repro\n" +
        "3.Press Tab until you reach the Color property and press Enter\n" +
        "4.When Color Picker is opened press Enter\n" +
        "5.Press Tab two times and observe the Test window\n",
        "Expected result: Focus is on the Repro window\n" +
        "Actual result: Focus is no longer on the Repro window\n",
        "Reproducible with: 2019.4.11f1 , 2020.1.6f1, 2020.2.0b3\n" +
        "Could not test with: 2018.4.27f1(UIElements not supported)",
        false, "FAV: 2018.4.27f1, 2019.4, 2020.1, 2020.2", true, 3, 3, 2, tester1);

        EditorBug editor4 = new EditorBug("Selection pop-up windows are not focused when opened",
        1675467, tester1.GetName(), new AreasNGrabbags { area = "IMGUI", grabbag = "Editor-External Grabbag" },
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3\n",
        "How to reproduce:\n" +
        "1.Open user's attached 'UnrealEngineBetterNGL.zip' project\n" +
        "2.Select the 'Image' GameObject\n" +
        "3.In the Inspector press Add Component\n" +
        "4.When dropdown is opened enter any text\n" +
        "5.Click outside the window\n",
        "Expected result: The entered text is selected and the window is focused\n" +
        "Actual result: The entered text is not selected and the window is not focused",
        false, "FAV: 2018.4, 2020.2", true, 4, 3, 3, tester1);

        PackageBug package1 = new PackageBug("Probuilder objects receive distorted lighting in the Windows/Mac Player when using Realtime GI",
        1575123, tester1.GetName(), new AreasNGrabbags { area = "Probuilder", grabbag = "Scene Tooling Grabbag" },
        "How to reproduce:\n" +
        "1.Open user's attached 'OMG JUST FIX IT.zip' project\n" +
        "2.Open 'SampleScene' Scene\n" +
        "3.Enter Play Mode\n" +
        "4.Observe the Game view\n" +
        "5.Go to File->Build And Run\n" +
        "6.Observe the application in the Player\n",
        "Expected result: Objects are illuminated the same as in the Editor, the lighting on the objects is not distorted\n" +
        "Actual result: Objects are not illuminated the same as in the Editor, the lighting on the objects is distorted\n",
        "Reproducible with: 4.3.0-preview.6 (2019.4.2f1), 4.3.1 (2020.1.6f1), 4.4.0-preview.1 (2020.2.0b3)\n" +
        "Not reproducible with: 4.2.3(2018.4.24f1), 4.3.0 - preview.4(2019.4.2f1)",
        true, "FAV: 2019.4.4f1, 2020.2", false, 2, 2, 2, "Shader Graph", "4.3.0-preview.6", tester1);

        EditorBug editor5 = new EditorBug("Keyboard input is not detected in the Input Field when built on WebGL",
        1467154, tester1.GetName(), new AreasNGrabbags { area = "WebGL", grabbag = "WebGL Grabbag" },
        "How to reproduce:\n" +
        "1.Open user's attached 'ThisInputWow.zip' project\n" +
        "2.Go to File->Build Settings\n" +
        "3.Make sure the platform is set to WebGL\n" +
        "4.Press Build\n" +
        "5.Write some text in the Input Field in the browser\n",
        "Expected result: Text appears in the Input Field, keyboard input (eg. CTRL+V) is registered\n" +
        "Actual result: No text appears in the Input Field, keyboard input (eg.CTRL + V) is not registered\n",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
        false, "FAV: 2018.4, 2020.2", true, 3, 4, 2, tester1);

        EditorBug editor6 = new EditorBug("NullReferenceException error is thrown if a sample in package.json does not exist",
         1644567, tester2.GetName(), new AreasNGrabbags { area = "Packman", grabbag = "Packman Grabbag" },
         "How to reproduce:\n" +
         "1.Open user's attached 'PacBugs.zip' project\n" +
         "2.Go to Window -> Package Manager\n" +
         "3.Expand 'Level Creator' in the Package Manager\n" +
         "4.Select the 'Level Creator v2' package\n" +
         "5.Observe the Console log\n",
         "Expected result: No errors are thrown when selecting a package, which doesn't have 'displayName' or 'path' fields in the samples attribute in the package.json file\n" +
         "Actual result: 'NullReferenceException: Object reference not set to an instance of an object' error is thrown in the Console log when a 'displayName' field is not in the samples attribute in the package.json file\n",
         "Reproducible with: 2020.2.0a12, 2020.2.0b3\n" +
         "Not reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0a11",
         true, "FAV: 2020.2.0a12", true, 3, 3, 2, tester2);

        EditorBug editor7 = new EditorBug("Textures become black when they are converted with Graphics.ConvertTexture",
         1754664, tester2.GetName(), new AreasNGrabbags { area = "Texture", grabbag = "Graphics - Texturing and Streaming Grabbag" },
         "How to reproduce:\n" +
         "1.Open user's attached 'BugReport.zip' project\n" +
         "2.Open the Textures folder\n" +
         "3.Observe the textures in the Textures folder\n" +
         "4.Go to BugReport->Convert Textures\n" +
         "5.Open Assets / Textures_Converted folder\n" +
         "6.Observe the textures in the Textures_Converted folder\n",
         "Expected result: Converted textures have a colorful pattern as the original textures\n" +
         "Actual result: Converted textures become black\n",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 3, 3, 2, tester2);

        EditorBug editor8 = new EditorBug("Collision geometry is not applied correctly to a Tilemap when Composite Collider's 2D 'Geometry Type' is set to Polygon",
         1542164, tester2.GetName(), new AreasNGrabbags { area = "Physics2D", grabbag = "Desktop Grabbag" },
         "How to reproduce:\n" +
         "1.Open the user's attached 'TileCollisionBug.zip' project\n" +
         "2.Open the 'SampleScene' Scene(Assets folder)\n" +
         "3.Make sure the Scene view is visible\n" +
         "4.Select the 'Tilemap' GameObject in the Hierarchy window\n" +
         "5.Observe the Scene view\n",
         "Expected result: Correct collision geometry is applied to the Tilemap, no tiles are left without a collider and there are no holes in the collider\n" +
         "Actual result: Two colliders appear on the Tilemap, as the Tilemap gets wider the collider gets more inconsistent\n",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 3, 3, 2, tester2);

        EditorBug editor9 = new EditorBug("ScriptableObjects are shown as MonoBehaviours in the Inspector's Narrow Selection section",
         1670414, tester2.GetName(), new AreasNGrabbags { area = "Asset Import Pipeline", grabbag = "Asset Pipeline V2 Grabbag" },
         "How to reproduce:\n" +
         "1.Open user's attached 'inspectorSelections.zip' project\n" +
         "2.Select all Assets from the Assets folder in the Project window\n" +
         "3.Observe the Inspector window's Narrow the Selection section\n",
         "Expected result: '4 Scriptable Objects' are in the Narrow the Selection section\n" +
         "Actual result: '4 Mono Behaviours' are in the Narrow the Selection section\n",
         "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
         false, "FAV: 2018.4, 2020.2", true, 1, 3, 2, tester2);

        scenarios.Add(editor1);
        scenarios.Add(editor2);
        scenarios.Add(editor3);
        scenarios.Add(editor4);
        scenarios.Add(package1);
        scenarios.Add(editor5);
        scenarios.Add(editor6);
        scenarios.Add(editor7);
        scenarios.Add(editor8);
        scenarios.Add(editor9);

        days.Add(day, scenarios);
    }

    private void InitializeDayFour(int day)
    {
        scenarios = new List<IScenario>();

        Tester tester1 = new Tester("Joachim", "Creator", "j.creator@unity3d.com", new DateTime(2019, 4, 18), new DateTime(2020, 12, 27));
        Tester tester2 = new Tester("David", "Creator", "d.creator@unity3d.com", new DateTime(2020, 8, 18), new DateTime(2021, 1, 10));

        EditorBug editor1 = new EditorBug("Build fails with an Exception when IL2CPP Scripting Backend is selected", 1679012, tester1.GetName(), new AreasNGrabbags { area = "IL2CPP", grabbag = "VM-IL2CPP Grabbag" },
        "How to reproduce:\n" +
        "1.Open the user's attached '2011Bug.zip' project\n" +
        "2.Go to File->Build Settings\n" +
        "3.Press Build\n",
        "Expected result: Project builds successfully\n" +
        "Actual result: Exceptions are thrown in the Console log\n",
        "Reproducible with: 2020.1.0a19, 2020.1.6f1\n" +
        "Not reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.0a18, 2020.2.0b3",
        true, "FAV: 2020.1.0a19", true, 2, 3, 2, tester1);

        EditorBug editor2 = new EditorBug("Multi-selecting and overwriting Prefab instances with changes does not apply changes to their outside Prefab assets", 1791012, tester1.GetName(),
         new AreasNGrabbags { area = "Scene Management", grabbag = "Scene Management Grabbag" },
        "How to reproduce:\n" +
        "1.Open the user's attached 'Bug Project.zip' project\n" +
        "2.Type 't:boxcollider' in the Hierarchy window's search bar\n" +
        "3.Select all GameObjects in the Hierarchy window\n" +
        "4.Remove the Box Collider Component in the Inspector\n" +
        "5.Select all parent GameObjects in the Hierarchy window\n" +
        "6.Press Overrides->Apply All in the Inspector window\n" +
        "7.Observe the GameObjects in the Scene view\n",
        "Expected result: Box Collider Component was removed from all Prefab instances and from the original Prefab\n" +
        "Actual result: On some Prefab instances and on the original Prefab, the Box Collider Component was not removed\n",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1",
        false, "FAV: 2018.4, 2020.1", true, 3, 3, 2, tester1);

        EditorBug editor3 = new EditorBug("Multiple ListView's TextField items inherit the selected TextField's text when scrolling down/up in the window", 1874123, tester1.GetName(),
         new AreasNGrabbags { area = "UI Toolkit", grabbag = "UI Toolkit Grabbag" },
        "How to reproduce:\n" +
        "1.Open the attached 'Listview.zip' project\n" +
        "2.Go to Window->ListViewExampleWindow\n" +
        "3.Select any TextField\n" +
        "4.Enter 'text' in the TextField's Input Field\n" +
        "5.Scroll down with the mouse wheel or the scrollbar in the right side of the window\n" +
        "6.Observe the window\n",
        "Expected result: The selected TextField item with 'text' text stays in the top when scrolling down\n" +
        "Actual result: The selected TextField item with 'text' text keeps appearing in the window when scrolling down\n",
        "Reproducible with: 2019.4.11f1, 2020.1.6f1, 2020.2.0b3\n" +
        "Could not test with: 2018.4.27f1(UIElements are not supported)",
        false, "FAV: 2019.4, 2020.2", true, 3, 3, 2, tester1);

        PackageBug package1 = new PackageBug("Shader breaks when a % is added to an Enum keyword Entry's display name", 1781245, tester1.GetName(),
          new AreasNGrabbags { area = "ShaderGraph", grabbag = "ShaderGraph Grabbag" },
         "How to reproduce:\n" +
         "1.Open the user's attached 'Enum2020.zip' project\n" +
         "2.Press the plus sign->Keyword->Enum in the left window of the Shader Editor\n" +
         "3.Select the newly created Enum keyword\n" +
         "4.Add a plus symbol in one of the Entries display names in the Graph Inspector and press Enter\n" +
         "5.Click 'Save Asset' in the Shader Editor\n" +
         "6.Observe the Inspector\n",
         "Expected result: No warning message is thrown in the Inspector window when a symbol is added to an Enum keyword Entry's display name\n" +
         "Actual result: 'shader is not supported on this GPU (none of the subshaders/fallbacks are suitable)' warning message is thrown in the Inspector\n",
         "Reproducible with: 7.3.1 (2019.4.11f1), 8.2.0 (2020.1.6f1), 10.0.0-preview.27 (2020.2.0b3)\n" +
         "Could not test with: 4.10.0-preview (2018.4.26f1) - Enum keyword not supported",
         false, "FAV: 2019.4, 2020.2", true, 3, 3, 2, "Shader Graph", "10.0.0-preview.27", tester1);

        EditorBug editor4 = new EditorBug("Tooltip and certain buttons in the Editor cause loss of focus on top level windows when VS or VS Code", 1578978, tester1.GetName(),
         new AreasNGrabbags { area = "IMGUI", grabbag = "Editor-External Grabbag" },
        "How to reproduce:\n" +
        "1.Open the attached 'repro.zip' project\n" +
        "2.Open the 'Example.cs' script with Visual Studio\n" +
        "3.Make sure 'Attach to Unity' is selected in the Visual Studio's toolbar\n" +
        "4.Press Run in the Visual Studio\n" +
        "5.Go back to the Unity Editor\n" +
        "6.Select any GameObject in the Hierarchy window\n" +
        "7.Open another application, so Unity Editor would be out of focus\n" +
        "8.Hover on one of the selected GameObject's properties in the Inspector window\n",
        "Expected result: No tooltip is shown since Unity Editor is not in focus\n" +
        "Actual result: Tooltip appears, the opened application loses focus and now the Unity Editor is in focus\n",
        "Reproducible with: 2020.1.0a23, 2020.1.6f1, 2020.2.0b3\n" +
        "Not reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.0a22",
        true, "FAV: 2020.1.0a23, 2020.2", true, 3, 3, 2, tester1);

        EditorBug editor5 = new EditorBug("One extra item is added to the ListView when a ListView is bound to a SerializeReference List", 1679412, tester2.GetName(),
         new AreasNGrabbags { area = "UI Toolkit", grabbag = "UI Toolkit Grabbag" },
        "How to reproduce:\n" +
        "1.Open the user's attached 'bug-uielements-list-binding.zip' project\n" +
        "2.Open the 'scene' Scene\n" +
        "3.Select the 'Tests' GameObject in the Hierarchy window\n" +
        "4.Observe the Inspector\n",
        "Expected result: ListView and the bound SerializeReference List have the same amount of items\n" +
        "Actual result: ListView has 3 items while the bound SerializeRefence List has only 2 items\n",
        "Reproducible with: 2019.4.11f1, 2020.1.6f1, 2020.2.0b3\n" +
        "Could not test with: 2018.4.27f1(UIElements are not supported)",
        true, "FAV: 2019.4, 2020.2", true, 3, 3, 2, tester2);

        PackageBug package2 = new PackageBug("InvalidCastException is thrown when a project is built with Windows/Mac Player and Development Build checked", 1785354, tester2.GetName(),
          new AreasNGrabbags { area = "Addressables Assets", grabbag = "Addressables Grabbag" },
         "How to reproduce:\n" +
         "1.Open the user's attached 'AddressablesBugReport.zip' project\n" +
         "2.Go to File->Build Settings\n" +
         "3.Make sure 'Development Build' is checked\n" +
         "4.Press 'Build And Run' in the Build Settings\n" +
         "5.Observe the Development Console when the application opens\n",
         "Expected result: No error messages are thrown in the Development Console\n" +
         "Actual result: 'InvalidCastException: Specified cast is not valid.' error message is thrown in the Development Console\n",
         "Reproducible with: 1.14.2 (2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3)\n" +
         "Not reproducible with: 1.13.1(2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3)\n",
         true, "FAV: 2018.4, 2020.2", true, 4, 2, 2, "Addressables", "1.14.2", tester2);

        EditorBug editor6 = new EditorBug("Turning off VSync in Windows Player in the Application.focusChanged callback causes another callback with focus equal to true", 1456564, tester2.GetName(),
         new AreasNGrabbags { area = "Windows", grabbag = "Desktop Grabbag" },
        "How to reproduce:\n" +
        "1.Open the user's attached 'repro-focus-error.zip' project\n" +
        "2.Open the 'SampleScene' Scene\n" +
        "3.Go to File->Build And Run\n" +
        "4.When the application opens, make sure 'Toggle VSync' is checked\n" +
        "5.Click outside the application to make it out of focus\n",
        "Expected result: 'Focused' and 'VSync' text disappears and framerate text changes to 20\n" +
        "Actual result: 'Focused' and 'VSync' text doesn't disappear and framerate text doesn't change to 20 instead stays equal to the monitor's Hz\n",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
        false, "FAV: 2018.4, 2020.2", true, 3, 2, 2, tester2);

        PackageBug package3 = new PackageBug("Hide Mobile Input value is true even when unchecked in the TMPro Input Field's Control Settings", 1864123, tester2.GetName(),
          new AreasNGrabbags { area = "Text", grabbag = "John the Text Guy" },
         "How to reproduce:\n" +
         "1.Open the user's attached 'TMPReport.zip' project\n" +
         "2.Expand the 'Canvas' GameObject in the Hierarchy window\n" +
         "3.Select the 'InputField (TMP)' GameObject\n" +
         "4.Observe the TextMeshPro Input Field Component's Control Settings values in the Inspector window\n" +
         "5.Right - click on the TextMesh Pro Input Field Component\n" +
         "6.Press the 'Debug ShouldHide Fields' menu item\n" +
         "7.Observe the Console log\n",
         "Expected result: 'Hide Mobile Input' value is false\n" +
         "Actual result: 'Hide Mobile Input' value is true\n",
         "Reproducible with: 2.1.1 (2019.4.11f1), 3.0.1 (2020.1.6f1, 2020.2.0b3)\n" +
         "Not reproducible with: 1.4.0-preview.1b (2018.4.27f1)",
         true, "FAV: 2019.4, 2020.2", true, 3, 3, 2, "TextMeshPro", "3.0.1", tester2);

        EditorBug editor7 = new EditorBug("OnApplicationQuit method is called before Application.wantsToQuit event is raised", 1785123, tester2.GetName(),
         new AreasNGrabbags { area = "Scripting", grabbag = "Scripting Grabbag" },
        "How to reproduce:\n" +
        "1.Open user's attached 'quit_test.zip' project\n" +
        "2.Open 'SampleScene' Scene\n" +
        "3.Enter Play Mode\n" +
        "4.Exit Play Mode\n" +
        "5.Observe the Console log\n",
        "Expected result: Application.wantsToQuit event is raised before OnApplicationQuit\n" +
        "Actual result: OnApplicationQuit is called before Application.wantsToQuit event is raised\n",
        "Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3",
        false, "FAV: 2018.4, 2020.2", true, 3, 3, 2, tester2);

        scenarios.Add(editor1);
        scenarios.Add(editor2);
        scenarios.Add(editor3);
        scenarios.Add(package1);
        scenarios.Add(editor4);
        scenarios.Add(editor5);
        scenarios.Add(package2);
        scenarios.Add(editor6);
        scenarios.Add(package3);
        scenarios.Add(editor7);

        days.Add(day, scenarios);
    }
}
