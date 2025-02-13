using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Localization.Settings;

public class MenuScript : MonoBehaviour
{
    // This script goes on ---Menus--- GameObject which should always be active
    // Script has save system

    [Header("Pause Menu / Main Menu")]
    [Tooltip("Assign Pause Menu or Main Menu if its Main Menu")]
    public GameObject pauseMenu;

    [Tooltip("The default button selected on respective Menu")]
    public GameObject pauseMenuDefaultButton;

    [Tooltip("Only true if its Main Menu")]
    public bool isMainMenu;

    [Header("Settings Menu")]
    public GameObject settingsMenu;

    [Tooltip("The default button selected on respective Menu")]
    public GameObject settingsMenuDefaultButton;

    [Tooltip("The default button selected on respective Menu")]
    public GameObject extrasMenuDefaultButton;

    [Header("Credits Menu")]
    public GameObject creditsMenu;

    [Tooltip("The default button selected on respective Menu")]
    public GameObject creditsMenuDefaultButton;

    [Header("Save Game Logo")]
    public GameObject saveGameLogo;

    [Header("Debug || Don't touch")]
    public int savedScene;
    public float selectedLocale;

    [Header("Player | StarterAssetsInputs")]
    public GameObject player;
    private StarterAssetsInputs starterAssetsInputs;
    private PlayerInput playerInput;
    public EventSystem eventSystem;

    //Private variables
    private bool crBool;
    public void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        // Find Player
        if (!isMainMenu)
        {
            player = GameObject.Find("PlayerCapsule");
        }
        if (player == null)
        {
            Debug.Log("MENU Couldnt find player");
        }

        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        if (eventSystem == null)
            Debug.Log("MENU Couldnt find EventSystem");
        if (isMainMenu == false)
        {
            starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
            playerInput = player.GetComponent<PlayerInput>();
        }

        // Save when on a new scene that isn't the main menu
        if (SceneManager.GetActiveScene() != null)
        {
            if (!isMainMenu)
            {
                SaveGame();
                ShowSaveGameLogo();
            }
        }

        //Localization
        PlayerPrefs.GetFloat("language");

        if (PlayerPrefs.GetFloat("language") == 0)
        {
            StartCoroutine(SetEnglish());
        }
        else if (PlayerPrefs.GetFloat("language") == 1)
        {
            StartCoroutine(SetEspanol());
        }
        else if (PlayerPrefs.GetFloat("language") == 2)
        {
            StartCoroutine(SetFrancais());
        }
        else if (PlayerPrefs.GetFloat("language") == 3)
        {
            StartCoroutine(SetDeutsch());
        }
        else if (PlayerPrefs.GetFloat("language") == 4)
        {
            StartCoroutine(SetPortuguese());
        }
        else if (PlayerPrefs.GetFloat("language") == 5)
        {
            StartCoroutine(SetChineseSimplified());
        }
        else if (PlayerPrefs.GetFloat("language") == 6)
        {
            StartCoroutine(SetRussian());
        }
        else if (PlayerPrefs.GetFloat("language") == 7)
        {
            StartCoroutine(SetJapanese());
        }
        else if (PlayerPrefs.GetFloat("language") == 8)
        {
            StartCoroutine(SetKorean());
        }
        else if (PlayerPrefs.GetFloat("language") == 9)
        {
            StartCoroutine(SetPolish());
        }
        else if (PlayerPrefs.GetFloat("language") == 10)
        {
            StartCoroutine(SetChineseTraditional());
        }
        else if (PlayerPrefs.GetFloat("language") == 11)
        {
            StartCoroutine(SetItalian());
        }

        // Debug only || Show saved scene in the menu & current language
        savedScene = PlayerPrefs.GetInt("Scene");
        selectedLocale = PlayerPrefs.GetFloat("language");
    }

    public void Update()
    {
        if (isMainMenu == false)
        {
            // Decomment before build
            if (starterAssetsInputs.pause || Input.GetKeyDown(KeyCode.I))
            {
                starterAssetsInputs.pause = false;
                playerInput.SwitchCurrentActionMap("UI");
                Debug.Log("MainMenu script current Action Map = UI");
                ShowCurrentActionMapLog();
                OpenMainMenu();
            }
        }
        else
        {
            if (!crBool)
            {
                DebugLogMainMenu();
            }
        }

        if (eventSystem.currentSelectedGameObject == null)
        {
            EventSystemSetSelectedGameObject();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        EventSystemSetSelectedGameObject();
    }

    public void OpenExtrasMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        creditsMenu.SetActive(false);
        EventSystemSetSelectedGameObject();
    }

    public void OpenCreditsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        creditsMenu.SetActive(true);
        EventSystemSetSelectedGameObject();
    }

    public void OpenMainMenu()
    {
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        EventSystemSetSelectedGameObject();
    }

    public void EventSystemSetSelectedGameObject()
    {
        //if(eventSystem.currentSelectedGameObject == null)
        if (pauseMenu != null && pauseMenu.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(pauseMenuDefaultButton);
        }
        else if (settingsMenu != null && settingsMenu.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(settingsMenuDefaultButton);
        }
        else if (creditsMenu != null && creditsMenu.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(creditsMenuDefaultButton);
        }
    }

    public void Pause()
    {
        playerInput.SwitchCurrentActionMap("UI");
        Debug.Log("MainMenu script current Action Map = UI");
        ShowCurrentActionMapLog();
        OpenMainMenu();
        EventSystemSetSelectedGameObject();
        //Time.timeScale = 0;
    }

    public void Return()
    {
        //Time.timeScale = 1;
        playerInput.SwitchCurrentActionMap("Player");
        Debug.Log("MainMenu script current Action Map = Player");
        ShowCurrentActionMapLog();
        pauseMenu.SetActive(false);
        Debug.Log("Menu closed");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }

    public void ShowSaveGameLogo()
    {
        StartCoroutine(SaveLogo());
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
    }

    public void DeleteSaveGame()
    {
        PlayerPrefs.SetInt("Scene", 1);
    }

    public void CursorVisibilityGamepad()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void ShowCurrentActionMapLog()
    {
        Debug.Log(" " + playerInput.currentActionMap);
        Debug.Log(" " + playerInput.currentControlScheme);
    }

    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution720()
    {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
    }

    public void SetResolution1080()
    {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
    }

    public void SetResolution1440()
    {
        Screen.SetResolution(2560, 1440, Screen.fullScreen);
    }

    public void SetResolution4K()
    {
        Screen.SetResolution(3840, 2160, Screen.fullScreen);
    }

    public void SetResolutionSteamDeck()
    {
        Screen.SetResolution(1280, 800, Screen.fullScreen);
    }
    public IEnumerator SaveLogo()
    {
        saveGameLogo.SetActive(true);

        yield return new WaitForSeconds(2f);

        saveGameLogo.SetActive(false);
    }

    public void English()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        PlayerPrefs.SetFloat("language", 0);
    }
    public void Espanol()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        PlayerPrefs.SetFloat("language", 1);
    }
    public void Francais()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        PlayerPrefs.SetFloat("language", 2);
    }
    public void Deutsch()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
        PlayerPrefs.SetFloat("language", 3);
    }
    public void Portuguese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[4];
        PlayerPrefs.SetFloat("language", 4);
    }
    public void Polish()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[9];
        PlayerPrefs.SetFloat("language", 9);
    }
    public void Italian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[11];
        PlayerPrefs.SetFloat("language", 11);
    }
    public void Russian()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[6];
        PlayerPrefs.SetFloat("language", 6);
    }
    public void Japanese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[7];
        PlayerPrefs.SetFloat("language", 7);
    }
    public void Korean()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[8];
        PlayerPrefs.SetFloat("language", 8);
    }
    public void SimpleChinese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[5];
        PlayerPrefs.SetFloat("language", 5);
    }
    public void TradChinese()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[10];
        PlayerPrefs.SetFloat("language", 10);
    }

    // Corroutines
    public IEnumerator SetEnglish()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
    }
    public IEnumerator SetEspanol()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
    }
    public IEnumerator SetFrancais()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
    }
    public IEnumerator SetDeutsch()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
    }
    public IEnumerator SetPortuguese()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[4];
    }
    public IEnumerator SetChineseSimplified()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[5];
    }
    public IEnumerator SetRussian()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[6];
    }
    public IEnumerator SetJapanese()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[7];
    }
    public IEnumerator SetKorean()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[8];
    }
    public IEnumerator SetPolish()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[9];
    }
    public IEnumerator SetChineseTraditional()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[10];
    }
    // Mamma mia pizza
    public IEnumerator SetItalian()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[11];
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
    public void DebugLogMainMenu()
    {
        crBool = true;
        Debug.Log("MainMenu bool is active");
    }
}
