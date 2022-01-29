using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class SettingsManager : MonoBehaviour
{

    public static Dictionary<string, string> defaultSettings = new Dictionary<string, string>() {
        {"mVol", "50"},
        {"fullscreen", "1"},
        {"resolution", ""},
        {"sens", "5"},
    };

    public static Dictionary<string, string> settings = defaultSettings;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void saveSettings() {
        foreach (KeyValuePair<string, string> setting in settings) {
            PlayerPrefs.SetString(setting.Key, setting.Value);
        }
        PlayerPrefs.Save();
    }

    public static void loadSettings() {
        Dictionary<string, string> newSettings = new Dictionary<string, string>();
        foreach (KeyValuePair<string, string> setting in settings) {
            if (PlayerPrefs.HasKey(setting.Key)) {
                newSettings[setting.Key] = PlayerPrefs.GetString(setting.Key);
            } else {
                newSettings[setting.Key] = settings[setting.Key];
            }
        }
        settings = newSettings;
    }

}
