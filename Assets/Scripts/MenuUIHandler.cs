using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject gameoverMenu;
    public GameObject background;
    public GameObject gameStats;
    public TextMeshProUGUI gameoverText;
    private TextMeshProUGUI[] gameStatsText;
    private int second;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        gameStatsText = gameStats.GetComponentsInChildren<TextMeshProUGUI>();
        second = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        StartCoroutine(Countdown());
        startMenu.SetActive(false);
        background.SetActive(false);
        gameStats.SetActive(true);
    }

    public void EndGame()
    {
        GameManager.instance.isGameOver = true;
        Time.timeScale = 0;
        gameStats.SetActive(false);
        gameoverMenu.SetActive(true);
        background.SetActive(true);
        int score = --GameObject.Find("SpawnManager").GetComponent<SpawnManager>().waveNumber;
        if (score > GameManager.instance.bestScore)
        {
            GameManager.instance.bestScore = score;
        }
        gameoverText.text = $"Stage{(score > 1 ? "s" : "")} Cleared: {score}\nPersonal Best: {GameManager.instance.bestScore}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateWaveNumber(int waveNumber)
    {
        gameStatsText[0].text = "Wave: " + waveNumber;
    }

    IEnumerator Countdown()
    {
        while (second > 0)
        {
            gameStatsText[1].text = $"{second}";
            second--;
            yield return new WaitForSecondsRealtime(1);
        }
        gameStatsText[1].text = "GO";
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        GameManager.instance.isGameOver = false;
        gameStatsText[1].text = "";
    }

}
