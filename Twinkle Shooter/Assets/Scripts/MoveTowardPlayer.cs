using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {
    private Transform player;
    public float speed = 2.0f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player Ship").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = player.position - transform.position;
        delta.Normalize();
        float moveSpeed = speed * Time.deltaTime;
        transform.position = transform.position + (delta * moveSpeed);
	}
}
