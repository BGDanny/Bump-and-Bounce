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
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI gameoverText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        background.SetActive(false);
        GameManager.instance.isGameOver = false;
        waveText.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        GameManager.instance.isGameOver = true;
        Time.timeScale = 0;
        gameoverMenu.SetActive(true);
        background.SetActive(true);
        int score = --GameObject.Find("SpawnManager").GetComponent<SpawnManager>().waveNumber;
        if (score > GameManager.instance.bestScore)
        {
            GameManager.instance.bestScore = score;
        }
        gameoverText.text = $"Stage Cleared: {score}\nPersonal Best: {GameManager.instance.bestScore}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateWaveNumber(int waveNumber)
    {
        waveText.text = "Wave: " + waveNumber;
    }

}
