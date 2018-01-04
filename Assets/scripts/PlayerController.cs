using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    float xmin = -10f;
    float xmax = 10f;
	float padding = 0.5f;

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
        speed = 6f;
    }
    void onFixedUpdate()
    {

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

        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }
}
