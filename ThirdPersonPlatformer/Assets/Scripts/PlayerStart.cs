using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour {
    public Transform player;
    public static bool spawned = false;
    public static PlayerStart _instance;

	// Use this for initialization
	void Start () {
        if (_instance !=null)
        {
            Destroy(_instance.gameObject);
        }
        _instance = this;

        if (!spawned)
        {
            SpawnPlayer();
            spawned = true;
        }
	}

    void SpawnPlayer()
    {
        Transform newObject = Instantiate(player, this.transform.position, Quaternion.identity) as Transform;
        newObject.name = "Player";
    }
}
