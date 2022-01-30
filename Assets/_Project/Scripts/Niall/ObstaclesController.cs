using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstaclesController : MonoBehaviour
{
    public SwitchController sC;
    public NotificationController nC;
    public Text interactText;
    bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        interactText.text = $"Press '{InputMapper.controls["Interact"]}' to shove boulder";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && InputMapper.GetKeyDown("Interact")) {
            if (GlobalStateManager._Instance.CurrentGameState != GlobalStateManager.GameState.PLAYER_SHADOW_REALM) {
                nC.ShowNotification("You can't find the strength to move the boulder");
            } else {
                nC.ShowNotification("The boulder collapses and the path is clear!");
                interactText.enabled = false;
                sC.gotoNormal();
                nC.ShowNotification("You feel your strength wane");
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player")
            return;
        playerInRange = true;
        interactText.enabled = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag != "Player")
            return;
        playerInRange = false;
        interactText.enabled = false;
    }

}
