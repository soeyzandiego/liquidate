using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Start()
    {
        if (FindObjectsOfType<MusicPlayer>().Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }
}
