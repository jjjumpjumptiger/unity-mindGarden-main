using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckMover : MonoBehaviour
{
	public float speed;
    public int direction; // 1 for right, -1 for left

    // Start is called before the first frame update
    void Start()
    {
        if (direction > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Flip the sprite by multiplying x scale by -1
            transform.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        if (transform.position.x > 10f || transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
