using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject InputField;

    public static MainManager Instance;
    public string ProjectName;

    // Start is called before the first frame update
    void Awake()
    {
        // Make instance persist through session.
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Load main scene.
    public void LoadMain()
    {
        ProjectName = InputField.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }
}
