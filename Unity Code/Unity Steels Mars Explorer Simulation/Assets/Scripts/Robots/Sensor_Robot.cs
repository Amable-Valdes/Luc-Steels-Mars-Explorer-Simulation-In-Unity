using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_Robot : MonoBehaviour
{
    private GameObject robot;

    void Awake()
    {
        robot = this.gameObject.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            Notify_Close_Robot(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            Notify_Close_Robot(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    private void Notify_Close_Robot(GameObject robot)
    {
        if (!robot.GetComponent<RobotControl>().close_Robots.Contains(robot))
        {
            robot.GetComponent<RobotControl>().close_Robots.Add(robot);
        }
    }
}
