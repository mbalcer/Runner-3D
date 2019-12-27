using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health =3;

    public void heartbroken()
    {
        health -= 1;
    }

    public void ResetHealth()
    {
        health = 3;
    }

    public void setHealth()
    {
       if(health==0)
        {
            health = 3;
        }
       else if(health>3)
        {
            health = 3;
        }
        else if (health < 0)
        {
            health = 0;
        }
    }

    public int getHealth()
    {
        return health;
    }

}
