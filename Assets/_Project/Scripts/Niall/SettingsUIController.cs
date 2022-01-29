using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class SettingsUIController : MonoBehaviour
{

    public PauseMenuController pauseMenuController;
    Slider mVol;
    Toggle fullscreenToggle;
    Dropdown resDropdown;
    String editingKeyMap = "";

    // Start is called before the first frame update
    void Start()
    {
        InputMapper.loadControls();
        SettingsManager.loadSettings();
        List<Component> components = GetComponentsInChildren<Component>().ToList<Component>();
        mVol = GetComponentInChildren<Slider>();
        fullscreenToggle = GetComponentInChildren<Toggle>();
        resDropdown = GetComponentInChildren<Dropdown>();

        initSettings(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetSettings() {
        foreach (KeyValuePair<string, string> pair in InputMapper.controls) {
            Text keyText = GetComponentsInChildren<Text>().First(text => text.gameObject.name == pair.Key);
        }
    }

    void initSettings(bool reset) {
        resDropdown.ClearOptions();
        int resIndex = 0;
        Resolution[] resolutions = Screen.resolutions.Reverse().ToArray();
        for (int i = 0; i < resolutions.Length; i++) {
            Resolution res = resolutions[i];
            resDropdown.options.Add(new Dropdown.OptionData() {text = res.ToString()});
            if (res.ToString() == ((reset) ? PlayerPrefs.GetString("resolution") : SettingsManager.settings["resolution"]))
                resIndex = i;
        }
        if (resIndex == 0)
            resIndex -= 1;
        resDropdown.SetValueWithoutNotify(resIndex);
        mVol.SetValueWithoutNotify(int.Parse(((reset) ? PlayerPrefs.GetString("mVol") : SettingsManager.settings["mVol"])));
        fullscreenToggle.SetIsOnWithoutNotify((((reset) ? PlayerPrefs.GetString("fullscreen") : SettingsManager.settings["fullscreen"]) == "1") ? true : false);
        foreach (KeyValuePair<string, string> pair in InputMapper.controls) {
            Text keyText = GetComponentsInChildren<Text>().First(Text => Text.gameObject.name == pair.Key);
            Button keyButton = keyText.GetComponentInChildren<Button>();
            keyButton.GetComponentInChildren<Text>().text = ((reset) ? PlayerPrefs.GetString(pair.Key) : pair.Value).ToString().ToUpper();
        }
        updateSettings();
    }

    void updateSettings() {
        updateMaxVolume();
        updateFullScreen();
        updateResolution();
    }

    public void updateMaxVolume() {
        mVol.GetComponentInChildren<Text>().text = mVol.value.ToString();
        SettingsManager.settings["mVol"] = mVol.value.ToString();
        AudioListener.volume = mVol.value / 100;
    }

    public void updateFullScreen() {
        SettingsManager.settings["fullscreen"] = (fullscreenToggle.isOn) ? "1" : "0";
        Screen.fullScreen = fullscreenToggle.isOn;
        updateResolution();
    }

    public void updateResolution() {
        SettingsManager.settings["resolution"] = resDropdown.options[resDropdown.value].text;
        String res = SettingsManager.settings["resolution"];
        String refreshRate = res.Split(" @ ")[1].Replace("Hz", "");
        res = res.Substring(0, res.LastIndexOf(" @"));
        String[] widthHeight = res.Split(" x ");
        Screen.SetResolution(width: int.Parse(widthHeight[0]), height: int.Parse(widthHeight[1]), fullscreenToggle.isOn, int.Parse(refreshRate));
    }

    public void selectControl(string keyMap) {
        if (editingKeyMap != "") {
            Text prevSelectedMap = GetComponentsInChildren<Text>().First(text => text.gameObject.name == editingKeyMap);
            Button prevSelectedButton = prevSelectedMap.GetComponentInChildren<Button>();
            prevSelectedButton.GetComponentInChildren<Text>().text = InputMapper.controls[editingKeyMap].ToString().ToUpper();
        }
        editingKeyMap = keyMap;
        Text text = GetComponentsInChildren<Text>().First(text => text.gameObject.name == keyMap);
        Button button = text.GetComponentInChildren<Button>();
        button.GetComponentInChildren<Text>().text = "Press a key";
    }

    //returns blank if key does not exist, or returns the name of the function it is bound to
    string keyAlreadyExists(string key, string currentKeyMap) {
        foreach (KeyValuePair<string, string> pair in InputMapper.controls) {
            if (pair.Value == key && pair.Key != currentKeyMap)
                return pair.Key;
        }
        return "";
    }

    void OnGUI() {
        Event e = Event.current;
        if (e.isKey && (editingKeyMap != "")) {
            if (e.keyCode == KeyCode.Escape) {
                updateControls(editingKeyMap, InputMapper.controls[editingKeyMap]);
                return;
            }
            updateControls(editingKeyMap, e.keyCode.ToString());
            editingKeyMap = "";
        }
    }

    void updateControls(string keyMap, string code) {
        InputMapper.controls[editingKeyMap] = code;
        Text keyText = GetComponentsInChildren<Text>().First(Text => Text.gameObject.name == keyMap);
        Button keyButton = keyText.GetComponentInChildren<Button>();
        keyButton.GetComponentInChildren<Text>().text = InputMapper.controls[editingKeyMap].ToString().ToUpper();
        String existingKey = keyAlreadyExists(code, editingKeyMap);
        if (existingKey != "") {
            Text oldKeyText = GetComponentsInChildren<Text>().First(Text => Text.gameObject.name == existingKey);
            Button oldButton = oldKeyText.GetComponentInChildren<Button>();
            oldButton.GetComponentInChildren<Text>().text = "Unbound";
            InputMapper.controls[existingKey] = "None";
        }
    }

    bool inSettingsScene() {
        return (GlobalSceneManager._Instance.CurrentSceneManagerState == GlobalSceneManager.SceneManagerState.SETTINGS);
    }

    public void saveSettings() {
        SettingsManager.saveSettings();
        InputMapper.SaveControls();
        if (inSettingsScene()) {
            GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
        } else {
            pauseMenuController.CloseSettings();
        }
    }

    public void cancel() {
        if (inSettingsScene()) {
            GlobalSceneManager._Instance.UpdateSceneManagerState(GlobalSceneManager.SceneManagerState.MAINMENU);
        } else {
            pauseMenuController.CloseSettings();
            initSettings(true);
        }
    }

    public void clearSettings() {
        PlayerPrefs.DeleteAll();
    }

}
