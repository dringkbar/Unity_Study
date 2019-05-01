using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public int health = 2;
    public Transform explosion;
    public AudioClip hitSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Laser"))
        {
            LaserBehaviour laser = collision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
            health -= laser.damage;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(hitSound);

        }

        if (health <= 0)
        {
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                controller.KilledEnemy();
                controller.IncreaseScore(10);
                Destroy(gameObject);
            }
        }
    }
}
