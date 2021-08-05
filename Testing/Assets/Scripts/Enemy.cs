using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange = 4.0f;
    public static float moveSpeed = 3.0f;
    Rigidbody2D rb;
    Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else if (distToPlayer > agroRange)
        {
            StopChasePlayer();
        }
    }

    void ChasePlayer()
    {
        anim.SetBool("Trigger", true);



        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
       // movingRight = true;

        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }


        //if (transform.position.x < player.position.x)//&& transform.position.y < player.position.y
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //    rb.velocity = new Vector2(moveSpeed, 0);
        //    if (transform.position.y < player.position.y)
        //    {
        //        rb.velocity = new Vector2(0, moveSpeed);
        //    }
        //    //  GetComponent<SpriteRenderer>().flipX = false;
        //}
        //else if (transform.position.x > player.position.x) //&& transform.position.y > player.position.y
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //    rb.velocity = new Vector2(-moveSpeed, 0);
        //    if (transform.position.y > player.position.y)
        //    {
        //        rb.velocity = new Vector2(0, -moveSpeed);
        //    }
        //    //  GetComponent<SpriteRenderer>().flipX = true;
        //}
    }
    void StopChasePlayer()
    {
        anim.SetBool("Trigger", false);
        rb.velocity = new Vector2(0, 0);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.xp = Player.xp - 1;
            //Debug.Log("xp--"+ gameObject.name);
        }
    }
    //public void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //Player.xp = -1;
    //        Debug.Log("xp--");
    //    }
    //}
}
