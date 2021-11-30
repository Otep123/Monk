using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public GameObject dustWalkPrefab;
    public GameObject dust2WalkPrefab;
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

    void SpawnDustWalk(int flip)
    {
        float dustOffset = 0.9f;
        Vector2 pos = transform.position;
        if (flip>0)
        {
            GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                pos + Vector2.right * dustOffset,
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)
            );
        }
        else
        {
            GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                pos + Vector2.left * dustOffset,
                transform.rotation * Quaternion.identity
            );
        }
    }

    void SpawnDustKickUp(int flip) {
        float dustOffset = Random.Range(0.1f, 1f);
        if (Random.Range(1,10) >= 4) {
            Vector2 pos = transform.position;
            if (flip>0)
            {
                GameObject DustWalkObject = Instantiate(dust2WalkPrefab,
                    pos + Vector2.right * dustOffset,
                    transform.rotation * Quaternion.Euler(0f, 180f, 0f)
                );
            }
            else
            {
                GameObject DustWalkObject = Instantiate(dust2WalkPrefab,
                    pos + Vector2.left * dustOffset,
                    transform.rotation * Quaternion.identity
                );
            }
        }
        else {
            Vector2 pos = transform.position;
            if (flip>0)
            {
                GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                    pos + Vector2.right * dustOffset,
                    transform.rotation * Quaternion.Euler(0f, 180f, 0f)
                );
            }
            else
            {
                GameObject DustWalkObject = Instantiate(dustWalkPrefab,
                    pos + Vector2.left * dustOffset,
                    transform.rotation * Quaternion.identity
                );
            }
        }
    }

    void SpawnDust2Walk(int flip)
    {
        float dustOffset = 0.9f;
        Vector2 pos = transform.position;
        if (flip>0)
        {
            GameObject DustWalkObject = Instantiate(dust2WalkPrefab,
                pos + Vector2.right * dustOffset,
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)
            );
        }
        else
        {
            GameObject DustWalkObject = Instantiate(dust2WalkPrefab,
                pos + Vector2.left * dustOffset,
                transform.rotation * Quaternion.identity
            );
        }
    }
}

