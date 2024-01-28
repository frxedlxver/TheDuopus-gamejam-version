using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}