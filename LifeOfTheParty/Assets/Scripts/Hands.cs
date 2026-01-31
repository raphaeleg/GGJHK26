using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    [SerializeField] public int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 1f, 1f);
    }

    private void AnimateSprite()
    {
    spriteIndex++;

    if (spriteIndex >= sprites.Length) {
        spriteIndex = 0;
    }

    if (spriteIndex < sprites.Length && spriteIndex >= 0) {
        spriteRenderer.sprite = sprites[spriteIndex];
    }
    }
}
