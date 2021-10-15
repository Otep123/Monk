using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    public float delayTime = 0.075f;
    public float durationTime = 1f;

    Rigidbody2D rigidbody2D;
    bool flash = false;
    float delayTimer = 0;
    float durationTimer = 0;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flash)
        {
            if (durationTimer <= durationTime)
            {
                durationTimer += Time.deltaTime;
                delayTimer += Time.deltaTime;
                if (delayTimer >= delayTime)
                {
                    spriteRenderer.enabled = !spriteRenderer.enabled;
                    delayTimer = 0;
                }
            }
            else
            {
                durationTimer = 0;
                delayTimer = 0;
                flash = false;
                spriteRenderer.enabled = true;
            }
        }
    }

    public void SetFlash(bool x)
    {
        flash = x;
    }

    public bool GetFlash()
    {
        return flash;
    }
}
