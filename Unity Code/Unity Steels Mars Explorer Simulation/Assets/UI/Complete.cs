using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Complete : MonoBehaviour
{
    public SimulationManager simulationManager;
    public GameObject complete;

    void Awake()
    {
        simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        Finish_Simulation();
        Destroy(GameObject.Find("SimulationManager"));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

    public void Finish_Simulation()
    {
        //Minerals
        complete.transform.GetChild(3).GetComponent<Text>().text = "" + simulationManager.minerals.Count;
        //Robots
        complete.transform.GetChild(5).GetComponent<Text>().text = "" + simulationManager.robots.Count;
        //Time
        float seconds = (int)(Time.time - simulationManager.start_Time);
        int mins = (int)(seconds / 60);
        if (mins >= 1)
        {
            seconds = seconds - mins * 60;
        }
        complete.transform.GetChild(6).GetComponent<Text>().text = mins + " mins, " + seconds + " seconds";
    }
}
