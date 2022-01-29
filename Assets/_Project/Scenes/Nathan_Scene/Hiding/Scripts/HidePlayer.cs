using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidePlayer : MonoBehaviour
{
    public GameObject player;

    public Text HideText;

    private bool CanHide = false;
    private bool CurrentlyHiding = false;

    // Player components
    private PlayerMovement PlayerScript;
    private Collider PlayerColliderEnabled;
    private Rigidbody PlayerUseGravity;
    private Vector3 PlayerPosition;

    private string HideMessage = "Press 'E' to hide";
    private string LeaveMessage = "Press 'E' to leave";

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = player.GetComponent<PlayerMovement>();
        PlayerColliderEnabled = player.GetComponent<BoxCollider>();
        PlayerUseGravity = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentlyHiding && Input.GetKeyDown(KeyCode.E))
        {
            StopHiding();
        }
        else if (CanHide && Input.GetKeyDown(KeyCode.E))
        {
            Hide();
        }
    }

    void Hide()
    {
        HideText.text = LeaveMessage;
        PlayerScript.CanMove = false;
        PlayerColliderEnabled.enabled = false;
        PlayerUseGravity.useGravity = false;

        PlayerPosition = player.transform.position;
        player.transform.position = transform.position;

        player.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        PlayerScript.PlayerCamera.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        CurrentlyHiding = true;
    }
    void StopHiding()
    {
        player.transform.position = PlayerPosition;

        player.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        PlayerScript.PlayerCamera.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        PlayerScript.CanMove = true;
        PlayerColliderEnabled.enabled = true;
        PlayerUseGravity.useGravity = true;
        CurrentlyHiding = false;

        HideText.text = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            HideText.text = HideMessage;
            CanHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            HideText.text = string.Empty;
            CanHide = false;
        }
    }
}
