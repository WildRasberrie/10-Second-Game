using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Threading;

public class Scene_Manager : MonoBehaviour
{
    InputSystem_Actions action;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip AudioClip;
    //do not destroy this object when loading a new scene
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        action = new InputSystem_Actions();
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
    private void Update()
    {
        //if timer ends, change to end scene
        if (Timer.timeOut) {
            SceneManager.LoadScene("End Scene");
        }

        if (Player_Controller.gameWon) {
            SceneManager.LoadScene("Win Scene");
        }

        //if esc is pressed, change to start menu
        if (action.Player.Escape.IsPressed())
        {
            SceneManager.LoadScene("Start Scene");
        }
    }

}
