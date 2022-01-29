using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    // Private properties
    Rigidbody PlayerRigidBody;
    // Movement
    private float LookUpDown;
    private float LookLeftRight;
    // Running
    private float EnergyRemaining = 100f;
    private bool RunEnabled = false;
    // Jumping
    private bool CanJump = true;
    Vector3 Jump;
    public float JumpForce = 2.0f;

    // Public properties
    public bool CanMove = true;
    public Camera PlayerCamera;
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;
    public float LookSensitivity = 5f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody>(); 
        Jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            MovePlayer();
            MoveCamera();
            PlayerJump();
        }
    }
    /// <summary>
    /// Handles moving the player character object.
    /// </summary>
    private void MovePlayer()
    {
        float movementSpeed = GetMovementSpeed();
        // Player movement control
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, 0, vertical) * (movementSpeed * Time.deltaTime));
    }
    /// <summary>
    /// Handles camera rotation, to allow looking around when moving mouse.
    /// </summary>
    private void MoveCamera()
    {
        // Camera look/rotation control
        LookLeftRight += Input.GetAxis("Mouse X") * LookSensitivity;
        LookUpDown += Input.GetAxis("Mouse Y") * LookSensitivity;
        transform.rotation = Quaternion.Euler(0f, LookLeftRight, 0f);

        LookUpDown = Mathf.Clamp(LookUpDown, -90f, 90f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(-LookUpDown, 0f, 0f);
    }
    /// <summary>
    /// Handles the player jumping.
    /// </summary>
    private void PlayerJump()
    {
        if (CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            CanJump = false;
            PlayerRigidBody.AddForce(Jump * JumpForce, ForceMode.Impulse);
        }
    }
    /// <summary>
    /// Gets the movement speed for the player.  Allows for sprinting.
    /// </summary>
    /// <returns></returns>
    private float GetMovementSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift) && RunEnabled)
        {
            CalculateEnergyRemaining(-0.03f);
            return RunSpeed;
        }

        CalculateEnergyRemaining(0.05f);
        return WalkSpeed;
    }
    /// <summary>
    /// Calculate if the player has enough energy to continue sprinting.
    /// Sprinting is disabled for a time if energy is fully depleted.
    /// </summary>
    /// <param name="energyChange"></param>
    private void CalculateEnergyRemaining(float energyChange)
    {
        EnergyRemaining += energyChange;

        if (EnergyRemaining <= 0)
            RunEnabled = false;

        if (EnergyRemaining > 100)
        {
            RunEnabled = true;
            EnergyRemaining = 100f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CanJump = true;
    }


}
