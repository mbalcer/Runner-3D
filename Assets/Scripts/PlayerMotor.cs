using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float animationDuration = 9f;
    private float time = 0f;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;
    private StarsManager starManager;
    private HeartManager heartManager;
    private bool scoreMultipler = false;
    private int timeScoreMultipler = 0;
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
       
        
        if (time < animationDuration)
        {
            time += 0.05f;
            characterController.Move(Vector3.forward * speed * Time.deltaTime);
            Debug.Log(time);
            return;
        }
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

                int score = Score.GetAllScore();
                DataManager.SetScore(score);
                time = 0;
                SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                break;
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
            //   horizontalVelocity = 0.0f;
            horizontalVelocity = Input.GetAxisRaw("Horizontal"); //wystarczy zakomentować jeśli nie ma być ruchu podczas skoku

        }

        moveVector.x = horizontalVelocity * speed;
        moveVector.y = verticalVelocity;
        moveVector.z = speed;

        characterController.Move(moveVector * Time.deltaTime);

        if (timeScoreMultipler > 0)
            timeScoreMultipler--;
        if (scoreMultipler && timeScoreMultipler == 0)
            scoreMultipler = false;
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

            SoundManager.PlaySound("star");

            if (scoreMultipler)
                starManager.CollectStar(other.gameObject, 2);
            else
                starManager.CollectStar(other.gameObject, 1);            
        }
        if(other.tag == "Powerup")
        {
            if (other.name == "ExtraLife(Clone)")
            {

                SoundManager.PlaySound("powerup");

                heartManager.addHeart();
            }
            else if (other.name == "ScoreMultiplier(Clone)")
            {
                SoundManager.PlaySound("premium");
                
                scoreMultipler = true;
                timeScoreMultipler += 500;
            }
            else if (other.name == "CoinMagnet(Clone)")
            {


                SoundManager.PlaySound("magnet");

                starManager.coinMagnet = true;
                starManager.timeCoinMagnet += 500;
            }
            Debug.Log(other.name);
            Destroy(other.gameObject);
        }
    }
}
