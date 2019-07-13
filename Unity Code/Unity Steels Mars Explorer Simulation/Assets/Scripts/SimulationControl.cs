using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationControl : MonoBehaviour
{
    private GameObject cam;
    public GameObject theMineral;
    public GameObject theRobot;
    private List<GameObject> minerals;
    private List<GameObject> robots;

    void Awake()
    {
        Start_Simulation();
    }

    private void Start_Simulation()
    {
        minerals = new List<GameObject>();
        robots = new List<GameObject>();

        Clusters_Generator();

        Robots_Generator();

        cam = GameObject.Find("CameraRig");
        List<Transform> newList = convertLists();
        cam.GetComponent<CameraControl>().m_Targets = newList.ToArray();
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
}
