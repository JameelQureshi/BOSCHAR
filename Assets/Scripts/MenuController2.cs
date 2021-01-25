using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController2 : MonoBehaviour
{

    public GameObject Menu;
    public GameObject About;
    public GameObject HowtoPlay;
    public GameObject Explore;
    public GameObject Settings;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DeleteAll()
    {
        GameObject[] boschs = GameObject.FindGameObjectsWithTag("Bosch");
        foreach (GameObject bosch in boschs)
        {
            Destroy(bosch);
        }
    }


    public void CloseButton()
    {
        Debug.Log("Pls");

        Menu.SetActive(false);
        HowtoPlay.SetActive(false);
        Explore.SetActive(false);
        Settings.SetActive(false);
        About.SetActive(false);

    }



    public void MenuButton() {


        Menu.SetActive(true);
        HowtoPlay.SetActive(false);
        Explore.SetActive(false);
        Settings.SetActive(false);
        About.SetActive(false);

    }
    public void HowtoPlayButton()
    {


       // Menu.SetActive(false);
        HowtoPlay.SetActive(true);
        Explore.SetActive(false);
        Settings.SetActive(false);
        About.SetActive(false);

    }

    public void AboutButton()
    {


       // Menu.SetActive(false);
        HowtoPlay.SetActive(false);
        Explore.SetActive(false);
        Settings.SetActive(false);
        About.SetActive(true);

    }


    public void ExploreButton()
    {


       // Menu.SetActive(false);
        HowtoPlay.SetActive(false);
        Explore.SetActive(true);
        Settings.SetActive(false);
        About.SetActive(false);

    }
    public void SettingButton()
    {


      //  Menu.SetActive(false);
        HowtoPlay.SetActive(false);
        Explore.SetActive(false);
        Settings.SetActive(true);
        About.SetActive(false);

    }
}
