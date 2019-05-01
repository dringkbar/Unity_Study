using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private static Text scoreText;
    private static int score;

    public static int Score
    {
        get { return score; }
        set { score = value; scoreText.text = "Score : " + score.ToString(); }
    }

    [HideInInspector]
    public static float speedModifier;

    [Header("장애물 정보")]
    [Tooltip("생성될 장애물")]
    public GameObject obstacleReference;

    [Tooltip("장애물에 쓰일 최소 y값")]
    public float obstacleMinY = -1.3f;
    [Tooltip("장애물에 쓰일 최대 y값")]
    public float obstacleMaxY = 1.3f;

    void CreatObstacle()
    {
        Instantiate(obstacleReference, new Vector3(RepeatingBackground.ScrollWidth, Random.Range(obstacleMinY, obstacleMaxY), 0.0f), Quaternion.identity);
    }
    // Use this for initialization
    void Start () {
        speedModifier = 1.0f;
        gameObject.AddComponent<GameStartBehaviour>();
        score = 0;
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
