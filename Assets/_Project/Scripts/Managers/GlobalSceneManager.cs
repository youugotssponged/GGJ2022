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
        ENDSCREEN_GOOD,
        ENDSCREEN_BAD,
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
                SceneManager.LoadScene("DENIAL");
                break;

            case SceneManagerState.LEVEL_ANGER:
                SceneManager.LoadScene("ANGER");
                break;

            case SceneManagerState.LEVEL_DEPRESSION:
                SceneManager.LoadScene("DEPRESSION");
                break;

            case SceneManagerState.LEVEL_ACCEPTANCE:
                SceneManager.LoadScene("ACCEPTANCE");
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

            default:
                break;
        }

        OnSceneChanged?.Invoke(sceneManagerState);
    }
}


