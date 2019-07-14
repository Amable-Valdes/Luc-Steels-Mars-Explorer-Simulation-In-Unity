using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complete : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject complete;

    public void MainMenu()
    {
        complete.SetActive(false);
        mainMenu.SetActive(true);
    }
}
