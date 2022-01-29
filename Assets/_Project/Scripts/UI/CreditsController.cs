using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CreditsController : MonoBehaviour
{

    public AudioSource AudioSource;
    public AudioClip BtnClick;
    public AudioClip BtnHover;

    public Text CreditsText;

    public int speed = 5;
    public int modifier = 10;

    public void BackToMainMenu() => GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);

    public void PlaySoundOnClick() => AudioSource.PlayOneShot(BtnClick);
    public void PlaySoundOnHover() => AudioSource.PlayOneShot(BtnHover);

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        CreditsText.transform.Translate(0, Time.deltaTime * speed * modifier, 0);
    }
}
