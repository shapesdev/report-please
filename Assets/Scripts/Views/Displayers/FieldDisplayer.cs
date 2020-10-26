using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FieldDisplayer : MonoBehaviour
{
    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text description;
    [SerializeField]
    TMP_Text[] options;

    public void DisplayFields(ReportFieldInfo info)
    {
        title.text = info.field;
        description.text = info.description;

        if(options.Length == info.info.Length && options.Length > 0)
        {
            for(int i = 0; i < options.Length; i++)
            {
                options[i].text = info.info[i].level + " - " + info.info[i].name;
                options[i].gameObject.GetComponent<FieldData>().SetData(info.info[i].name);
            }
        }
        gameObject.SetActive(true);
    }

    public void TurnOnInspectorMode()
    {
        title.raycastTarget = true;
        description.raycastTarget = true;

        if(options.Length > 0)
        {
            foreach (var option in options)
            {
                option.raycastTarget = true;
            }
        }
    }

    public void TurnOffInspectorMode()
    {
        title.raycastTarget = false;
        description.raycastTarget = false;

        if (options.Length > 0)
        {
            foreach (var option in options)
            {
                option.raycastTarget = false;
            }
        }
    }
}
