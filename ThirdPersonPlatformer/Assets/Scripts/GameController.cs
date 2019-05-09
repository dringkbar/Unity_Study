using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    private ParticleSystem goalPS;
    public ParticleSystem GoalPS
    { get { return goalPS; } set { goalPS = value; } }

    public static GameController _instance;
    private int OrbsCollected;
    private int orbsTotal;

    public Text scoreText;

    private void Start()
    {
        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");

        OrbsCollected = 0;
        orbsTotal = orbs.Length;

        scoreText.text = "Orbs: " + OrbsCollected + "/" + orbsTotal;
    }

    private void Update()
    {
        if (Input.GetKeyDown("f2"))
        {
            this.gameObject.GetComponent<LevelEditor>().enabled = true;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void CollectedOrb()
    {
        OrbsCollected++;
        scoreText.text = "Orbs: " + OrbsCollected + "/" + orbsTotal;
        if (OrbsCollected >= orbsTotal)
        {
            goalPS.Play();
        }
    }

    public void UpdateOrbTotals(bool reset = false)
    {
        if (reset)
            OrbsCollected = 0;
        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");
        orbsTotal = orbs.Length;
        scoreText.text = "Orbs: " + OrbsCollected + "/" + orbsTotal;
    }

}