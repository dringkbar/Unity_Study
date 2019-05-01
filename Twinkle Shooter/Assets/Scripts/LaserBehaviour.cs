using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {
    public float lifetime = 2.0f;
    public float speed = 5.0f;
    public int damage = 1;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,lifetime);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
	}
}
