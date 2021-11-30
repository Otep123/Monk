using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public characterType type;
    public enum characterType
    {
        NPC,
        PLAYER,
        ENEMY
    }
    public float movementSpeed = 4;
    protected Rigidbody2D rigidbody2D;
    [HideInInspector]
    public Vector2 move;
    [HideInInspector]
    public float horizontal;
    [HideInInspector]
    public float vertical;
    [HideInInspector]
    public bool isSprinting;
    [HideInInspector]
    public Vector3 direction;
}
