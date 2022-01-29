using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainMenuController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip MainMenuThemeMusic;
    public AudioClip BtnClick;
    public AudioClip BtnHover;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = MainMenuThemeMusic; // Need to test if PlayOneShot() overrides the .clip prop
        AudioSource.loop = true;
        AudioSource.Play();
    }

    // UI Event handlers
    public void PlaySoundOnClick() => AudioSource.PlayOneShot(BtnHover);
    public void PlaySoundOnHover() => AudioSource.PlayOneShot(BtnHover);

    // Button Handlers for Main Menu, mainly for redirect
    public void StartGame() => GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.START_GAME);
    public void Instructions() => GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.INSTRUCTIONS);
    public void Settings() => GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.SETTINGS);

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
