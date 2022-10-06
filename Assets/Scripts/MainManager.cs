using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject _inputField;

    // ENCAPSULATION
    public static MainManager Instance { get; private set; }
    // ENCAPSULATION
    public string ProjectName { get; private set; }

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
        ProjectName = _inputField.GetComponent<TMP_InputField>().text;
        SceneManager.LoadScene(1);
    }
}
