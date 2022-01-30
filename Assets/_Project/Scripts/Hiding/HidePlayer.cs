using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidePlayer : MonoBehaviour
{
    public GameObject Player;

    public Text HideText;

    private bool CanHide = false;
    private bool CurrentlyHiding = false;

    // Player components
    private PlayerMovement PlayerScript;
    private Collider PlayerColliderEnabled;
    private Rigidbody PlayerUseGravity;
    private Vector3 PlayerPosition;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<PlayerMovement>();
        PlayerColliderEnabled = Player.GetComponent<CapsuleCollider>();
        PlayerUseGravity = Player.GetComponent<Rigidbody>();
        InputMapper.loadControls();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentlyHiding && InputMapper.GetKeyDown("Interact"))
        {
            StopHiding();
        }
        else if (CanHide && InputMapper.GetKeyDown("Interact"))
        {
            Hide();
        }
    }

    void Hide()
    {
        HideText.text = $"Press '{InputMapper.controls["Interact"]}' to leave";
        PlayerScript.CanMove = false;
        PlayerColliderEnabled.enabled = false;
        PlayerUseGravity.useGravity = false;

        PlayerPosition = Player.transform.position;
        Player.transform.position = transform.position;

        Player.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        PlayerScript.PlayerCamera.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        CurrentlyHiding = true;
    }
    void StopHiding()
    {
        Player.transform.position = PlayerPosition;

        Player.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
        PlayerScript.PlayerCamera.transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        PlayerScript.CanMove = true;
        PlayerColliderEnabled.enabled = true;
        PlayerUseGravity.useGravity = true;
        CurrentlyHiding = false;

        HideText.text = string.Empty;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            HideText.text = $"Press '{InputMapper.controls["Interact"]}' to hide";
            CanHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            HideText.text = string.Empty;
            CanHide = false;
        }
    }
}
