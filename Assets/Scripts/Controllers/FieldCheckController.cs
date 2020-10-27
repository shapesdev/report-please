using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldCheckController
{
    public Tuple<bool, bool> CheckFields(GameObject field1, GameObject field2, List<Discrepancy> discrepancies, Discrepancy discrepancy, RuleBookSO ruleBook)
    {
        bool correlation = false;
        bool discrepancyFound = false;

        foreach(var disc in discrepancies)
        {
            if((disc.firstTag == field1.tag && disc.secondTag == field2.tag) || (disc.firstTag == field2.tag && disc.secondTag == field1.tag))
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
        }

        if(correlation && discrepancy != null)
        {
            if ((discrepancy.firstTag == field1.tag && discrepancy.secondTag == field2.tag) ||
                (discrepancy.firstTag == field2.tag && discrepancy.secondTag == field1.tag))
            {
                if(field1.tag == "Regression" || field2.tag == "Regression")
                {
                    var fav = field1.tag == "FAV" ? field1.GetComponent<TMP_Text>().text : field2.GetComponent<TMP_Text>().text;
                    var regression = field2.tag == "Regression" ? field2.GetComponent<TMP_Text>().text : field1.GetComponent<TMP_Text>().text;

                    bool containsAVersion = false;

                    foreach (var stream in ruleBook.versionInfo)
                    {
                        for (int i = 0; i < stream.versions.Length; i++)
                        {
                            if (fav.Contains(stream.versions[i]))
                            {
                                containsAVersion = true;
                                break;
                            }
                        }
                    }

                    if (regression.Contains("Yes"))
                    {
                        if (containsAVersion == false)
                        {
                            discrepancyFound = true;
                        }
                    }
                    if (regression.Contains("No"))
                    {
                        if (containsAVersion == true)
                        {
                            discrepancyFound = true;
                        }
                    }
                }
                else
                {
                    discrepancyFound = true;
                }
            }
        }

        return Tuple.Create(correlation, discrepancyFound);
    }
}
