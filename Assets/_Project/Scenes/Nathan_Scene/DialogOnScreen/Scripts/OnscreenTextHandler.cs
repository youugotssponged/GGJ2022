using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnscreenTextHandler : MonoBehaviour
{
    public GameObject PlayerObject;
    public Text ScreenText;
    public TextAsset TextFile;

    private bool Triggered = false;
    private bool WaitingForInput = false;
    private bool ScreenClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && WaitingForInput)
            ScreenClicked = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerObject && !Triggered)
        {
            Debug.Log("Trigger Entered.");
            Triggered = true;

            string[] testArray = TextFile.text.Split("\n");

            StartCoroutine(UpdateText(testArray));
        }
    }

    IEnumerator UpdateText(string[] textArray)
    {
        // Stop playing moving
        PlayerMovement playerMovementscript = PlayerObject.GetComponent<PlayerMovement>();
        playerMovementscript.CanMove = false;

        // Split into sentences.
        foreach (string sentence in textArray)
        {
            // Type text to screen letter by letter.
            foreach (char c in sentence)
            {
                // Update text on screen.
                ScreenText.text += c;
                yield return new WaitForSeconds(0.02f);
            }

            // For for user to click to continue.
            WaitingForInput = true;
            while (!ScreenClicked)
            {
                yield return new WaitForSeconds(0.1f);
            }

            // Reset before continuing.
            WaitingForInput = false;
            ScreenClicked = false;

            ScreenText.text = string.Empty;

            yield return new WaitForSeconds(0.2f);
        }
        
        // Allow player movement
        playerMovementscript.CanMove = true;

        yield return 0;
    }
}
