using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    float angle;
    Character parentCharacter;

    private Vector2 walkDirection;
    protected Animator animator;

    enum Facing
    {
        N, S, W, E, NW, NE, SW, SE
    }

    private Facing directionFacing = Facing.E;

    // Start is called before the first frame update
    void Start()
    {
        walkDirection = new Vector2();
        animator = GetComponent<Animator>();
        parentCharacter = this.transform.parent.gameObject.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 bodyDirection = parentCharacter.direction;
        angle = Mathf.Atan2(bodyDirection.y, bodyDirection.x) * Mathf.Rad2Deg;

        if (parentCharacter.isSprinting) {
            animator.SetFloat("AnimationSpeed", 2f);
        } else animator.SetFloat("AnimationSpeed", 1f);

        if (!(animator.GetFloat("MovementSpeed") > 0) || !(parentCharacter is Player)) {
            //Up
            if (angle >= 45 && angle <= 135)
            {
                directionFacing = Facing.N;
                animator.SetFloat("LookY", 0.5f);
                animator.SetFloat("LookX", 0f);
            }
            //Down
            else if (angle >= -135 && angle <= -45)
            {
                directionFacing = Facing.S;
                animator.SetFloat("LookY", -0.5f);
                animator.SetFloat("LookX", 0f);
            }
            //Right
            if (angle >= -45 && angle <= 45)
            {
                directionFacing = Facing.E;
                animator.SetFloat("LookX", 0.5f);
                animator.SetFloat("LookY", 0f);
            }
            //Left
            else if (angle >= 135 && angle <= 180 || angle >= -180 && angle <= -135)
            {
                directionFacing = Facing.W;
                animator.SetFloat("LookX", -0.5f);
                animator.SetFloat("LookY", 0f);
            }
        }
        else {
            walkDirection.Set(parentCharacter.move.x, parentCharacter.move.y);
            walkDirection.Normalize();

            animator.SetFloat("LookX", walkDirection.x);
            animator.SetFloat("LookY", walkDirection.y);
        }
        
        animator.SetFloat("MovementSpeed", parentCharacter.move.magnitude);
    }

    
}
