using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public string nextLevelName;
    public float minTimeBetweenLevelLoads;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Goal>().OnLevelComplete += (object sender, EventArgs e) =>
        {
            Debug.Log("Level Complete!");
            if (nextLevelName != null && nextLevelName != "")
                StartCoroutine(LoadLevelAsync());
        };
    }

    IEnumerator LoadLevelAsync()
    {
        float start = Time.time;
        AsyncOperation loadNextScene = SceneManager.LoadSceneAsync(nextLevelName);
        loadNextScene.allowSceneActivation = false;
        yield return new WaitUntil(() => loadNextScene.progress >= 0.9f);
        float timePassed = Time.time - start;
        if (timePassed < minTimeBetweenLevelLoads)
            yield return new WaitForSeconds(minTimeBetweenLevelLoads - timePassed);
        loadNextScene.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
