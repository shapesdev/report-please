using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuView : MonoBehaviour, IMenuView
{
    [SerializeField]
    private GameObject titleScreen;
    [SerializeField]
    private GameObject mainMenuScreen;
    [SerializeField]
    private GameObject optionsPanel;
    [SerializeField]
    private GameObject continuePanel;
    [SerializeField]
    private Text currentDayText;

    [SerializeField]
    private AudioMixer soundMixer;
    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Slider musicSlider;

    public event EventHandler<StoryButtonPressedEventArgs> OnStoryButtonPressed = (sender, e) => { };
    public event EventHandler<EndlessButtonPressedEventArgs> OnEndlessButtonPressed = (sender, e) => { };

    private void Start()
    {
        if(PlayerPrefs.GetFloat("MusicVol") != 0)
        {
            soundSlider.value = PlayerPrefs.GetFloat("SoundVol");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");

            // Formule taikoma norint paversti reikšmes i decibelų reikšmę
            soundMixer.SetFloat("SoundVol", Mathf.Log10(soundSlider.value) * 20);
            musicMixer.SetFloat("MusicVol", Mathf.Log10(musicSlider.value) * 20);
        }
    }

    public void Play()
    {
        if(titleScreen.activeInHierarchy)
        {
            titleScreen.SetActive(false);
            mainMenuScreen.SetActive(true);
        }
    }

    public void PlayStory()
    {
        var eventArgs = new StoryButtonPressedEventArgs();
        OnStoryButtonPressed(this, eventArgs);
    }

    public void PlayEndless()
    {
        var eventArgs = new EndlessButtonPressedEventArgs();
        OnEndlessButtonPressed(this, eventArgs);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowContinuePanel(DateTime time)
    {
        continuePanel.SetActive(true);
        currentDayText.text = "LAST SAVED DAY: " + time.ToString("yyyy MM dd");
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void StartNew()
    {
        PlayerPrefs.DeleteAll();
        PlayerData.instance.ClearPlayerData();
        DataExportHelper.instance.ResetFile();
        App.instance.LoadStoryGame();
    }

    public void Continue()
    {
        App.instance.LoadStoryGame();
    }

    public void CloseContinuePanel()
    {
        continuePanel.SetActive(false);
    }

    public void ChangeSoundVolume(float sliderValue)
    {
        soundMixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SoundVol", sliderValue);
    }

    public void ChangeMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
    }
}
