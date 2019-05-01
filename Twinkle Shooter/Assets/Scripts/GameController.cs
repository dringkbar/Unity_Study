using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public Transform enemy;
    [Header("User Interface")]
    private int score = 0;
    private int waveNumber = 0;

    public Text ScoreText;
    public Text WaveText;

    [Header("Wave Properties")]
    public float timeBeforeSpawing = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    public int currentNumberOfEnemies = 0;

    public void IncreaseScore(int increase)
    {
        score += increase;
        ScoreText.text = "Score : " + score;
    }


    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(timeBeforeSpawing);

        while (true)
        {
            if(currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                WaveText.text = "Wave : " + waveNumber;
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    float randDistance = Random.Range(10, 25);
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
