using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Threading;
using Unity.VisualScripting;

public class Scene_Manager : MonoBehaviour
{
    InputSystem_Actions action;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip AudioClip;
    [SerializeField] Button button;
    GameObject instance;
    //do not destroy this object when loading a new scene
    void Awake()
    {
        action = new InputSystem_Actions();
        //singleton pattern to ensure only one instance
        if (instance != null )
        {
            Destroy(this.gameObject);
        }
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);

        }
       
        AudioSource = FindFirstObjectByType<AudioSource>();
    }

    void OnEnable() { 
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    //on click change scene to Game Scene
    public void StartGame()
    {
        if (AudioSource != null) PlayAudio(AudioClip);
        SceneManager.LoadScene("Game Scene");
    }

    void PlayAudio(AudioClip audio)
    {
        AudioSource.PlayOneShot(audio);
    }
    //if esc is pressed change scene to start menu
    void Update()
    {
        //if esc is pressed, change to start menu
        if (action.Player.Escape.IsPressed())
        {
            SceneManager.LoadScene("Start Scene");
            Player_Controller.gameWon = false;
            Timer.timeOut = false;
        }
    }
  
}
