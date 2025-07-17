using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentPoints;

    public int CurrentPoints { get { return currentPoints; } private set { } }

    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Player Information")]
    [SerializeField] TextMeshProUGUI playerCurrentLivesText;
    [SerializeField] int playerStartingLives = 10;
    [SerializeField] int playerCurrentLives;

    [Header("Finish Line Information")]
    [SerializeField] int currentStageIndex; //must sort the build setting scenes
    public int CurrentStageIndex { get { return currentStageIndex; } private set { } }

    [Header ("Respawn Location")]
    [SerializeField] private Vector3 startingPosition;
    public Vector3 StartingPosition { get {  return startingPosition; } private set {  } }

    void Start()
    {
        currentPoints = 0;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText($"{currentPoints}");

        playerCurrentLives = playerStartingLives;

        playerCurrentLivesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        playerCurrentLivesText.SetText($"X{playerCurrentLives}");

        currentStageIndex = SceneManager.GetActiveScene().buildIndex;

        startingPosition = new Vector3(-8, -1.5f, 0);
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
