using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentPoints;

    public int CurrentPoints { get { return currentPoints; } private set { } }

    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Player Information")]
    [SerializeField] TextMeshProUGUI playerCurrentLivesText;
    [SerializeField] int playerStartingLives = 10;
    [SerializeField] int playerCurrentLives;

    void Start()
    {
        currentPoints = 0;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText($"{currentPoints}");

        playerCurrentLives = playerStartingLives;

        playerCurrentLivesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        playerCurrentLivesText.SetText($"X{playerCurrentLives}");
    }

    public void UpdateScore(int value)
    {
        currentPoints += value;
        scoreText.SetText(string.Format("{0:N0}" , currentPoints));
    }

    public void LoseALife()
    {
        playerCurrentLives--;
        playerCurrentLivesText.SetText($"X{playerCurrentLives}");
    }

    public void AddALife()
    {
        playerCurrentLives++;
        playerCurrentLivesText.SetText($"X{playerCurrentLives}");
    }
}
