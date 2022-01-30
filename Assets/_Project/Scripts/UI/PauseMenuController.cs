using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseMenuController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip BtnClick;
    public AudioClip BtnHover;

    public bool isPaused;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject PlayerObject;

    private PlayerMovement PlayerMovementScript;

    // Start is called before the first frame update
    private void Start()
    {
        isPaused = false;
        AudioSource = GetComponent<AudioSource>();
        PlayerMovementScript = PlayerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            _ = isPaused ? Resume() : Pause(); // Cheeky Discard   
    }



    // Apparently Trigger event handlers need to be VOID and no type... ffs
    public void PauseUI() => Pause();
    public void ResumeUI() => Resume();


    // UI Event handlers
    public bool Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        PlayerMovementScript.CanMove = false;

        return true;
    }
    public bool Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PausePanel.SetActive(false);

        // Close settings if not already closed, this deals with the issue of having settings open and unpausing
        CloseSettingsPanel();

        PlayerMovementScript.CanMove = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        return isPaused;
    }
    public void OpenSettings() 
    {
        if(isPaused)
        {
            PausePanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
    }
    public void CloseSettings() 
    { 
        PausePanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void CloseSettingsPanel() => SettingsPanel.SetActive(false);

    public void BackToMainMenu() 
    {
        Time.timeScale = 1f; // resume engine time before moving back
        GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PlaySoundOnClick() => AudioSource.PlayOneShot(BtnClick);
    public void PlaySoundOnHover() => AudioSource.PlayOneShot(BtnHover);


}
