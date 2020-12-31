using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueView : MonoBehaviour, IDialogueView
{
    public GameObject inspectorWordsGO;
    public GameObject testerWordsGO;

    public static event Action OnInspectorSpeak;
    public static event Action OnTesterSpeak;

    public void ShowDiscrepancyDialogue(string inspectorWords, string testerWords)
    {
        StartCoroutine(DisplayDialogue(2f, inspectorWords, testerWords));
    }

    IEnumerator DisplayDialogue(float delay, string inspector, string tester)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (inspector != null)
            {
                inspectorWordsGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = inspector;
                inspectorWordsGO.SetActive(true);
                OnInspectorSpeak?.Invoke();
            }
            yield return new WaitForSeconds(1f);
            if(tester != null)
            {
                testerWordsGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = tester;
                testerWordsGO.SetActive(true);
                OnTesterSpeak?.Invoke();
            }
            yield return new WaitForSeconds(3f);
            inspectorWordsGO.SetActive(false);
            testerWordsGO.SetActive(false);

            break;
        }
    }
}
