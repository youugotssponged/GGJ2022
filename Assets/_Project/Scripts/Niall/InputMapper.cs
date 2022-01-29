using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMapper : MonoBehaviour
{
    private static readonly Dictionary<char, KeyCode> _keycodeCache = new Dictionary<char, KeyCode>();
    public static Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>() {
        {"Forward", KeyCode.T},
        {"Back", KeyCode.Y},
        {"Left", KeyCode.A},
        {"Right", KeyCode.D},
        {"Interact", KeyCode.E},
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
        Dictionary<string, KeyCode> newControls = new Dictionary<string, KeyCode>();
        foreach (KeyValuePair<string, KeyCode> map in controls) {
            if (PlayerPrefs.HasKey(map.Key)) {
                newControls[map.Key] = GetKeyCodeFromString(char.Parse(PlayerPrefs.GetString(map.Key)));
            } else {
                newControls[map.Key] = controls[map.Key];
            }
        }
        controls = newControls;
    }

    static KeyCode GetKeyCodeFromString(char character) {
        // Get from cache if it was taken before to prevent unnecessary enum parse
        KeyCode code;
        if (_keycodeCache.TryGetValue(character, out code)) return code;
        // Cast to it's integer value
        int alphaValue = character;
        code = (KeyCode)Enum.Parse(typeof(KeyCode), alphaValue.ToString());
        _keycodeCache.Add(character, code);
        return code;
    }

    public static void SetKeyMap(string keyMap, KeyCode code) {
        controls[keyMap] = code;
    }

    public static void SetKeyMap(string keyMap, char keyString) {
        controls[keyMap] = GetKeyCodeFromString(keyString);
    }

    public static bool GetKeyDown(string keyMap) {
        return Input.GetKeyDown(controls[keyMap]);
    }

    public static void SaveControls() {
        foreach (KeyValuePair<string, KeyCode> map in controls) {
            PlayerPrefs.SetString(map.Key, ((char)(map.Value)).ToString());
        }
        PlayerPrefs.Save();
    }

}
