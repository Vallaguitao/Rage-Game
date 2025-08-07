using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

    [Header("Public Game Knowledge")]
    public static GameManager gameManagerScript;
    public bool isPaused;

    [Header("Clean Up (Player Information -> Formerly located on traps")]
    public GameObject player;
    public PlayerController playerControllerScript;
    public SpriteRenderer playerRenderer;
    public AudioManager audioManager;

    [SerializeField] private EventSystem eventSystem1;

    [Header("Event Managers (Formerly on Player Controller")]
    [SerializeField] private CanvasGroup pausedMenu;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onDePause;

    public CanvasGroup PausedMenu { get { return pausedMenu; } private set { } }
    public UnityEvent OnPause { get { return onPause; } private set { } }
    public UnityEvent OnDePause { get { return onDePause; } private set { } }

    private void Awake()
    {
        gameManagerScript = this;
        isPaused = false;

        //Player Information
        player = GameObject.FindGameObjectWithTag("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
        playerRenderer = player.GetComponent<SpriteRenderer>();
        audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        //Player Current Points
        currentPoints = 0;
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        scoreText.SetText($"{currentPoints}");


        //Player Live Text
        playerCurrentLives = playerStartingLives;
        playerCurrentLivesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
        playerCurrentLivesText.SetText($"X{playerCurrentLives}");

        //Load Stage
        currentStageIndex = SceneManager.GetActiveScene().buildIndex;

        onPause.AddListener(playerControllerScript.OnPause);
        onDePause.AddListener(playerControllerScript.OnDePause);

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

    

    public IEnumerator PlayerRespawn()
    {
        player.transform.position = gameManagerScript.StartingPosition;
        yield return new WaitForSeconds(1f);
        playerRenderer.enabled = true;
    }

    public void PlayerDied()
    {

        playerRenderer.enabled = false;

        LoseALife();

        StartCoroutine("PlayerRespawn");

        playerRenderer.enabled = true;
        //put an invinsibility period
    }

    #region Buttons Control

    public void PlayerPause()
    {
        playerControllerScript.OnPause();
    }

    public void PlayerUnPause()
    {
        playerControllerScript.OnDePause();
    }

    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void UnselectButton()
    {

        eventSystem1.SetSelectedGameObject(null);

    }

    public void RestartStage()
    {

        SceneManager.LoadScene(GameManager.gameManagerScript.CurrentStageIndex);

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit(); // 
        #endif
    }

#endregion
}
