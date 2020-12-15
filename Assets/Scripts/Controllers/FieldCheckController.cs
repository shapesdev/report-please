using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldCheckController
{
    public Tuple<bool, bool> CheckFields(GameObject field1, GameObject field2, List<Discrepancy> discrepancies, Discrepancy discrepancy)
    {
        bool correlation = false;
        bool discrepancyFound = false;

        foreach(var disc in discrepancies)
        {
            if((disc.GetFirstTag() == field1.tag && disc.GetSecondTag() == field2.tag) || (disc.GetFirstTag() == field2.tag && disc.GetSecondTag() == field1.tag))
            {
                if (field1.tag == "Areas" || field2.tag == "Areas")
                {
                    var text1 = field1.GetComponent<TMP_Text>().text;
                    var text2 = field2.GetComponent<TMP_Text>().text;

                    if (field1.tag == "Areas")
                    {
                        if (text1.Contains(text2))
                        {
                            correlation = true;
                            break;
                        }
                    }
                    else
                    {
                        if (text2.Contains(text1))
                        {
                            correlation = true;
                            break;
                        }
                    }
                }
                else
                {
                    correlation = true;
                    break;
                }
            }
            else if (disc.GetFirstTag() == "FAV" || disc.GetSecondTag() == "FAV" && discrepancy != null)
            {
                if ((field1.tag == "FAV" && field2.tag == "ReproWith") || (field2.tag == "FAV" && field1.tag == "ReproWith") ||
                    (field1.tag == "Regression" && field2.tag == "ReproWith") || (field1.tag == "ReproWith" && field2.tag == "Regression"))
                {
                    correlation = true;
                    Debug.Log(correlation);
                }
            }
        }

        if(correlation && discrepancy != null)
        {
            if ((discrepancy.GetFirstTag() == field1.tag && discrepancy.GetSecondTag() == field2.tag) ||
                (discrepancy.GetFirstTag() == field2.tag && discrepancy.GetSecondTag() == field1.tag))
            {
                discrepancyFound = true;
            }
            else if ((field1.tag == "FAV" && field2.tag == "ReproWith") || (field2.tag == "FAV" && field1.tag == "ReproWith") ||
        (field1.tag == "Regression" && field2.tag == "ReproWith") || (field1.tag == "ReproWith" && field2.tag == "Regression"))
            {
                discrepancyFound = true;
            }
        }

        return Tuple.Create(correlation, discrepancyFound);
    }
}
