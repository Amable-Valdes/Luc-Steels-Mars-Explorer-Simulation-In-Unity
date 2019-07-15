using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLarge : MonoBehaviour
{
    private GameObject robot;

    void Awake()
    {
        robot = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Mineral") { 
            Notify_Close_Mineral(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Mineral")
        {
            Notify_Close_Mineral(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    private void Notify_Close_Mineral(GameObject mineral)
    {
        if (!robot.GetComponent<RobotControl>().close_Minerals.Contains(mineral) && !mineral.GetComponent<MineralBehaviour>().inWarehouse)
        {
            robot.GetComponent<RobotControl>().close_Minerals.Add(mineral);
        }
    }

    
}
