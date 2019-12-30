using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip star, jumpSound, magnet, powerup, premium, menuSound;
    private static AudioSource audioSrc;
    private static bool createdSound = false;

    void Start()
    {
       
        star = Resources.Load<AudioClip>("ButtonPresses");
       // jumpSound = Resources.Load<AudioClip>("Jump2");
        magnet = Resources.Load<AudioClip>("Magnet");
        powerup = Resources.Load<AudioClip>("PowerUp");
        premium = Resources.Load<AudioClip>("PremiumCurrency");
       
        if (createdSound == false)
        {
            audioSrc = GetComponent<AudioSource>();
            audioSrc.loop = true;
            audioSrc.Play();
            DontDestroyOnLoad(this.gameObject);
            createdSound = true;
        }
            audioSrc.Play();
        
    }

    // Update is called once per frame

    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "star":
                audioSrc.PlayOneShot(star);
                break;
            case "magnet":
                audioSrc.PlayOneShot(magnet);
                break;
            case "powerup":
                audioSrc.PlayOneShot(powerup);
                break;
            case "premium":
                audioSrc.PlayOneShot(premium);
                break;
        }
    }
}
