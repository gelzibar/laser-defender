using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float width = 5f;
    public float height = 2f;

    public float speed;
    float xmin = -10f;
    float xmax = 10f;
    float padding = 0.5f;

	int directionChangeCounter = 0;
	int directionChangeCounterMax = 60;
	int randomDirection;


    // Use this for initialization
    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
        speed = 3.5f;
		randomDirection = 0;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.position, enemyPrefab.transform.rotation) as GameObject;
            enemy.transform.parent = child;
            enemy.name = enemyPrefab.name;
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        float deltaSpeed = speed * Time.deltaTime;

		if(directionChangeCounter > directionChangeCounterMax)
		{
			randomDirection = Random.Range(0, 2);
			directionChangeCounter = 0;
		}
		else
		{
			directionChangeCounterMax = 60 + Random.Range(0, 91);
			directionChangeCounter++;
		}

        if (randomDirection == 1)
        {
            transform.position += Vector3.left * deltaSpeed;
        }
		else
		{
			transform.position += Vector3.right * deltaSpeed;
		}
		
        float newX = Mathf.Clamp(transform.position.x, xmin + (width/2), xmax - (width/2));

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

    }
}
