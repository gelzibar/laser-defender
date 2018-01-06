using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public float health = 150f;
    public GameObject projectilePrefab;
    public float shotsPerSeconds = 0.5f;

    public AudioClip fireSound;
    public AudioClip deathSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float probability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < probability)
        {
            Fire();
        }

    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Laser missile = col.GetComponent<Laser>();
        if (missile && col.name == "Laser Shot")
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Scorekeeper.Score(10);
        Destroy(gameObject);
    }

    void Fire()
    {
        GameObject laser = Instantiate(projectilePrefab, transform.position, transform.rotation, GameObject.Find("Projectiles").transform);
        laser.name = "Enemy Laser Shot";
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1 * laser.GetComponent<Laser>().projectileSpeed);

        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
}
