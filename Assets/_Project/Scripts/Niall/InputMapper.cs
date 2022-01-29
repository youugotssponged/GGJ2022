using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper : MonoBehaviour
{
    private static readonly Dictionary<char, KeyCode> _keycodeCache = new Dictionary<char, KeyCode>();
    public static Dictionary<string, string> defaultControls = new Dictionary<string, string>() {
        {"Forward", "W"},
        {"Back", "S"},
        {"Left", "A"},
        {"Right", "D"},
        {"Interact", "E"},
        {"Jump", "Space"},
        {"Sprint", "LeftShift"},
    };

    public static Dictionary<string, string> controls = defaultControls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void loadControls() {
        Dictionary<string, string> newControls = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> map in controls) {
            if (PlayerPrefs.HasKey(map.Key)) {
                newControls[map.Key] = PlayerPrefs.GetString(map.Key);
            } else {
                newControls[map.Key] = controls[map.Key];
            }
        }
        controls = newControls;
    }

    static KeyCode GetKeyCodeFromString(string character) {
        return (KeyCode) System.Enum.Parse(typeof(KeyCode), character);
    }

    public static string GetKeyFromAction(string action) {
        return controls[action];
    }

    public static bool GetKey(string keyMap) {
        return Input.GetKey(GetKeyCodeFromString(controls[keyMap]));
    }

    public static bool GetKeyDown(string keyMap) {
        return Input.GetKeyDown(GetKeyCodeFromString(controls[keyMap]));
    }

    public static float GetAxis(string axis) {
        switch (axis) {
            case "Horizontal":
                if (GetKey("Right"))
                    return 1;
                if (GetKey("Left"))
                    return -1;
                break;
            case "Vertical":
                if (GetKey("Forward"))
                    return 1;
                if (GetKey("Back"))
                    return -1;
                break;
        }
        return 0;
    }

    public static void SaveControls() {
        foreach (KeyValuePair<string, string> map in controls) {
            PlayerPrefs.SetString(map.Key, map.Value.ToString());
        }
        PlayerPrefs.Save();
    }

}
