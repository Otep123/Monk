using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private float movementSpeedEdit;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeedEdit = movementSpeed;
        rigidbody2D = GetComponent<Rigidbody2D>();
        move = new Vector2();
        direction = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal and Vertical are normalized, they represent direction e.g. 0,1
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 playerPixelPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;

        direction = (mousePos - playerPixelPos).normalized;

        move = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            isSprinting = true;
            movementSpeedEdit = movementSpeedEdit * 1.5f;
        }   
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isSprinting = false;
            movementSpeedEdit = movementSpeed;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2D.position + Vector2.up * 0.1f, direction, 2.5f, LayerMask.GetMask("NPC"));  //Stores the result of the raycast
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
        pos.x = pos.x + movementSpeedEdit * horizontal * Time.deltaTime;
        pos.y = pos.y + movementSpeedEdit * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(pos);
    }
}
