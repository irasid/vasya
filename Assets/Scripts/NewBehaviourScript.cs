using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    bool facingRight = true;
    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float score;
    public float move;
    public float mov;

    protected Rigidbody2D RB2d; 

    private GameObject uliki;
    // Use this for initialization


    // Use this for initialization
    void Start () {
        RB2d = GetComponent<Rigidbody2D>();
		
	}
    // Update is called once per frame
    void FixedUpdate()
    {


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

       move = Input.GetAxis("Horizontal");
       Vector2 movement = new Vector2(move, 0.0f);
       // RB2d.AddForce(movement);
    }

    // Update is called once per frame
    void Update () {
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {

            RB2d.AddForce (new Vector2(0f, jumpForce));
        }
        mov = move * maxSpeed;

        //Debug.Log(RB2d.velocity);
        RB2d.velocity = (new Vector2(mov, RB2d.velocity.y));
        Debug.Log(RB2d.velocity);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();



        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.name == "dieCollider") || (col.gameObject.name == "lisha-03"+"lusha-04"))
            Application.LoadLevel(Application.loadedLevel);

        if (col.gameObject.name == "ulika")
        {
            score++;
            Destroy(col.gameObject);
        }

        if (col.gameObject.name == "endLevel")
        {
            if (!(GameObject.Find("svitock"))) Application.LoadLevel("scene2");
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 50, 50), "Uliki: " + score);
    }



}
