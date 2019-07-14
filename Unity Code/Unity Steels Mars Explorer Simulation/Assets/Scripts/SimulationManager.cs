using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationManager : MonoBehaviour
{

    private GameObject mainMenu;
    private GameObject complete;

    private Text label_Minerals;
    private Text label_Mineral_Totals;
    private Text label_Mineral_Base;

    private GameObject cam;
    public GameObject theMineral;
    public GameObject theRobot;

    private List<GameObject> minerals;
    private List<GameObject> minerals_Stored;
    private List<GameObject> robots;

    private float start_Time;

    void Awake()
    {
        mainMenu = GameObject.Find("Canvas/MainMenu");
        mainMenu.SetActive(true);

        label_Minerals = GameObject.Find("Canvas/Stadistics/Label_Minerals").GetComponent<Text>();
        label_Minerals.enabled = false;

        label_Mineral_Totals = GameObject.Find("Canvas/Stadistics/Minerals_Total").GetComponent<Text>();
        label_Mineral_Totals.enabled = false;

        label_Mineral_Base = GameObject.Find("Canvas/Stadistics/Minerals_Base").GetComponent<Text>();
        label_Mineral_Base.enabled = false;

        complete = GameObject.Find("Canvas/Complete");
        complete.SetActive(false);
        
        cam = GameObject.Find("CameraRig");
        cam.GetComponent<CameraControl>().m_Targets = new List<Transform>().ToArray();
    }

    public void Start_Simulation()
    {
        start_Time = Time.time;

        mainMenu.SetActive(false);
        label_Minerals.enabled = true;
        label_Mineral_Totals.enabled = true;
        label_Mineral_Base.enabled = true;

        minerals = new List<GameObject>();
        minerals_Stored = new List<GameObject>();
        robots = new List<GameObject>();

        Clusters_Generator();

        Robots_Generator();

        label_Mineral_Totals.text = "- In the ground: " + minerals.Count;

        cam.GetComponent<CameraControl>().m_Targets = convertLists().ToArray();
    }

    private void Clusters_Generator()
    {
        float positionX;
        float positionZ;
        int numClusters = Random.Range(2, 7);
        for (int i = 0; i < numClusters; i++)
        {
            positionX = Random.Range(-40, 41);
            positionZ = Random.Range(-40, 41);
            Mineral_Generator(positionX, positionZ);
        }
    }

    private void Mineral_Generator(float clusterPositionX, float clusterPositionZ)
    {
        int numMineralsToCreate = Random.Range(3, 13);
        float newPositionX;
        float newPositionZ;
        Object newMineral;
        for (var i = 0; i < numMineralsToCreate; i++)
        {
            newPositionX = Random.Range(-10, 11) + clusterPositionX;
            newPositionZ = Random.Range(-10, 11) + clusterPositionZ;

            if (newPositionX > 40)
            {
                newPositionX = clusterPositionX - Random.Range(0, 10);
            }
            else if (newPositionX < -40)
            {
                newPositionX = clusterPositionX + Random.Range(0, 10);
            }

            if (newPositionZ > 40)
            {
                newPositionZ = clusterPositionZ - Random.Range(0, 10);
            }
            else if (newPositionZ < -40)
            {
                newPositionZ = clusterPositionZ + Random.Range(0, 10);
            }
            
            newMineral = Instantiate(theMineral, new Vector3(newPositionX, 0.2f, newPositionZ), Quaternion.identity);
            minerals.Add(((GameObject)newMineral));
        }

    }

    private List<Transform> convertLists()
    {
        List<Transform> newList = new List<Transform>();

        foreach (GameObject mineral in minerals)
        {
            newList.Add(mineral.transform);
        }

        foreach (GameObject robot in robots)
        {
            newList.Add(robot.transform);
        }

        return newList;
    }

    private void Robots_Generator()
    {
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(5, 0.5f, 5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(-5, 0.5f, 5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, -5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(5, 0.5f, -5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(-5, 0.5f, -5), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(5, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(-5, 0.5f, 0), Quaternion.identity)));
    }

    public void Mineral_In_Base(GameObject mineral)
    {
        minerals_Stored.Add(mineral);
        label_Mineral_Base.text = "- In the mothership: " + minerals_Stored.Count;
        if (minerals_Stored.Count == minerals.Count)
        {
            Finish_Simulation();
        }
    }

    public void Finish_Simulation()
    {
        //Minerals
        complete.transform.GetChild(3).GetComponent<Text>().text = "" + minerals.Count;
        //Robots
        complete.transform.GetChild(5).GetComponent<Text>().text = "" + robots.Count;
        //Time
        float seconds = (int) (Time.time - start_Time);
        int mins = (int)(seconds / 60);
        if (mins >= 1)
        {
            seconds = seconds - mins * 60;
        }
        complete.transform.GetChild(6).GetComponent<Text>().text = mins + " mins, " + seconds + " seconds";


        mainMenu.SetActive(false);
        label_Minerals.enabled = false;
        label_Mineral_Totals.enabled = false;
        label_Mineral_Base.enabled = false;
        complete.SetActive(true);
    }
}