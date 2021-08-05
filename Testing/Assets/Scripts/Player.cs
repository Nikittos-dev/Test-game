using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
   
    public static float speed =4.0f;
    public static float xp;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    public Joystick joystick;
    public ControlType controlType;
    public GameObject CantrolPhone;
    public GameObject Boomb;

    public float time = 5.0f;
    public float xill = 7f;
    private bool flag = false;

    [SerializeField] Text XP;
    [SerializeField] Text score ;
    [SerializeField] Text Bscore ;
    private int bestScore;
    private static float Score;
    public enum ControlType { PC, Android }

    private bool facingright = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Score = 0;
        xp = 3;
    }

    void Update()
    {

        XP.text = "XP: "+xp.ToString();
        if (xp < 3.0)
        {
            xill -= Time.deltaTime;
            if (xill <= 0)
            {
                xp +=1;
                xill = 7f;
            }
        }
        Score = Score+ Time.deltaTime;
        bestScore = (int)Score;
        score.text = "Счет: " + bestScore.ToString();
        if (PlayerPrefs.GetInt("score") <= bestScore)
        {
            PlayerPrefs.SetInt("score", bestScore);
        }
        Bscore.text = "Рекорд : " + PlayerPrefs.GetInt("score").ToString();

        if (controlType == ControlType.Android)
        {
            moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            CantrolPhone.gameObject.SetActive(true);
            Boomb.SetActive(true);
        }
        else if (controlType == ControlType.PC)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            CantrolPhone.gameObject.SetActive(false);
            Boomb.SetActive(false);
        }
        moveVelocity = moveInput.normalized * speed;
        if (!facingright && moveInput.x>0)
        {
            Flip();
        }
        else if(facingright && moveInput.x < 0)
        {
            Flip();
        }
        time = time- Time.deltaTime;
        if (time <= 0.0f)
        {
            flag = true;          
        }

        if (xp <= 0)
        {
            Restart();
        }
       
    }
     void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        
    }
    public void Boombs()
    {
        if (flag == true)
        {
            Instantiate(Boomb, gameObject.transform.position, gameObject.transform.rotation);
            flag = false;
            time = 5.0f;
        }
    }
   
    public void Restart() {

            SceneManager.LoadScene(0);
    }
    private void Flip()
    {
        facingright = !facingright;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
