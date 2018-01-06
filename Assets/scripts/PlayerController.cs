using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    float xmin = -10f;
    float xmax = 10f;
    float padding = 0.5f;
    float ymin, ymax;

    public GameObject projectilePrefab;
    float firingRate = .33f;

    float health = 150;

    public AudioClip laserSound;

    void Start()
    {
        onStart();
    }

    void FixedUpdate()
    {
        onFixedUpdate();
    }

    void Update()
    {
        onUpdate();
    }
    void onStart()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;

        Vector3 topmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 botmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        ymin = botmost.y + padding;
        ymax = topmost.y - padding;

        speed = 6f;
    }
    void onFixedUpdate()
    {

    }

    void Fire()
    {
        GameObject laser = Instantiate(projectilePrefab, transform.position, transform.rotation, GameObject.Find("Projectiles").transform);
        laser.name = "Laser Shot";
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laser.GetComponent<Laser>().projectileSpeed);

        AudioSource.PlayClipAtPoint(laserSound, transform.position);
    }
    void onUpdate()
    {
        float deltaSpeed = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            transform.position += Vector3.left * deltaSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * deltaSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Laser missile = col.GetComponent<Laser>();
        if (missile && col.name == "Enemy Laser Shot")
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Scorekeeper.Reset();
                Destroy(gameObject);
            }
        }
    }
}
