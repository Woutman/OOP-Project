using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Slider _civilianSlider;
    [SerializeField] private Slider _policeSlider;
    [SerializeField] private Slider _killerSlider;
    [SerializeField] private TextMeshProUGUI _civilianCounter;
    [SerializeField] private TextMeshProUGUI _policeCounter;
    [SerializeField] private TextMeshProUGUI _killerCounter;

    [SerializeField] private TextMeshProUGUI _projectNameText;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings();
        if (MainManager.Instance != null)
        {
            _projectNameText.text = MainManager.Instance.ProjectName;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        _civilianCounter.text = " " + _civilianSlider.value;
        _policeCounter.text = " " + _policeSlider.value;
        _killerCounter.text = " " + _killerSlider.value;
    }

    [Serializable]
    private class SaveData
    {
        public float CiviliansToSpawn;
        public float PoliceToSpawn;
        public float KillersToSpawn;
    }

    public void SaveSettings()
    {
        SaveData saveData = new SaveData();
        saveData.CiviliansToSpawn = _civilianSlider.value;
        saveData.PoliceToSpawn = _policeSlider.value;
        saveData.KillersToSpawn = _killerSlider.value;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            _civilianSlider.value = saveData.CiviliansToSpawn;
            _policeSlider.value = saveData.PoliceToSpawn;
            _killerSlider.value = saveData.KillersToSpawn;
        }      
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
