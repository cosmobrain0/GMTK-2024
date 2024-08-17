using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public string nextLevelName;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Goal>().OnLevelComplete += (object sender, EventArgs e) =>
        {
            if (nextLevelName != null && nextLevelName != "")
                SceneManager.LoadScene(nextLevelName);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
