using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : Character
{
    public GameObject canvas;
    public float maxIdleDistance = 4;
    public bool isFollowingPlayer;
    public GameObject playerCharacter;
    private GameObject portraitImage;
    private DialogueManager dialogue;
    private bool isShowing;
    private Vector3 velocity;
    private Vector3 targetPos;
    private float movementSpeedEdit;

    void Start()
    {
        //canvas = GameObject.FindGameObjectWithTag("Canvas");
        //dialogue = canvas.GetComponent<DialogueManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        move = new Vector2();
        direction = new Vector2();
        targetPos = new Vector3();
        movementSpeedEdit = movementSpeed;
        this.type = characterType.NPC;
    }

    void Update() 
    {
        if (isFollowingPlayer) 
        {
            targetPos = playerCharacter.transform.position;
            Vector2 normalizedTarget = targetPos.normalized;
            direction = (targetPos - this.transform.position).normalized;
            horizontal = direction.x;
            vertical = direction.y;
        }
    }

    void FixedUpdate() 
    {
        if (isFollowingPlayer) 
        {
            Vector3 moveTowardsPos = Vector3.MoveTowards(transform.position, targetPos, movementSpeedEdit * Time.deltaTime);
            rigidbody2D.MovePosition(moveTowardsPos);
            float currentDistance = Vector3.Distance(transform.position, targetPos);
            if (currentDistance < maxIdleDistance && movementSpeedEdit > 0) {
                movementSpeedEdit -= 0.1f;
                if (movementSpeedEdit <= 0) {
                    movementSpeedEdit = 0;
                    move.Set(0,0);
                }
            }
            else if (currentDistance >= maxIdleDistance) {
                move.Set(moveTowardsPos.x,moveTowardsPos.y);
                if (movementSpeedEdit < movementSpeed) {
                    movementSpeedEdit += 0.1f;
                }
            }
            Debug.Log(move);
            //StartCoroutine(MoveToSpot(playerCharacter.transform.position, movementSpeed));
        }
    }

    // IEnumerator MoveToSpot(Vector3 targetPosition, float duration)
    //  {
    //     float elapsedTime = 0;
    //     Vector3 currentPos = transform.position;
 
    //     // while (elapsedTime < waitTime)
    //     // {
    //     //     transform.position = Vector3.Lerp(currentPos, targetPosition, (elapsedTime / duration));
    //     //     elapsedTime += Time.deltaTime;

    //     //     yield return null;
    //     // }

    //     while (elapsedTime < duration)
    //     {
    //         transform.position = Vector3.Lerp(currentPos, targetPosition, (elapsedTime / duration));
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }
    //  }

    public void DisplayDialogue()
    {
        isShowing = !isShowing;
        canvas.SetActive(isShowing);
        dialogue.StartStory();
    }
}
