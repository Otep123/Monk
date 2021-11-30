using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public float health = 5.0f;
    public float speed = 3.0f;
    public GameObject fireBurstPrefab;
    public GameObject dustWalkPrefab;
    public GameObject dust2WalkPrefab;

    float orig_speed;
    float angle;
    float horizontal;
    float vertical;
    float dustOffset = 0.9f;
    bool spellEnd;

    FlashDamage flashDamage;
    Rigidbody2D rigidbody2D;
    Animator animator;

    Vector2 move = new Vector2();
    enum Facing
    {
        N, S, W, E, NW, NE, SW, SE
    }

    private Facing playerFacing = Facing.E; 

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
        Vector3 playerPixelPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;

        Vector3 direction = (mousePos - playerPixelPos).normalized;
        if (!animator.GetBool("Casting"))
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            move = new Vector2(horizontal, vertical);
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        animator.SetFloat("Speed", move.magnitude);
        //Up
        if (angle >= 45 && angle <= 135)
        {
            playerFacing = Facing.N;
            animator.SetFloat("LookY", 0.5f);
            animator.SetFloat("LookX", 0f);
        }
        //Down
        else if (angle >= -135 && angle <= -45)
        {
            playerFacing = Facing.S;
            animator.SetFloat("LookY", -0.5f);
            animator.SetFloat("LookX", 0f);
        }
        //Right
        if (angle >= -45 && angle <= 45)
        {
            playerFacing = Facing.E;
            animator.SetFloat("LookX", 0.5f);
            animator.SetFloat("LookY", 0f);
        }
        //Left
        else if (angle >= 135 && angle <= 180 || angle >= -180 && angle <= -135)
        {
            playerFacing = Facing.W;
            animator.SetFloat("LookX", -0.5f);
            animator.SetFloat("LookY", 0f);
        }

        //Input
        if (Input.GetMouseButtonDown(0))
        {
            if (!animator.GetBool("Casting")) {
                //CastFireBurst();
                animator.SetBool("Casting", true);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, direction, 1.5f, LayerMask.GetMask("NPC"));  //Stores the result of the raycast
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

    void CastFireBurst()
    {
        if (animator.GetBool("Casting"))
        {
            if (angle > 90 || angle < -90)
            {
                GameObject fireburst = Instantiate(fireBurstPrefab,
                    rigidbody2D.position + Vector2.up * 0.2f,
                    transform.rotation * Quaternion.Euler(0f, 0f, angle)
                );
                fireburst.transform.localScale = new Vector3(fireburst.transform.localScale.x, -1);
            }
            else
            {
                GameObject fireburst = Instantiate(fireBurstPrefab,
                    rigidbody2D.position + Vector2.up * 0.2f,
                    transform.rotation * Quaternion.Euler(0f, 0f, angle)
                );
            }
        }  
    }

    void Stop()
    {
        animator.SetBool("Casting", false);
    }
}
