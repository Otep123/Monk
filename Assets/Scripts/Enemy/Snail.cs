using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float speed;
    public bool vertical = true;
    public float changeTime = 1.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            direction = 1;
            if (RandomBool())
            {
                direction = -direction;
                timer = changeTime;
                vertical = !vertical;
            }
            else
            {
                direction = 0;
                timer = changeTime;
            }
        }
    }

    bool RandomBool()
    {
        return (Random.value > 0.5);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        if (vertical) position.y = position.y + Time.deltaTime * speed * direction;
        else position.x = position.x + Time.deltaTime * speed * direction; 
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Monk player = other.gameObject.GetComponent<Monk>();

        if (player != null)
        {
            //player.ChangeHealth(-1);
        }

        direction = -direction;
    }
}
