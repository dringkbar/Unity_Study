using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour {
    [Tooltip("플레이어가 점프할때 더해지는 힘")]
    public Vector2 jumpForce = new Vector2(2, 300);
    private bool beenHit;
    private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
        beenHit = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
    private void LateUpdate()
    {
        if((Input.GetKeyUp("space")) || Input.GetMouseButtonDown(0) && !beenHit)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(jumpForce);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        beenHit = true;
        GameController.speedModifier = 0;
        GetComponent<Animator>().speed = 0.0f;

        if (!gameObject.GetComponent<GameEndBehaviour>())
        {
            gameObject.AddComponent<GameEndBehaviour>();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
