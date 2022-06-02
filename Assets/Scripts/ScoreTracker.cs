using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text endScoreText;

    int score = 0;

    void Start()
    {
        UpdateText();
    }

    public void Add()
    {
        score++;
        UpdateText();
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
        endScoreText.text = string.Format("Correctly distributed assets: {0}", score);
    }
}
