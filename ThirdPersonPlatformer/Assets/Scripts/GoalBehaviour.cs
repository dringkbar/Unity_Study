using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehaviour : MonoBehaviour {
    ParticleSystem ps;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (ps.isPlaying)
        {
            print("You Win!");
        }
    }
}
