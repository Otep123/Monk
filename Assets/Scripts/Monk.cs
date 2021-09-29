using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : MonoBehaviour
{
    public float speed = 3.0f;

    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject fireBurstPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("Casting", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));  //Stores the result of the raycast
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

    void CastSpell()
    {
        GameObject FireBurstObject = Instantiate(fireBurstPrefab, rigidbody2D.position + Vector2.up * 0.2f, Quaternion.identity);
    }

    void FixedUpdate()
    {
        Vector2 pos = rigidbody2D.position;
        pos.x = pos.x + speed * horizontal * Time.deltaTime;
        pos.y = pos.y + speed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(pos);
    }
}
