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
    private StarsManager starManager;
    // Start is called before the first frame update
    void Start()
    {
        starManager = GameObject.Find("StarManager").GetComponent<StarsManager>();
        characterController = this.GetComponent<CharacterController>();
        animation = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < animationDuration)
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
            other.GetComponent<Animation>().Play(); // TODO Działa tylko dla śmietników. Inne obiekty nie są animowane. Należy to naprawić
            other.GetComponent<AudioSource>().Play();
            //TODO zabranie jednego życia
        }
        if(other.tag == "Fruits")
        {
            starManager.CollectStar(other.gameObject);
        }
    }
}
