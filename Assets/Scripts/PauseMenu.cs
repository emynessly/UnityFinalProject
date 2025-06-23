using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioMixer soundMixer;
    [SerializeField] private AudioMixer musicMixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
        soundSlider.value = 0.5f;
        musicSlider.value = 0.5f;
        soundMixer.SetFloat("SoundVolume", -40);
        musicMixer.SetFloat("MusicVolume", -40);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                 PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void onChangeSoundSlider()
    {
        float value = soundSlider.value;
        if (value == 0)
            soundMixer.SetFloat("SoundVolume", -80);
        else
            soundMixer.SetFloat("SoundVolume", Mathf.Log10(value) * 20);
    }
    public void onChangeMusicSlider()
    {
        float value = musicSlider.value;
        if (value == 0)
            soundMixer.SetFloat("MusicVolume", -80);
        else
            musicMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
}
