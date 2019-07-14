using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void Start_Simulation()
    {
        GameObject.Find("SimulationManager").GetComponent<SimulationManager>().Start_Simulation();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
