using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct Level
{
    public string name;
    public Button button;
}

public class LoadFirstLevel : MonoBehaviour
{
    [SerializeField]
    public Level[] levels;
    int currentLevel = 0;

    private void Start()
    {
        SetLevel(0);
    }

    public void OnClick()
    {
        Debug.Log($"Loading level {currentLevel}");
        SceneManager.LoadScene(levels[currentLevel].name);
    }

    public void SetLevel(int level)
    {
        if (level >= 0 && level < levels.Length)
        {
            levels[currentLevel].button.GetComponent<Image>().color = Color.grey;
            currentLevel = level;
            levels[currentLevel].button.GetComponent<Image>().color = Color.white;
        }
    }
}
