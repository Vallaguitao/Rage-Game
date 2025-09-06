using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public bool isInvincibleState;

    [SerializeField] private EventSystem eventSystem1;

    [Header("Event Managers (Formerly on Player Controller")]
    [SerializeField] private CanvasGroup pausedMenu;
    [SerializeField] private UnityEvent onPause;
    [SerializeField] private UnityEvent onDePause;

    [Header("Pause")]
    [SerializeField] private Slider loadingSlider;

    public CanvasGroup PausedMenu { get { return pausedMenu; } private set { } }
    public UnityEvent OnPause { get { return onPause; } private set { } }
    public UnityEvent OnDePause { get { return onDePause; } private set { } }

    [SerializeField] private string currentActiveScene;

    private void Awake()
    {
        gameManagerScript = this;

        InputSystem.DisableDevice(Mouse.current);
        Cursor.visible = false;
        
        //isPaused = false;
        //Player Information

        currentActiveScene = SceneManager.GetActiveScene().name;

        isInvincibleState = false;

        //temp (because still no persistence)
        if ((currentActiveScene == "Main Menu") || (currentActiveScene == "Credits") || (currentActiveScene == "Start Menu"))
        {
            return;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerControllerScript = player.GetComponent<PlayerController>();
            playerRenderer = player.GetComponent<SpriteRenderer>();
            audioManager = GameObject.Find("Audio").GetComponent<AudioManager>();
        }
        
    }

    void Start()
    {
        isPaused = false;
        if ((currentActiveScene == "Main Menu") || (currentActiveScene == "Credits") || (currentActiveScene == "Start Menu"))
        {
            return;
        }
        else
        {
            //Player Current Points
            currentPoints = 0;
            scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
            scoreText.SetText(string.Format("{0:N0}", currentPoints));


            //Player Live Text
            playerCurrentLives = playerStartingLives;
            playerCurrentLivesText = GameObject.Find("Lives").GetComponent<TextMeshProUGUI>();
            playerCurrentLivesText.SetText(string.Format("{0:N0}", $"X{playerCurrentLives}"));

            //Load Stage
            currentStageIndex = SceneManager.GetActiveScene().buildIndex;

            onPause.AddListener(playerControllerScript.OnPause);
            onDePause.AddListener(playerControllerScript.OnDePause);
        }
        

    }

    public void UpdateScore(int value)
    {
        currentPoints += value;
        scoreText.SetText(string.Format("{0:N0}" , currentPoints));
    }

    public void LoseALife()
    {
        playerCurrentLives--;
        playerCurrentLivesText.SetText( "X" + string.Format("{0:N0}", playerCurrentLives)); // changed the $"X{playerCurrentLives} because comma wont show
    }

    public void AddALife()
    {
        playerCurrentLives++;
        playerCurrentLivesText.SetText("X" + string.Format("{0:N0}", playerCurrentLives));
    }

    

    public IEnumerator PlayerRespawn()
    {
        player.transform.position = gameManagerScript.StartingPosition;

        yield return new WaitForSeconds(1f);

        player.GetComponent<Animator>().SetBool("IsInvincible", false);
        isInvincibleState = false;
        playerRenderer.enabled = true;
    }

    public void PlayerDied()
    {
        if(!isInvincibleState)
        {
            playerRenderer.enabled = false;

            LoseALife();
            isInvincibleState = true;
            player.GetComponent<Animator>().SetBool("IsInvincible", true);

            StartCoroutine(PlayerRespawn());
            

            playerRenderer.enabled = true;
        }
        else
        {
            print("I am invincible, Cannot die");
        }

    }

    #region Pause Buttons Control

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

        playerControllerScript.CancelledControl();
        StartCoroutine(LoadLevelAsynch(currentStageIndex));
        
    }

    //this is nothing
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        playerControllerScript.CancelledControl();
        print($"Scene Loaded: {scene}, Mode {mode}");
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

    public void LoadNextLevel()
    {
        playerControllerScript.CancelledControl();
        StartCoroutine(LoadLevelAsynch(++currentStageIndex));
    }

    public void LoadStageSelect(int stageIndex)
    {
        playerControllerScript.CancelledControl();
        StartCoroutine(LoadLevelAsynch(stageIndex));
    }

    //the 2 IEnumerator below has the same code
    public IEnumerator LoadLevelAsynch(string levelToLoad)
    {

        if (loadingSlider != null)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadingSlider.value = progressValue;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            print("No Slider");
        }

    }

    public IEnumerator LoadLevelAsynch(int levelToLoadIndex)
    {

        if (loadingSlider != null)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoadIndex);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                loadingSlider.value = progressValue;
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            print("No Slider");
        }

    }
}
