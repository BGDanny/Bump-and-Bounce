using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControlUIHandler : MonoBehaviour
{
    private TextMeshProUGUI controlText;
    private List<string> controls = new List<string>();
    private int index = 0;
    public GameObject nextButton;
    public GameObject prevButton;
    public GameObject exitButton;
    public GameObject[] powerup;
    public GameObject dummy;
    private Dummy dummyObject;
    private bool spawnDummy;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        controlText = GetComponentInChildren<TextMeshProUGUI>();
        controls.Add("Use Q E to rotate camera");
        controls.Add("Use W S A D to move");
        controls.Add("Pick up the powerup");
        controls.Add("The yellow powerup allows you to bump into enemies with greater force, now try knock the dummy off");
        controls.Add("The green powerup allows you to launch rockets towards enemies, try press Space");
        controls.Add("The red powerup allows you to smash the ground and pushing close enemies back, try press Space");
        controls.Add("Remember all powerups have time limits outside this tutorial!");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnDummy)
        {
            dummyObject = FindObjectOfType<Dummy>();
            if (dummyObject == null)
            {
                Instantiate(dummy, new Vector3(5, 1, -5), dummy.transform.rotation);

            }
        }
    }

    public void NextText()
    {
        controlText.text = controls[++index];
        if (index == 1)
        {
            prevButton.SetActive(true);
        }
        else if (index == 2)
        {
            Instantiate(powerup[0], new Vector3(-5, 0, -5), powerup[0].transform.rotation);
        }
        else if (index == 3)
        {
            Instantiate(dummy, new Vector3(5, 1, -5), dummy.transform.rotation);
            spawnDummy = true;
        }
        else if (index == 4)
        {
            Instantiate(powerup[1], new Vector3(-5, 0, 0), powerup[1].transform.rotation);

        }
        else if (index == 5)
        {
            Instantiate(powerup[2], new Vector3(-5, 0, 5), powerup[2].transform.rotation);

        }
        else if (index == controls.Count - 1)
        {
            nextButton.SetActive(false);
            exitButton.SetActive(true);
        }
    }

    public void PrevText()
    {
        controlText.text = controls[--index];
        if (index == 0)
        {
            prevButton.SetActive(false);
        }
        else if (index != controls.Count - 1)
        {
            exitButton.SetActive(false);

            nextButton.SetActive(true);
        }

    }

    public void ExitToMenu()
    {
        SceneManager.LoadSceneAsync("Main");
    }
}
