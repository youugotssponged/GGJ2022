using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    public Image flame;
    public AudioClip flameOnSound;
    public AudioClip flameOffSound;
    public Light directionalLighting;
    private new AudioSource audio;
    private Color normalColor;
    private Color shadowRealmColour = new Color(0.6313726f, 0.254902f, 0.6078432f, 1);
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        normalColor = directionalLighting.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoNormal() {
        GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_NORMAL);
        directionalLighting.transform.eulerAngles = new Vector3(50, -30, 0);
        directionalLighting.color = normalColor;
        flame.enabled = false;
        audio.PlayOneShot(flameOffSound);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player")
            return;
        if (GlobalStateManager._Instance.CurrentGameState == GlobalStateManager.GameState.PLAYER_NORMAL) {
            GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_SHADOW_REALM);
            directionalLighting.transform.eulerAngles = new Vector3(30, -30, 0);
            directionalLighting.color = shadowRealmColour;
            StartCoroutine(fadeInFlame());
        } else if (GlobalStateManager._Instance.CurrentGameState == GlobalStateManager.GameState.PLAYER_SHADOW_REALM) {
            GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_NORMAL);
            directionalLighting.transform.eulerAngles = new Vector3(50, -30, 0);
            directionalLighting.color = normalColor;
            flame.enabled = false;
            StartCoroutine(fadeOutFlame());
        }
    }

    private IEnumerator fadeInFlame() {
        audio.pitch = 1.5f;
        flame.enabled = true;
        audio.PlayOneShot(flameOnSound);
        for (float i = 0; i < 1; i += Time.deltaTime) {
            flame.color = new Color(255, 255, 255, i);
            yield return null;
        }
    }

    private IEnumerator fadeOutFlame() {
        audio.pitch = 2f;
        audio.PlayOneShot(flameOffSound);
        for (float i = 1; i < 0; i -= Time.deltaTime) {
            flame.color = new Color(255, 255, 255, i);
            yield return null;
        }
    }

}
