using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropBoomb : MonoBehaviour
{

    private float stopTime = 2.5f;
    private float TimeGo = 2.5f;
    private float baffspeed = 0.05f;
    private bool sttE;
    private bool sttD;
    public Animator anim;
    public Animator anim1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.speed = Player.speed + baffspeed;
            //Debug.Log(Player.speed);
        }
        else if (collision.CompareTag("Enemy"))
        {
            
            sttE = true;
            if (stopTime > 0.0f && sttE == true)
            {
                Enemy.moveSpeed = 0.5f;
                anim.SetTrigger("Dir");
            }
            
        }
        else if (collision.CompareTag("Dog"))
        {
           
            sttD = true;
            if (stopTime > 0.0f && sttD == true)
            {
                Patrul.speed = 0.5f;
                anim1.SetTrigger("Dir");
            }
           
        }
    }
    void Start()
    {
        sttE = false;
        sttD = false;

    }
    void Update()
    {
        if (sttE == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            TimeGo = TimeGo - Time.deltaTime;
            if (TimeGo <= 0 )
            {               
                sttE = false;
                Destroy(gameObject);
                Enemy.moveSpeed = 3.0f;
            }
        }
        if (sttD == true)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            TimeGo = TimeGo - Time.deltaTime;
            if (TimeGo <= 0.0f)
            {      
                sttD = false;
                Destroy(gameObject);
                Patrul.speed = 3.5f;
            }
        }
    }  
}
