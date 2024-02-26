using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckSpawnSciript_new : MonoBehaviour
{

	public GameObject duckPrefab;
    public float spawnInterval = 3f;
    public float minSpeed = 3f;
    public float maxSpeed = 5f;
    public Vector2 minMaxSize = new Vector2(0.5f, 1.5f);

    public int countLeftToRight = 0;
    public int countRightToLeft = 0;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnInterval)
        {
            SpawnDuck();
            timer = 0;
        }
    }

    void SpawnDuck()
    {
        bool moveLeftToRight = Random.value > 0.5f;
        GameObject duck = Instantiate(duckPrefab, GetSpawnPosition(moveLeftToRight), Quaternion.identity);
        float size = Random.Range(minMaxSize.x, minMaxSize.y);
        duck.transform.localScale = new Vector3(size, size, size);

        duckMover mover = duck.AddComponent<duckMover>();
        mover.speed = Random.Range(minSpeed, maxSpeed);
        // mover.speed = Random.Range(minSpeed, GameManager.Instance.maxSpeed);

        if (moveLeftToRight)
        {
            mover.direction = 1; // If moveLeftToRight is true, set direction to 1 (right)
        }
        else
        {
            mover.direction = -1; // If moveLeftToRight is false, set direction to -1 (left)
        }

        // Increment count based on direction
        if (moveLeftToRight)
        {

            Debug.Log("Enter Left. Count: " + countLeftToRight);
            countLeftToRight++;
            Debug.Log("Leave Left. Count: " + countLeftToRight);

        }
        else {
            Debug.Log("Enter Right. Count: " + countRightToLeft);
            countRightToLeft++;
            Debug.Log("Leave Right. Count: " + countRightToLeft);
        }
    }

    Vector3 GetSpawnPosition(bool moveLeftToRight)
    {
        float y = Random.Range(-4f, 4f);
        float x = moveLeftToRight ? -10f : 10f;
        float z = Camera.main.transform.position.z + 10; // Adjust this value as needed
        return new Vector3(x, y, 0);
    }
}
