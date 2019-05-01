using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : RepeatingBackground {
    protected override void Offscreen(ref Vector3 pos)
    {
        Destroy(this.gameObject);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Score++;
    }
}
