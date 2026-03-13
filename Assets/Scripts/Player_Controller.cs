using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    Vector3 targetPOS;
    bool targetSet, targetReached;
    int imgCount;
    public static bool gameWon =false;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite[] UI_Images;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip AudioClip;

    Animator anim;
    void Start()
    {
        // grab the rigidbody component of the player game object
        rb = GetComponent<Rigidbody2D>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        // get mouse input 
        var mouse = Mouse.current.position.ReadValue();
        var mousePOS = Camera.main.ScreenToWorldPoint(mouse);
        mousePOS.z = 0;


        //add player movement to follow cursor 
        PlayerMovement(mousePOS);
        //show flower bouquet based on collection amount
        sprite.sprite = UI_Images[imgCount];
        //if all flowers are collected, end the game
        if (imgCount == UI_Images.Length-1){
            gameWon = true;
        }

        //if timer ends, change to end scene
        if (Timer.timeOut)
        {
            SceneManager.LoadScene("End Scene");
        }

        if (gameWon)
        {
            SceneManager.LoadScene("Win Scene");
        }

    }

    //play sound effect when reaching glass 
    void PlayAudio(AudioClip audio) { 
        AudioSource.PlayOneShot(audio);
    }

    // on trigger enter with flower, destroy the flower and add to bouqet 
    public void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Flower")) { 
            Destroy(col.gameObject);
            PlayAudio(AudioClip);
            imgCount++;
        }

    }
    //rotate player to face mouse position
    void RotatePlayer(Vector3 MousePOS) {
        transform.rotation = Quaternion.Euler(0,0,Theta(MousePOS));
    }
    //get theta angle btwn player and mouse 
    //rotate player to face mouse position
    float Theta(Vector3 MousePOS) { 
        //formula for theta = tan ^-1 (y/x)
        float theta = Mathf.Atan2(MousePOS.y - transform.position.y, MousePOS.x - transform.position.x) * Mathf.Rad2Deg;
        if (theta < 0) theta += 360f;
        if (theta > 90f) theta -=90f;
        return theta;
    }

    void PlayerMovement(Vector3 mousePosition) {
        //get mouse left click input 
        var leftClick = Mouse.current.leftButton.wasPressedThisFrame;

        // set player position to follow mouse position after click 
        if (leftClick)
        {
            targetSet = true;
            targetPOS = mousePosition;
            RotatePlayer(mousePosition);
        }

        if (targetSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPOS, speed * Time.deltaTime);
            //play walking animation 
            anim.Play("Walk");
        }

        if (transform.position == targetPOS)
        {
            targetReached = true;
        }

        if (targetReached)
        {
            targetSet = false;
            targetReached = false;
            //play idle animation
            anim.Play("Idle");

        }
    }
}
