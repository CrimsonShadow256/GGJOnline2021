using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GGJ_Online;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Image gameOverImage;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] TextMeshProUGUI victoryText;

    bool isGameOver = false;
    bool isVictory = false;
    Color gameOverFinalColor;
    Color gameOverStartingColor;
    float timeToFade = 3.0f;
    float fadeStartTime = -1.0f;

    private void Awake()
    {
        gameOverFinalColor = gameOverImage.color;
        gameOverStartingColor = Color.clear;

        gameOverScreen.SetActive(false);
        victoryScreen.SetActive(false);

    }
    public void GameOver()
    {
        isGameOver = true;
        fadeStartTime = Time.time;
    }

    public void Victory()
    {
        isVictory = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadTitle();
            return;
        }

        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            if (Input.anyKeyDown)
                ReloadScene();
        }
        else if (isVictory)
        {
            victoryText.text = "You found treasures worth <color=yellow>$"
                + ScoreManager.GetScore().ToString("F2")
                + "</color>,\nbut more treasure still awaits in the temple.";
            victoryScreen.SetActive(true);
            if (Input.anyKeyDown)
                LoadTitle();
        }

        FadeInGameOverScreen();
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    void FadeInGameOverScreen()
    {
        if (fadeStartTime < 0.0f)
            return;

        float t = (Time.time - fadeStartTime) / timeToFade;
        gameOverImage.color = Color.Lerp(gameOverStartingColor, gameOverFinalColor, t);
    }
}
