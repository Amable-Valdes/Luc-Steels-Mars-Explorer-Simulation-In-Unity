using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralSpawn : MonoBehaviour
{
    /*
    public GameObject theMineral;
    private List<GameObject> minerals;

    // Start is called before the first frame update
    void Start()
    {
        minerals = new List<GameObject>();

        ClusterGenerator();

        GameObject.Find("SimulationControl").GetComponent<SimulationControl>().minerals = minerals;
    }

    private IEnumerator ClusterGenerator()
    {
        float positionX;
        float positionZ;
        int numClusters = Random.Range(2, 7);
        for (int i = 0; i < numClusters; i++)
        {
            positionX = Random.Range(-40, 41);
            positionZ = Random.Range(-40, 41);
            MineralGenerator(positionX, positionZ);
        } return null;

    }

    private void MineralGenerator(float clusterPositionX, float clusterPositionZ)
    {
        int numMineralsToCreate = Random.Range(3, 13);
        float newPositionX;
        float newPositionZ;
        Object newMineral;
        for (var i = 0; i < numMineralsToCreate; i++)
        {
            newPositionX = Random.Range(-20, 21) + clusterPositionX;
            newPositionZ = Random.Range(-20, 21) + clusterPositionZ;

            if (newPositionX > 40)
            {
                newPositionX = clusterPositionX - Random.Range(0, 20);
            }
            else if (newPositionX < -40)
            {
                newPositionX = clusterPositionX + Random.Range(0, 20);
            }

            if (newPositionZ > 40)
            {
                newPositionZ = clusterPositionZ - Random.Range(0, 20);
            }
            else if (newPositionZ < -40)
            {
                newPositionZ = clusterPositionZ + Random.Range(0, 20);
            }


            newMineral = Instantiate(theMineral, new Vector3(newPositionX, 0.2f, newPositionZ), Quaternion.identity);
            minerals.Add(((GameObject)newMineral));
        }

    }*/
}
