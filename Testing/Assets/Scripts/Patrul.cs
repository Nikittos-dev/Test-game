using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrul : MonoBehaviour
{
    public static float speed = 3.5f;
    public int positionOfPatrol;
    bool movingRight;
    public Transform point;
    public float rayDistance = 3f;
    //public GameObject LeftPos;

    Transform player;
    public float stoppingDistance;
    Animator anim;



   // GameObject Walls;


    bool chill = false;
    bool angry = false;
    bool goBack = false;

    private Rigidbody2D rb;
    void Start()
    {
       // Walls = GameObject.FindWithTag("Walls");
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }


    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            //anim.SetBool("enemyWalk", true);
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
        if (movingRight == true)
        {
            //hit.transform.gameObject.tag == "Enemy"
            //hit.collider = GameObject.FindWithTag("Walls")
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);
            if (hit == true && hit.transform.gameObject.tag == "Walls")
            {

                rb.velocity = Vector2.up * 5;
            }
        }
        else if (movingRight == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, rayDistance);

            if (hit == true && hit.transform.gameObject.tag == "Walls")
            {

                rb.velocity = Vector2.up * 5;
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.left * rayDistance);
    }

    void Chill()
    {
        anim.SetBool("Trigger", false);
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;

        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
    }

    void Angry()
    {
        anim.SetBool("Trigger", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        movingRight = true;

        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);

        }

    }

    void GoBack()
    {
        anim.SetBool("Trigger", false);
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        movingRight = true;

        if (movingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.xp = Player.xp - 1;
            //Debug.Log("xp--"+ gameObject.name);
        }
    }

}


//if (transform.position.x >= transform.position.x+1)
//{
//    transform.eulerAngles = new Vector3(0, -180, 0);
//    movingRight = false;
//}
//else if (transform.position.x <= -3)
//{
//    transform.eulerAngles = new Vector3(0, 0, 0);
//    movingRight = true;
//}