using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {
    [Tooltip("이 오브젝트가 얼마나 빠르게 움직이는가")]
    public float scrollSpeed;
    public const float ScrollWidth = 8;

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos.x -= scrollSpeed * Time.deltaTime * GameController.speedModifier;

        if (transform.position.x < -ScrollWidth)
            Offscreen(ref pos);
        transform.position = pos;
    }

    protected virtual void Offscreen(ref Vector3 pos)
    {
        pos.x += 2 * ScrollWidth;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


	}
}
