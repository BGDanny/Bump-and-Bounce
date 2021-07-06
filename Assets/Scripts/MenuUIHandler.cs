using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject gameoverMenu;
    public GameObject background;
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
    }

    public void EndGame()
    {
        GameManager.instance.isGameOver = true;
        Time.timeScale = 0;
        gameoverMenu.SetActive(true);
        background.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
