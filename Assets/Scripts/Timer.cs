using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float timer;
    float score;
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindFirstObjectByType<CollisionScript>().isGameCleared)
        {
            timer += Time.deltaTime;
        }
        else if (FindFirstObjectByType<CollisionScript>().isGameCleared)
        {
            score = timer;
            scoreText.text = "YOU'VE CLEARED THE GAME IN " + Mathf.Round(score).ToString() + " SECONDS!!";
        }
    }
}
