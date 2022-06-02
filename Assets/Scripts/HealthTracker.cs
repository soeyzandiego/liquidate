using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{

    [SerializeField] int maxHealth = 4;
    [SerializeField] Sprite[] heartSprites;
    [SerializeField] Image[] hearts;
    [SerializeField] GameObject gameOver;

    int health;

    void Start()
    {
        gameOver.SetActive(false);
        health = maxHealth;
        UpdateHearts();
    }

    void Update()
    {
        if (health <= 0)
        {
            EndGame();
        }
    }

    public void Damage()
    {
        health--;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        if (health == 4)
        {
            hearts[0].sprite = heartSprites[0];
            hearts[1].sprite = heartSprites[0];
        }
        else if (health == 3)
        {
            hearts[0].sprite = heartSprites[0];
            hearts[1].sprite = heartSprites[1];
        }
        else if (health == 2)
        {
            hearts[0].sprite = heartSprites[0];
            hearts[1].sprite = heartSprites[2];
        }
        else if (health == 1)
        {
            hearts[0].sprite = heartSprites[1];
            hearts[1].sprite = heartSprites[2];
        }
        else if (health == 0)
        {
            hearts[0].sprite = heartSprites[2];
            hearts[1].sprite = heartSprites[2];
        }
    }

    void EndGame()
    {
        gameOver.SetActive(true);

        Asset[] assets = FindObjectsOfType<Asset>();
        foreach (Asset asset in assets)
        {
            Destroy(asset);
        }
    }
}
