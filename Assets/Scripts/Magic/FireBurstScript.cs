using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurstScript : MonoBehaviour
{
    Rigidbody2D rigidBody2d;
    BoxCollider2D collider2D;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 forceDirection = new Vector2(1,0);
        rigidBody2d.AddForce(forceDirection);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //we also add a debug log to know what the projectile touch
        Debug.Log("Projectile Collision with " + other.gameObject);
        Snail s = other.gameObject.GetComponent<Snail>();

        if (s != null)
        {
            s.Damage();
        }
    }

    void boxColliderResize(int phase)
    {
        Vector2 size = new Vector2(0, 0);
        Vector2 offset = new Vector2(0, 0);
        switch (phase)
        {
            case 2:
                offset.Set(2.8f, 0.4f);
                size.Set(3.9f,1f);
                break;
            case 3:
                offset.Set(5.7f, 0.9f);
                size.Set(4.7f, 1.2f);
                break;
            case 4:
                offset.Set(5.7f, 1.3f);
                size.Set(4.77f, 2.5f);
                break;
        }
        collider2D.size = size;
        collider2D.offset = offset;
    }

    void removeCollider()
    {
        collider2D.enabled = false;
    }

}
