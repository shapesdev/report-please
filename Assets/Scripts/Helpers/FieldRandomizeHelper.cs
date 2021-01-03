using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldRandomizeHelper : MonoBehaviour
{
    public static FieldRandomizeHelper instance;
    public RuleBookSO rulebook;
    private string[] names = new string[13] {"Liam", "Noah", "Oliver", "William", "Jori", "Gerard",
    "Ramoj", "Gabriel", "Justin", "Aurimas", "Kasparas", "Kristinas", "Donny"};
    private string[] surNames = new string[13] {"Smith", "Johnson", "Brown", "Garcia", "Miller", "Davis",
    "Martinez", "Lopez", "Gonzalez", "Taylor", "Jackson", "Moore", "Lee"};
    private List<int> numbers = new List<int>();

    private void Start()
    {
        instance = this;
    }

    public Dictionary<DateTime, List<IScenario>> RandomizeScenarios(List<IScenario> bugs, DateTime day, List<Discrepancy> discrepancies)
    {
        List<IScenario> bugList = new List<IScenario>();

        for (int i = 0; i < 10; i++)
        {
            var rnd = UnityEngine.Random.Range(0, bugs.Count - 1);
            if (!bugList.Contains(bugs[rnd]))
            {
                bugList.Add(bugs[rnd]);
                bugs.Remove(bugs[rnd]);
            }
        }
        foreach (var bug in bugList)
        {
            var currentBug = (Bug)bug;
            var tester = GetRandomizedTester();
            var rndDiscrepancy = UnityEngine.Random.Range(0, discrepancies.Count - 1);

            currentBug.SetTester(tester);
            currentBug.SetTesterName(tester.GetName());
            SetDiscrepancy(discrepancies[rndDiscrepancy], currentBug);
        }

        var days = new Dictionary<DateTime, List<IScenario>>();
        days.Add(day, bugList);

        return days;
    }

    private Tester GetRandomizedTester()
    {
        var name = names[(UnityEngine.Random.Range(0, 12))];
        var surname = surNames[(UnityEngine.Random.Range(0, 12))];
        var id = UnityEngine.Random.Range(0, 16);
        if(numbers.Contains(id))
        {
            id -= 1;
        }
        var tester = new Tester(id, name, surname, name + "." + surname + "@diversity.com", new DateTime(2020, 11, 14), new DateTime(2021, 2, 14), "Here you go");
        return tester;
    }

    private void SetDiscrepancy(Discrepancy type, Bug bug)
    {
        if(type.GetFirstTag() == "Name" && type.GetSecondTag() == "Name")
        {
            bug.SetTesterName("John");
            bug.SetDiscrepancy(new Discrepancy("Name", "Name",
                new Dialogue("Tester name appears to be wrong", "That can't be true")));
        }
        else if(type.GetFirstTag() == "FAV" && type.GetSecondTag() == "Regression")
        {
            bug.SetRegression(!bug.IsRegression());
            bug.SetDiscrepancy(new Discrepancy("FAV", "Regression",
            new Dialogue("You have wrong Regression field", "No, its correct")));
        }
        else if(type.GetFirstTag() == "ReproSteps" && type.GetSecondTag() == "OrderRule")
        {
            var tempReproSteps = bug.GetReproSteps();
            var tempExpected = bug.GetExpectedActualResults();

            bug.SetReproSteps(tempReproSteps);
            bug.SetExpectedActual(tempExpected);
            bug.SetDiscrepancy(new Discrepancy("ReproSteps", "OrderRule",
            new Dialogue("Your Repro steps are in wrong order", "Oh, I forgot to fix it..")));
        }
        else if(type.GetFirstTag() == "Public")
        {
            bug.SetReportToPrivate();
            bug.SetDiscrepancy(new Discrepancy("Public", "PublicRule",
            new Dialogue("Your Report is marked as Private", "Is that bad?")));
        }
        else if(type.GetFirstTag() == "Title")
        {
            if(bug.GetPlatformImportance() >= 3)
            {
                bug.SetPlatFormImportance(1);
            }
            else
            {
                bug.SetPlatFormImportance(4);
            }
            bug.SetDiscrepancy(new Discrepancy("Title", "Platform",
            new Dialogue("Wrong Platform Importance is set in your Report", "How is it wrong?")));
        }
        else if(type.GetFirstTag() == "ReproWith" && type.GetSecondTag() == "NotAllVersionRule")
        {
            if(bug.IsRegression() == false)
            {
                bug.SetReproStepsWith("Reproducible with: 2020.1.0a15, 2020.1.6f1\n" +
                "Not reproducible with: 2019.4.11f1, 2020.1.0a14, 2020.2.0b3");
                bug.SetDiscrepancy(new Discrepancy("ReproWith", "NotAllVersionRule", new Dialogue("You have not tested all versions", "My mistake")));
            }
            else
            {
                bug.SetReproStepsWith("Reproducible with: 2018.4.27f1, 2019.4.11f1, 2020.1.6f1, 2020.2.0b3");
                bug.SetDiscrepancy(new Discrepancy("ReproWith", "NotAllVersionRule", new Dialogue("You have not tested all versions", "Oh, my bad")));
            }
        }
    }
}
