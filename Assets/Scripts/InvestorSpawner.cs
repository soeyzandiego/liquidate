using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestorSpawner : MonoBehaviour
{

    [SerializeField] float minInterval = 2f;
    [SerializeField] float maxInterval = 5f;
    [SerializeField] bool increaseRate = true;
    [SerializeField] float[] yPosOptions;
    [SerializeField] GameObject[] prefabs;

    float waitTime = 1;
    float timeElapsed = 0;

    // will be used to decrease the spawn interval
    // keeps track of the initial intervals that were used
    float startMin;
    float startMax;

    void OnDrawGizmos()
    {
        // visualize areas for spawning
        foreach (float y in yPosOptions)
        {
            Vector3 pos = new Vector3(0, y);
            Vector3 size = new Vector3(15, 1);
            Gizmos.DrawWireCube(pos, size);
        }
    }

    void Start()
    {
        startMin = minInterval;
        startMax = maxInterval;
    }

    void Update()
    {
        // spawns investors in intervals
        SpawnTimer();
        // total time elapsed (used to speed up spawning)
        Timer();
    }

    void SpawnTimer()
    {
        if (waitTime <= 0)
        {
            Spawn();
            waitTime = Random.Range(minInterval, maxInterval);
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    void Spawn()
    {
        int index = Random.Range(0, prefabs.Length);
        float side = Mathf.Sign(Random.Range(-10, 10)); // used to determine left or right side

        // choose one of y positions for spawning
        int yIndex = Random.Range(0, yPosOptions.Length);
        // add some wiggle room so investors are not all lined up at the same height
        float yPos = yPosOptions[yIndex] + Random.Range(-0.3f, 0.3f);

        // multiply the offscreen x pos (7) by side (-1 or 1)
        Vector2 pos = new Vector2(7 * side, yPos);

        Instantiate(prefabs[index], pos, Quaternion.identity);
    }

    void Timer()
    {
        timeElapsed += Time.deltaTime;

        if (!increaseRate) { return; }

        // TODO clean this up... somehow idk yet
        if (timeElapsed > 15 && timeElapsed < 30)
        {
            minInterval = startMin - 2f;
            maxInterval = startMax - 2f;
        }
        else if (timeElapsed >= 30 && timeElapsed < 45)
        {
            minInterval = startMin - 3.5f;
            maxInterval = startMax - 3f;
        }
        else if (timeElapsed >= 45 && timeElapsed < 55)
        {
            minInterval = startMin - 3.75f;
            maxInterval = startMax - 4.5f;
        }
        else if (timeElapsed >= 55 && timeElapsed < 70)
        {
            maxInterval = startMax - 5.5f;
        }
        else if (timeElapsed >= 70 && timeElapsed < 85)
        {
            maxInterval = startMax - 6f;
        }
        else if (timeElapsed >= 85)
        {
            minInterval = startMin - 4f;
            maxInterval = startMax - 6.5f;
        }

    }
}
