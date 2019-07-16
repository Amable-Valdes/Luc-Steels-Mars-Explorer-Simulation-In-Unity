using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimulationManager : MonoBehaviour
{
    private Text label_Minerals;
    private Text label_Mineral_Totals;
    private Text label_Mineral_Ground;
    private Text label_Mineral_Base;

    private GameObject cam;
    public GameObject theMineral;
    public GameObject theRobot;

    public List<GameObject> minerals;
    private List<GameObject> minerals_Stored;
    public List<GameObject> robots;

    public float start_Time;

    void Awake()
    {
        Start_Simulation();
    }

    public void Start_Simulation()
    {

        label_Minerals = GameObject.Find("Canvas/Stadistics/Label_Minerals").GetComponent<Text>();
        label_Mineral_Totals = GameObject.Find("Canvas/Stadistics/Minerals_Total").GetComponent<Text>();
        label_Mineral_Ground = GameObject.Find("Canvas/Stadistics/Minerals_Ground").GetComponent<Text>();
        label_Mineral_Base = GameObject.Find("Canvas/Stadistics/Minerals_Base").GetComponent<Text>();

        cam = GameObject.Find("CameraRig");

        start_Time = Time.time;

        minerals = new List<GameObject>();
        minerals_Stored = new List<GameObject>();
        robots = new List<GameObject>();

        Clusters_Generator();

        Robots_Generator();

        label_Mineral_Totals.text = "- In total: " + minerals.Count;
        label_Mineral_Ground.text = "- In the ground: " + minerals.Count;

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
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
        robots.Add(((GameObject)Instantiate(theRobot, new Vector3(0, 0.5f, 0), Quaternion.identity)));
    }

    public void Mineral_In_Base(GameObject mineral)
    {
        minerals_Stored.Add(mineral);
        label_Mineral_Ground.text = "- In the ground: " + (minerals.Count - minerals_Stored.Count);
        label_Mineral_Base.text = "- In the mothership: " + minerals_Stored.Count;
        if (minerals_Stored.Count == minerals.Count)
        {
            SceneManager.LoadScene("Complete_Menu", LoadSceneMode.Single);
        }
    }
}