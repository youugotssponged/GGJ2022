using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InstructionsController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip BtnClick;
    public AudioClip BtnHover;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void BackToMainMenu() => GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
    public void PlayClick() => AudioSource.PlayOneShot(BtnClick);
    public void PlayHover() => AudioSource.PlayOneShot(BtnHover);
}
