using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    public Image flame;
    public AudioClip flameOnSound;
    public AudioClip flameOffSound;
    private AudioSource audio;
    private Color normalColor = new Color(1, 0.9568627f, 0.8392157f, 1);
    private Color shadowRealmColour = new Color(0.6313726f, 0.254902f, 0.6078432f, 1);
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Light sceneLighting = GetComponentInChildren<Light>();
        if (GlobalStateManager._Instance.CurrentGameState == GlobalStateManager.GameState.PLAYER_NORMAL) {
            GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_SHADOW_REALM);
            sceneLighting.transform.eulerAngles = new Vector3(30, -30, 0);
            sceneLighting.color = shadowRealmColour;
            StartCoroutine(fadeInFlame());
        } else if (GlobalStateManager._Instance.CurrentGameState == GlobalStateManager.GameState.PLAYER_SHADOW_REALM) {
            GlobalStateManager._Instance.UpdateGameState(GlobalStateManager.GameState.PLAYER_NORMAL);
            sceneLighting.transform.eulerAngles = new Vector3(50, -30, 0);
            sceneLighting.color = normalColor;
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
