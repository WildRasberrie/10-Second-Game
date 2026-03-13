using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Restart : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    Button button;
    void Start()
    {
        button = FindFirstObjectByType<Button>();
        button.onClick.AddListener(RestartGame);
        //if audio source is null, find it
        if (AudioSource == null) AudioSource = FindFirstObjectByType<AudioSource>();

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
