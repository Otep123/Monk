using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wildlife : MonoBehaviour
{
    public float health = 1;
    public float speed;
    public GameObject dustExplosionPrefab;

    Rigidbody2D rigidbody2D;
    bool vertical = false;
    float timer;
    float changeTime;
    int verticalDirection = 1;
    int horizontalDirection = 1;
    float temp;             //Stores speed float
    Animator animator;
    FlashDamage flashDamage;

    // Start is called before the first frame update
    void Start()
    {
        changeTime = Random.Range(0.1f, 5f);
        temp = speed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        flashDamage = GetComponent<FlashDamage>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        RandomDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            RandomDirection();
            if (RandomBool())
            {
                animator.SetBool("Idle", false);
                StopMovement(false);
                
                timer = changeTime;
                vertical = !vertical;
            }
            else
            {
                animator.SetBool("Idle", true);
                StopMovement(true);
                timer = changeTime;
            }

        }
        if (horizontalDirection > 0) animator.SetFloat("MoveX", 0);
        else animator.SetFloat("MoveX", -1);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        if (vertical) position.y = position.y + Time.deltaTime * speed * verticalDirection;
        position.x = position.x + Time.deltaTime * speed * horizontalDirection;
        rigidbody2D.MovePosition(position);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        Monk player = other.gameObject.GetComponent<Monk>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomDirection();
        if (RandomBool()) vertical = !vertical;
    }

    public void Damage()
    {
        if (health < 1)
        {
            GameObject dustExplosionObject = Instantiate(dustExplosionPrefab, rigidbody2D.position, transform.rotation * Quaternion.identity);
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
        }
        else
        {
            speed = temp;
        }
    }

    void RandomDirection()
    {
        if (RandomBool()) verticalDirection = -verticalDirection;
        if (RandomBool()) horizontalDirection = -horizontalDirection;
    }

    bool RandomBool()
    {
        return (Random.value > 0.5);
    }
}
