using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour, ICharacterView
{
    public PlayableDirector characterWalkInDirector;
    public PlayableDirector characterWalkingDirector;
    public PlayableDirector characterWalkOutDirector;

    public void ShowTesterCharacter(IScenario scenario, Sprite sprite, int current, int last,
        IGameSelectionView selectionView, DateTime day, Discrepancy discrepancy)
    {
        var currentSprite = characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite;

        if (currentSprite == null)
        {
            characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
            characterWalkOutDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;

            StartCoroutine(DisplayCharacter(true, false, selectionView, day, scenario, sprite));
        }
        else if (currentSprite != sprite)
        {
            StartCoroutine(DisplayCharacter(true, true, selectionView, day, scenario, sprite));
            characterWalkInDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
        }
        else
        {
            StartCoroutine(DisplayCharacter(false, false, selectionView, day, scenario, sprite));
        }
    }

    IEnumerator DisplayCharacter(bool walkIn, bool walkOut, IGameSelectionView selectionView,
    DateTime day, IScenario scenario, Sprite sprite)
    {
        while (true)
        {
            if (walkIn)
            {
                if (walkOut)
                {
                    characterWalkInDirector.transform.GetChild(0).gameObject.SetActive(false);
                    characterWalkOutDirector.Play();
                    yield return new WaitForSeconds(1.3f);
                    characterWalkOutDirector.gameObject.GetComponentInChildren<Image>(true).sprite = sprite;
                }

                var animator = characterWalkInDirector.gameObject.GetComponentInChildren<Animator>(true);
                if (animator.enabled == false) { animator.enabled = true; }

                characterWalkingDirector.Play();
                yield return new WaitForSeconds(2f);
                characterWalkInDirector.Play();
                yield return new WaitForSeconds(2f);
                animator.enabled = false;
            }

            if (characterWalkOutDirector.transform.GetChild(0).gameObject.activeInHierarchy)
            {
                characterWalkOutDirector.transform.GetChild(0).gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(2f);

            if (day.Day == 10 || scenario.IsEmployeeIdMissing() == true) { selectionView.ActivateSelectable(0, 1); }
            else { selectionView.ActivateSelectable(0, 2); }

            break;
        }
    }
}
