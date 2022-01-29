using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper : MonoBehaviour
{
    private static readonly Dictionary<char, KeyCode> _keycodeCache = new Dictionary<char, KeyCode>();
    public static Dictionary<string, string> controls = new Dictionary<string, string>() {
        {"Forward", "W"},
        {"Back", "S"},
        {"Left", "A"},
        {"Right", "D"},
        {"Interact", "E"},
    };

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
        KeyCode code;
        if (character.Length > 1) {
            switch(character) {
                case "UpArrow":
                    code = KeyCode.UpArrow;
                    break;
                case "DownArrow":
                    code = KeyCode.DownArrow;
                    break;
                case "LeftArrow":
                    code = KeyCode.LeftArrow;
                    break;
                case "RightArrow":
                    code = KeyCode.RightArrow;
                    break;
                default:
                    code = KeyCode.None;
                    break;
            }
        } else {
            if (_keycodeCache.TryGetValue(char.Parse(character), out code)) return code;
            // Cast to it's integer value
            int alphaValue = char.Parse(character);
            code = (KeyCode)Enum.Parse(typeof(KeyCode), alphaValue.ToString());
            _keycodeCache.Add(char.Parse(character), code);
        }
        return code;
    }

    public static string GetKeyFromAction(string action) {
        return controls[action];
    }

    public static bool GetKeyDown(string keyMap) {
        return Input.GetKeyDown(GetKeyCodeFromString(controls[keyMap]));
    }

    public static void SaveControls() {
        foreach (KeyValuePair<string, string> map in controls) {
            PlayerPrefs.SetString(map.Key, map.Value.ToString());
        }
        PlayerPrefs.Save();
    }

}
