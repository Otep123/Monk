using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public float health = 5.0f;
    public float speed = 3.0f;
    public float fireBurstRecoilTime = 1f;
    public float fireBurstRecoilSpeed = 1f;
    public GameObject fireBurstPrefab;
    public GameObject dustWalkPrefab;
    public GameObject dust2WalkPrefab;

    float orig_speed;
    float horizontal;
    float vertical;
    float dustOffset = 0.9f;

    FlashDamage flashDamage;
    Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        orig_speed = speed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flashDamage = GetComponent<FlashDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("Casting"))
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
        
        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) //Checks if move vector has any value in it, if it does then Monk prob moved
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = orig_speed;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Casting", true); 
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            flashDamage.SetFlash(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));  //Stores the result of the raycast
                                                                                                                                            //rigidbody2d.pos is Monk's feet, Vector2.up * 0.2 is an offset to his center
                                                                                                                                            //raycast is 1.5f units long
                                                                                                                                            //Tests only the "NPC" layer
            if (hit.collider != null)   //Checks if the raycast actually hit something
            {
                NonPlayableCharacter npc = hit.collider.GetComponent<NonPlayableCharacter>(); //Gets the collider's NPC script
                if (npc != null)
                {
                    npc.DisplayDialogue();
                }
            }
            
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = rigidbody2D.position;
        pos.x = pos.x + speed * horizontal * Time.deltaTime;
        pos.y = pos.y + speed * vertical * Time.deltaTime;

        if (!animator.GetBool("Casting")) rigidbody2D.MovePosition(pos);
    }

    public void ChangeHealth (float amount)
    {
        if (!flashDamage.GetFlash())
        {
            health += amount;
            if (amount < 0)
            {
                flashDamage.SetFlash(true);
            }
        }
        
    }

    void SpawnDustWalk(int flip)
    {
        if (flip>0)
        {
            GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                rigidbody2D.position + Vector2.right * dustOffset + Vector2.down * 0.1f,
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)
            );
        }
        else
        {
            GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                rigidbody2D.position + Vector2.left * dustOffset + Vector2.down * 0.1f,
                transform.rotation * Quaternion.identity
            );
        }
    }

    void SpawnDust2Walk(int flip)
    {
        if (flip > 0)
        {
            GameObject Dust2WalkObject = Instantiate(dust2WalkPrefab,
                rigidbody2D.position + Vector2.right * dustOffset + Vector2.down * 0.1f,
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)
            );
        }
        else
        {
            GameObject Dust2WalkObject = Instantiate(dust2WalkPrefab,
                rigidbody2D.position + Vector2.left * dustOffset + Vector2.down * 0.1f,
                transform.rotation * Quaternion.identity
            );
        }
    }

    void CastFireBurst(int flip)
    {
        //If flip is 1 or more, spell is flipped
        if (flip>0) 
        {
            GameObject FireBurstObject = Instantiate(fireBurstPrefab,
                rigidbody2D.position + Vector2.up * 0.2f,
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)
                );
        }
        //If flip is less than or equal to 0, spell is not flipped
        else
        {
            GameObject FireBurstObject = Instantiate(fireBurstPrefab,
                rigidbody2D.position + Vector2.up * 0.2f,
                transform.rotation * Quaternion.identity
                );
        }
    }
}
