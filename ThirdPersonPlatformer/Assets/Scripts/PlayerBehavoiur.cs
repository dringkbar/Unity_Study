using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavoiur : MonoBehaviour {
    private Rigidbody rigidbody;
    public Vector2 jumpForce = new Vector2(0, 450);
    public float maxSpeed = 3.0f;
    public float speed = 50.0f;
    private float xMove;
    private bool shouldJump;
    private float yPrevious;
    private bool onGround;
    private bool collidingWall;

    private void FixedUpdate()
    {
        Movement();
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    void Movement()
    {
        xMove = Input.GetAxis("Horizontal");

        if(collidingWall && !onGround)
        {
            xMove = 0;
        }

        if(xMove != 0)
        {
            float xSpeed = Mathf.Abs(xMove * rigidbody.velocity.x);

            if(xSpeed < maxSpeed)
            {
                Vector3 movementForce = new Vector3(1, 0, 0);
                movementForce *= xMove * speed;
                RaycastHit hit;
                if(!rigidbody.SweepTest(movementForce,out hit , 0.05f))
                {
                    rigidbody.AddForce(movementForce);
                }

            }

            if (Mathf.Abs(rigidbody.velocity.x) > maxSpeed)
            {
                Vector2 newVelocity;

                newVelocity.x = Mathf.Sign(rigidbody.velocity.x) * maxSpeed;
                newVelocity.y = rigidbody.velocity.y;
                rigidbody.velocity = newVelocity;
            }
        }
        else
        {
            Vector2 newVelocity = rigidbody.velocity;

            newVelocity.x *= 0.9f;
            rigidbody.velocity = newVelocity;
        }
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump")) { shouldJump = true; }
        if (shouldJump && onGround)
        {
            rigidbody.AddForce(jumpForce);
            shouldJump = false;
        }
    }

    // Use this for initialization
    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        shouldJump = false;
        xMove = 0.0f;
        onGround = false;
        yPrevious = Mathf.Floor(transform.position.y);
        collidingWall = false;
	}

    private void OnCollisionStay(Collision collision)
    {
        if (!onGround)
        {
            collidingWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collidingWall = false;
    }

    // Update is called once per frame
    void Update () {
        Jumping();
        CheckGrounded();
	}

    void CheckGrounded()
    {
        float distance = (GetComponent<CapsuleCollider>().height / 2 * this.transform.localScale.y) + .01f;
        Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
        Vector3 origin = transform.position;

        if (!onGround)
        {
            if(Physics.Raycast(origin, floorDirection, distance))
            {
                onGround = true;
            }
        }
        else if ((Mathf.Floor(transform.position.y) != yPrevious))
        {
            onGround = false;
        }

        yPrevious = Mathf.Floor(transform.position.y);
    }

    //private void OnDrawGizmos()
    //{
    //    Debug.DrawLine(transform.position, transform.position + rigidbody.velocity, Color.red);
    //}
}
