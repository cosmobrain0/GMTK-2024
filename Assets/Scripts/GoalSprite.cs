using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalSprite : MonoBehaviour
{

    public Sprite closedDoor;
    public Sprite openDoor;
    public Goal goal;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedDoor;
        goal.OnLevelComplete += (object sendor, EventArgs e) => spriteRenderer.sprite = openDoor;
    }
}
