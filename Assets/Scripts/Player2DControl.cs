﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
public class Player2DControl : MonoBehaviour {
    public enum ProjectAxis { onlyX = 0, xAndY = 1 };
    public ProjectAxis projectAxis = ProjectAxis.onlyX;
    public float speed = 150;
    public float addForce = 7;
    public bool lookAtCursor;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public KeyCode addForceButton = KeyCode.Space;
    public bool isFacingRight = true;
    private Vector3 direction;
    private float vertical;
    private float horizontal;
    private Rigidbody2D body;
    private float rotationY;
    private bool jump;

    public float jumpForce = 700f;


    public int score;
  
    public GameObject ulikaPrefab;
    public GameObject ulika;


    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
        body.fixedAngle = true;

       ulika = GameObject.FindWithTag("ulika");


        if (projectAxis == ProjectAxis.xAndY)
        {
            body.gravityScale = 0;
            body.drag = 10;
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
        {
            body.drag = 10;
            jump = true;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground") //Some comment. Add more text
        {
            body.drag = 0;
            jump = false;
        }
    }

    void FixedUpdate()
    {
        body.AddForce(direction * body.mass * speed);

        if (Mathf.Abs(body.velocity.x) > speed / 100f)
        {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed / 100f, body.velocity.y);
        }

        if (projectAxis == ProjectAxis.xAndY)
        {
            if (Mathf.Abs(body.velocity.y) > speed / 100f)
            {
                body.velocity = new Vector2(body.velocity.x, Mathf.Sign(body.velocity.y) * speed / 100f);
            }
        }
        else
        {
            if (Input.GetKey(addForceButton) && jump)
            {
                body.velocity = new Vector2(0, addForce);
            }
        }
    }

    void Flip()
    {
        if (projectAxis == ProjectAxis.onlyX)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }


   
    void OnTriggerEnter2D(Collider2D col)
    {
        ulika = GameObject.FindWithTag("ulika");
        if (col.gameObject == ulika)
            ulika = GameObject.FindWithTag("ulika");
        {
            Destroy(col.gameObject);
            score++;
            
        }
    }

    // Update is called once per frame
    void Update () {
        if (lookAtCursor)
        {
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetKey(upButton)) vertical = 1;
        else if (Input.GetKey(downButton)) vertical = -1; else vertical = 0;

        if (Input.GetKey(leftButton)) horizontal = -1;
        else if (Input.GetKey(rightButton)) horizontal = 1; else horizontal = 0;

        if (projectAxis == ProjectAxis.onlyX)
        {
            direction = new Vector2(horizontal, 0);
        }
        else
        {
            if (Input.GetKeyDown(addForceButton)) speed += addForce; else if (Input.GetKeyUp(addForceButton)) speed -= addForce;
            direction = new Vector2(horizontal, vertical);
        }

        if (horizontal > 0 && !isFacingRight) Flip(); else if (horizontal < 0 && isFacingRight) Flip();
    
}

    void OnGUI()
    {
       GUI.Box(new Rect(40, 20, 50, 50), "Ulika: " + score);
      }





}
