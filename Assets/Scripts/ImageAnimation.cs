using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float spritePerFrame = 1;
    public bool loop = true;
    public bool destroyOnEnd = false;
    public float animationSpeed = 1f;

    private int index = 0;
    private Image image;
    private float frame = 0;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!loop && index == sprites.Length) return;
        frame += animationSpeed;
        if (frame < spritePerFrame) return;
        image.sprite = sprites[index];
        frame = 0;
        index++;
        if (index >= sprites.Length)
        {
            if (loop) index = 0;
            if (destroyOnEnd) Destroy(gameObject);
        }
    }
}
