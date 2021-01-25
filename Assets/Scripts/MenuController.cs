using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject btn_Get_Started;
    public GameObject btn_How_It_Works;


    public string FirstRun
    {
        set
        {
            PlayerPrefs.SetString("FirstRun", value);
        }
        get
        {
            return PlayerPrefs.GetString("FirstRun","true");
        }
    }

    private void Start()
    {
        if (FirstRun == "true")
        {
            FirstRun = "false";
            btn_Get_Started.SetActive(false);
            btn_How_It_Works.SetActive(true);
        }
        else
        {
            btn_Get_Started.SetActive(true);
            btn_How_It_Works.SetActive(false);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
