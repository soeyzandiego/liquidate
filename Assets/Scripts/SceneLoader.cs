using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void ReloadScene(float delay)
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(WaitAndLoad(i, delay));
    }

    public void LoadMenu(float delay)
    {
        StartCoroutine(WaitAndLoad(0, delay));
    }

    public void LoadGame(float delay)
    {
        StartCoroutine(WaitAndLoad(1, delay));
    }

    IEnumerator WaitAndLoad(int index, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(index);
    }
}
