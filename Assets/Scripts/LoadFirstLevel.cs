using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadFirstLevel : MonoBehaviour
{
    public string firstLevelName;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene(firstLevelName);
    }
}
