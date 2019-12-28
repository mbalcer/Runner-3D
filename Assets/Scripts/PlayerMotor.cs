using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveVector;
    private Animation animation;
    private float speed = 5.0f;
    private float verticalVelocity = 0.0f;
    private float horizontalVelocity = 0.0f;
    private float gravity = 12.0f;
    public float jumpForce = 5f;
    private float animationDuration = 3.0f;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    private StarsManager starManager;
    private HeartManager heartManager;
    // Start is called before the first frame update
    void Start()
    {
        starManager = GameObject.Find("StarManager").GetComponent<StarsManager>();
        heartManager = GameObject.Find("HeartManager").GetComponent<HeartManager>();
        characterController = this.GetComponent<CharacterController>();
        animation = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (heartManager.getHealth())
        {
            case 3:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(true);
                break;
            case 2:
                Heart1.SetActive(true);
                Heart2.SetActive(true);
                Heart3.SetActive(false);
                break;
            case 1:
                Heart1.SetActive(true);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
                break;
            default:
                Heart1.SetActive(false);
                Heart2.SetActive(false);
                Heart3.SetActive(false);
                heartManager.ResetHealth();
                //TODO
                // Wstawić zmiane sceny na deathScene
                break;
        }
        
        if (Time.time < animationDuration)
        {
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }
        moveVector = Vector3.zero;
        if(characterController.isGrounded)
        {
            horizontalVelocity = Input.GetAxisRaw("Horizontal");
            verticalVelocity = -gravity * Time.deltaTime;
               animation.Play("Run");
            
            if (Input.GetButtonDown("Jump"))
            {
                animation.Stop("Run");
                animation.Play("Runtojumpspring");
                verticalVelocity = jumpForce;
            
            }
            
        }
        else
        {
      
            verticalVelocity -= gravity * Time.deltaTime;
            horizontalVelocity = 0.0f;

        }

        moveVector.x = horizontalVelocity * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        characterController.Move(moveVector * Time.deltaTime);

    }
    public void SetSpeed(float modifier)
    {
        speed = 5.0f + modifier;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            if(other.GetComponent<Animation>() != null)
            {
                other.GetComponent<Animation>().Play();
            }
            if(other.GetComponent<AudioSource>() != null)
            {
                other.GetComponent<AudioSource>().Play();
            }
            heartManager.heartbroken();
            Debug.Log(heartManager.getHealth());
          
        }
        if(other.tag == "Star")
        {
            starManager.CollectStar(other.gameObject);
        }
        if(other.tag == "Powerup")
        {
            Destroy(other.gameObject);
        }
    }
}
