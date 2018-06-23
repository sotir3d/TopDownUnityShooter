using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;

public class MainMenuScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionsDropdown;
    public Dropdown graphicsDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;

    public GameObject startPanel;
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject levelsPanel;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Resolution[] resolutions;

    FullScreenMode fullScreenMode;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        startPanel.SetActive(true);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        levelsPanel.SetActive(false);

        resolutions = Screen.resolutions;
        
        fullscreenToggle.isOn = Screen.fullScreen;

        ToggleFullscreen(Screen.fullScreen);

        for (int i = 0; i < resolutions.Length; i++)
        {
            resolutionsDropdown.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
                resolutionsDropdown.value = i;

            resolutionsDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resolutionsDropdown.value].width, resolutions[resolutionsDropdown.value].height, fullScreenMode); });
        }

        //when returning to the main menu from another level, set all UI elements to its current values
        graphicsDropdown.value = QualitySettings.GetQualityLevel();


        float audioMixerVolume = 0;

        audioMixer.GetFloat("volume", out audioMixerVolume);
        volumeSlider.value = audioMixerVolume;
    }

    string ResToString(Resolution res)
    {
        return res.width + " x " + res.height + " @ " + res.refreshRate + " Hz";
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

        Screen.SetResolution(resolutions[resolutionsDropdown.value].width, resolutions[resolutionsDropdown.value].height, fullScreenMode);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);

    }

    void DisableAllPanels()
    {
        startPanel.SetActive(false);
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void ToStartScreen()
    {
        DisableAllPanels();
        startPanel.SetActive(true);
    }

    public void ToSettingsScreen()
    {
        DisableAllPanels();
        settingsPanel.SetActive(true);
    }

    public void ToControlsScreen()
    {
        DisableAllPanels();
        controlsPanel.SetActive(true);
    }

    public void ToLevelsScreen()
    {
        DisableAllPanels();
        levelsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
