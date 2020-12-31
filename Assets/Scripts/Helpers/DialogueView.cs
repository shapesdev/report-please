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

    public void ShowDialogue(string inspectorWords, string testerWords, float delay)
    {
        StartCoroutine(DisplayDialogue(inspectorWords, testerWords, delay));
    }

    IEnumerator DisplayDialogue(string inspector, string tester, float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            if (inspector != "")
            {
                inspectorWordsGO.transform.GetChild(0).gameObject.GetComponent<Text>().text = inspector;
                inspectorWordsGO.SetActive(true);
                OnInspectorSpeak?.Invoke();
            }
            yield return new WaitForSeconds(1f);
            if(tester != "")
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
