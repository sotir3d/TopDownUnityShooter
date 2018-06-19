using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionsDropdown;
    public Dropdown graphicsDropdown;
    public Toggle fullscreenToggle;

    public GameObject startPanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;

    Resolution[] resolutions;

    FullScreenMode fullScreenMode;

    private void Start()
    {
        startPanel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        graphicsDropdown.value = QualitySettings.GetQualityLevel();
        fullscreenToggle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionsDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            resolutionsDropdown.value = i;

            resolutionsDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resolutionsDropdown.value].width, resolutions[resolutionsDropdown.value].height, fullScreenMode); });

        }
        
        ToggleFullscreen(Screen.fullScreen);

    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height;
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
            fullScreenMode = FullScreenMode.FullScreenWindow;
        else
            fullScreenMode = FullScreenMode.Windowed;

        //Screen.SetResolution(resolutions[resolutionsDropdown.value].width, resolutions[resolutionsDropdown.value].height, fullScreenMode);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);

    }

    public void ToStartScreen()
    {
        startPanel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ToSettingsScreen()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ToControlsScreen()
    {
        startPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
