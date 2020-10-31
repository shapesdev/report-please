using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip[] musicClips;
    [SerializeField]
    private SoundEffectsSO soundEffects;

    private void OnEnable()
    {
        GameController.OnGameInitialized += PlayMusic;
        GameController.OnInspectorMode += PlayInspectorSound;
        GameController.OnDiscrepancy += PlayDiscrepancySound;
        GameController.OnCitation += PlayCitationSound;

        MenuController.OnMenuInitialized += PlayMusic;

        GameStampView.OnStampPanelOpen += PlayStampPanelSound;
        GameStampView.OnStampPlaced += PlayStampSound;

        GameSelectionView.OnPaperDrag += PlayPaperDragSounds;

        LineController.OnHighlight += PlayHighlightSounds;

        RuleBookView.OnTurnPage += PlayTurnPageSound;
        AreasDisplayer.OnTurnPage += PlayTurnPageSound;
        ReportFieldsDisplayer.OnTurnPage += PlayTurnPageSound;
    }

    private void OnDisable()
    {
        GameController.OnGameInitialized -= PlayMusic;
        GameController.OnInspectorMode -= PlayInspectorSound;
        GameController.OnDiscrepancy -= PlayDiscrepancySound;
        GameController.OnCitation -= PlayCitationSound;

        MenuController.OnMenuInitialized -= PlayMusic;

        GameStampView.OnStampPanelOpen -= PlayStampPanelSound;
        GameStampView.OnStampPlaced -= PlayStampSound;

        GameSelectionView.OnPaperDrag -= PlayPaperDragSounds;

        LineController.OnHighlight -= PlayHighlightSounds;

        RuleBookView.OnTurnPage -= PlayTurnPageSound;
        AreasDisplayer.OnTurnPage -= PlayTurnPageSound;
        ReportFieldsDisplayer.OnTurnPage -= PlayTurnPageSound;
    }

    private void PlayMusic(int value)
    {
        musicSource.clip = musicClips[value];
        musicSource.Play();
    }

    private void PlayHighlightSounds(int value)
    {
        soundSource.clip = soundEffects.highlightSounds[value];
        soundSource.Play();
    }

    private void PlayStampSound(bool value)
    {
        if(value == false)
        {
            StartCoroutine(StampSoundPlayer());
        }
        else
        {
            soundSource.clip = soundEffects.stampPlaceSounds[0];
            soundSource.Play();
        }
    }

    private IEnumerator StampSoundPlayer()
    {
        while(true)
        {
            yield return null;

            soundSource.clip = soundEffects.stampPlaceSounds[0];
            soundSource.Play();

            yield return new WaitForSeconds(soundSource.clip.length + 0.1f);

            soundSource.clip = soundEffects.stampPlaceSounds[1];
            soundSource.Play();

            break;
        }
    }

    private void PlayStampPanelSound(int value)
    {
        soundSource.clip = soundEffects.stampOpenSounds[value];
        soundSource.Play();
    }

    private void PlayInspectorSound(int value)
    {
        soundSource.clip = soundEffects.inspectorSounds[value];
        soundSource.Play();
    }

    private void PlayCitationSound()
    {
        soundSource.clip = soundEffects.citationSound;
        StartCoroutine(CitationSoundPlayer());
    }

    private IEnumerator CitationSoundPlayer()
    {
        while(true)
        {
            yield return null;
            soundSource.Play();

            yield return new WaitForSeconds(soundSource.clip.length);
            soundSource.Play();

            yield return new WaitForSeconds(soundSource.clip.length);
            soundSource.Play();

            break;
        }
    }

    private void PlayDiscrepancySound()
    {
        soundSource.clip = soundEffects.discrepancySound;
        soundSource.Play();
    }

    private void PlayPaperDragSounds(int value)
    {
        soundSource.clip = soundEffects.paperDragSounds[value];
        soundSource.Play();
    }
    private void PlayTurnPageSound()
    {
        soundSource.clip = soundEffects.turnPageSound;
        soundSource.Play();
    }
}
