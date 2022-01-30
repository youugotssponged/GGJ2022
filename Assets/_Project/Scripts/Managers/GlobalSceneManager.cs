using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalSceneManager : MonoBehaviour
{
    public enum SceneManagerState : int
    {
        SPLASHSCREEN = 0,
        MAINMENU,
        LEVEL_DENIAL,
        LEVEL_ANGER,
        LEVEL_DEPRESSION,
        LEVEL_ACCEPTANCE,
        CREDITS,
        LOADINGSCREEN,
        SETTINGS,
        INSTRUCTIONS,
    }

    public SceneManagerState CurrentSceneManagerState; 

    public static GlobalSceneManager _Instance;
    public static event Action<SceneManagerState> OnSceneChanged;

    private void Awake()
    {
        _Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // FOR DEMO
        if(Input.GetKeyDown(KeyCode.Alpha7))
            UpdateSceneManagerState(SceneManagerState.LEVEL_DENIAL);
        if(Input.GetKeyDown(KeyCode.Alpha8))
            UpdateSceneManagerState(SceneManagerState.LEVEL_ANGER);
        if(Input.GetKeyDown(KeyCode.Alpha9))
            UpdateSceneManagerState(SceneManagerState.LEVEL_DEPRESSION);
        if(Input.GetKeyDown(KeyCode.Alpha0))
            UpdateSceneManagerState(SceneManagerState.LEVEL_ACCEPTANCE);
        if(Input.GetKeyDown(KeyCode.Minus))
            UpdateSceneManagerState(SceneManagerState.CREDITS);
    }

    public void UpdateSceneManagerState(SceneManagerState sceneManagerState)
    {
        CurrentSceneManagerState = sceneManagerState;

        switch (sceneManagerState)
        {
            case SceneManagerState.SPLASHSCREEN:
                SceneManager.LoadScene("SplashScreen");
                break;

            case SceneManagerState.MAINMENU:
                SceneManager.LoadScene("MainMenu");
                break;

            case SceneManagerState.LEVEL_DENIAL:
                SceneManager.LoadScene("LEVEL_DENIAL");
                break;

            case SceneManagerState.LEVEL_ANGER:
                SceneManager.LoadScene("Anger 2");
                break;

            case SceneManagerState.LEVEL_DEPRESSION:
                SceneManager.LoadScene("Depression 3");
                break;

            case SceneManagerState.LEVEL_ACCEPTANCE:
                SceneManager.LoadScene("LEVEL_ACCEPTANCE");
                break;

            case SceneManagerState.LOADINGSCREEN:
                SceneManager.LoadScene("LOADINGSCREEN");
                break;

            case SceneManagerState.SETTINGS:
                SceneManager.LoadScene("SettingsScene");
                break;

            case SceneManagerState.INSTRUCTIONS:
                SceneManager.LoadScene("Instructions");
                break;

            case SceneManagerState.CREDITS:
                SceneManager.LoadScene("Credits");
                break;

            default:
                break;
        }

        OnSceneChanged?.Invoke(sceneManagerState);
    }
}


