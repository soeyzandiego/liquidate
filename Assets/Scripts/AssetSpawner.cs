using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssetSpawner : MonoBehaviour
{

    [SerializeField] float minWaitTime = 1.5f;
    [SerializeField] float maxWaitTime = 2.5f;
    [SerializeField] int maxActive = 10;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] TMP_Text activeText;
    [SerializeField] Color maxTextColor = Color.red;

    AudioSource audioSrc;
    Queue<string> assetQueue = new Queue<string>();
    int assetsActive;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // TODO swap from string to Asset.assetTypes???
    public void QueueAsset(string type)
    {
        if (assetsActive < maxActive)
        {
            assetQueue.Enqueue(type);
            assetsActive++;
        }
        else
        {
            print("max active assets");
            audioSrc.Play();
            return;
        }
    }

    void Update()
    {
        activeText.text = string.Format("{0}/{1}", assetsActive, maxActive);

        if (assetsActive < maxActive)
        {
            activeText.color = Color.white;
        }
        else
        {
            activeText.color = maxTextColor;
        }

        if (assetQueue.Count > 0)
        {
            float newTime = Random.Range(minWaitTime, maxWaitTime);
            string asset = assetQueue.Dequeue();
            StartCoroutine(SpawnAsset(asset, newTime));
        }
        else 
        { 
            return; 
        }
    }

    IEnumerator SpawnAsset(string type, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Spawn(type);
    }

    void Spawn(string type)
    {
        int index = 0;

        switch (type.ToLower())
        {
            case "potion":
                index = 0;
                break;
            case "ball":
                index = 1;
                break;
            case "sword":
                index = 2;
                break;
            case "pick":
                index = 3;
                break;
            case "crate":
                index = 4;
                break;
            case "rocket":
                index = 5;
                break;
        }

        Vector2 pos = new Vector2
            (
                Random.Range(-5f, 5f),
                Random.Range(1f, 3f)
            );
        Instantiate(prefabs[index], pos, Quaternion.identity);
    }

    public void SubtractAsset()
    {
        assetsActive--;
    }
}
