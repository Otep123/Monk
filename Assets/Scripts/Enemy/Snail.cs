using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    public float health = 1;
    public float speed;
    public bool vertical = true;
    public float changeTime = 1.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    float temp;             //Stores speed float
    Animator animator;
    FlashDamage flashDamage;

    // Start is called before the first frame update
    void Start()
    {
        temp = speed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        flashDamage = GetComponent<FlashDamage>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        if (RandomBool())
        {
            direction = -direction;
            if (RandomBool()) vertical = !vertical;
        }
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            if (RandomBool())
            {
                animator.SetBool("Idle", false);
                StopMovement(false);
                direction = -direction;
                timer = changeTime;
                vertical = !vertical;
            } else
            {
                animator.SetBool("Idle", true);
                StopMovement(true);
                timer = changeTime;
            }
            
        }
        if (direction > 0) animator.SetFloat("MoveX", 0);
        else animator.SetFloat("MoveX", -1);
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
            player.ChangeHealth(-1);
        }

        direction = -direction;
        if (RandomBool()) vertical = !vertical;
    }

    public void Damage()
    {
        if (health<=0)
        {
            GameObject.Destroy(gameObject);
        }
        health--;
        flashDamage.SetFlash(true);
    }

    void StopMovement(bool value)
    {
        if (value)
        {
            speed = 0;
        } else
        {
            speed = temp;
        }
    }

    bool RandomBool()
    {
        return (Random.value > 0.5);
    }
}
