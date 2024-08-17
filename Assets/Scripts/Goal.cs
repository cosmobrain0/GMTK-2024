using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    bool levelCompleted;
    public bool LevelCompleted { get => levelCompleted; }
    public event EventHandler OnLevelComplete;

    // Start is called before the first frame update
    void Start()
    {
        levelCompleted = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (levelCompleted) return;
            levelCompleted = true;
            if (OnLevelComplete != null) OnLevelComplete(this, EventArgs.Empty);
        }
    }
}
