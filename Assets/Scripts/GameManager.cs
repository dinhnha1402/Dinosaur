using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 1f;
    public float gameSpeed { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] public Button ResumeButton;


    private Player player;
    private Spawner spawner;
    private float pauseGameSpeed;

    public float score = 0;


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        } else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        gameSpeed = 0f;
        enabled = false;
        //NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;


        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);

        

    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        hiscoreText.text = "Best: " + Mathf.FloorToInt(score).ToString("D4");
        score = 0;

    }

    public void GamePause()
    {
        if (GameManager.Instance.ResumeButton.isActiveAndEnabled == false)
        {
            pauseGameSpeed = gameSpeed;
            ResumeButton.gameObject.SetActive(true);
            gameSpeed = 0f;
            enabled = false;
            spawner.gameObject.SetActive(false);
        }


    }

    public void GameResume()
    {
        ResumeButton.gameObject.SetActive(false);
        gameSpeed = pauseGameSpeed;
        enabled = true;
        player.animatedSprite.Animate();
        spawner.gameObject.SetActive(true);
    }

    public void Running()
    {
        player.animatedSprite.Animate();
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString("D4");
    }
}
