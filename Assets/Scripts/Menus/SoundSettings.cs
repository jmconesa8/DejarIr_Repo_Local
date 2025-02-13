using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [ContextMenu("Set firstTime to 0")]
    void SetFirstTimeTo0()
    {
        PlayerPrefs.SetInt("firstTime", 0);
    }
    [Header("Debug only || don't touch")]
    public int firstTime;

    private GameObject[] themes;
    private GameObject[] fx;

    [Space(10)]
    [Header("Sound")]
    public Slider themeVolumeSlider;
    public Slider fxVolumeSlider;

    public float themeVolume = 1f;
    public float fxVolume = 1f;

    void Start()
    {
        themes = GameObject.FindGameObjectsWithTag("Theme");
        fx = GameObject.FindGameObjectsWithTag("FX");

        themeVolume = PlayerPrefs.GetFloat("themeVolume");
        fxVolume = PlayerPrefs.GetFloat("fxVolume");

        if (PlayerPrefs.GetInt("firstTime") != 1)
        {
            SetVolumeFirstTime();
        }
        else
        {
            Debug.Log("Game has been opened before");
        }

        for (int i = 0; i < themes.Length; i++)
        {
            themes[i].GetComponent<AudioSource>().volume = themeVolume;
        }
        for (int i = 0; i < fx.Length; i++)
        {
            fx[i].GetComponent<AudioSource>().volume = fxVolume;
        }

        if (themeVolumeSlider != null)
        {
            themeVolumeSlider.value = themeVolume;
        }
        if (fxVolumeSlider != null)
        {
            fxVolumeSlider.value = fxVolume;
        }
    }

    void Update()
    {
        PlayerPrefs.SetFloat("themeVolume", themeVolume);
        PlayerPrefs.SetFloat("fxVolume", fxVolume);

        for (int i = 0; i < themes.Length; i++)
        {
            themes[i].GetComponent<AudioSource>().volume = themeVolume;
        }
        for (int i = 0; i < fx.Length; i++)
        {
            fx[i].GetComponent<AudioSource>().volume = fxVolume;
        }

        // Debug only, delete later
        //firstTime = PlayerPrefs.GetInt("firstTime");
    }

    public void ThemeVolumeUpdater(float volume)
    {
        themeVolume = volume;
    }
    public void FXVolumeUpdater(float volume)
    {
        fxVolume = volume;
    }

    public void SetVolumeFirstTime()
    {
        if (fxVolumeSlider != null)
        { 
            fxVolume = fxVolumeSlider.maxValue;
        }
        if (themeVolumeSlider != null)
        {
            themeVolume = themeVolumeSlider.maxValue;
        }

        PlayerPrefs.SetInt("firstTime", 1);
    }
}
