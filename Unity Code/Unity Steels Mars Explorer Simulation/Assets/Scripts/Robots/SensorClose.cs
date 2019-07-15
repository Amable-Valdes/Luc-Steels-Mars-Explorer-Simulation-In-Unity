using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorClose : MonoBehaviour
{
    private GameObject robot;

    void Awake()
    {
        robot = this.transform.parent.gameObject;
    }

    void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            Notify_Close_Robot(collisionInfo.GetComponent<Collider>().gameObject);
        }

        if (collisionInfo.tag == "Track")
        {
            Notify_Close_Track(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "Robot")
        {
            Notify_Close_Robot(collisionInfo.GetComponent<Collider>().gameObject);
        }

        if (collisionInfo.tag == "Track")
        {
            Notify_Close_Track(collisionInfo.GetComponent<Collider>().gameObject);
        }
    }

    private void Notify_Close_Robot(GameObject robot)
    {
        if (!robot.GetComponent<RobotControl>().close_Robots.Contains(robot))
        {
            robot.GetComponent<RobotControl>().close_Robots.Add(robot);
        }
    }

    private void Notify_Close_Track(GameObject track)
    {
        if (!robot.GetComponent<RobotControl>().close_Tracks.Contains(track))
        {
            GameObject mothership = robot.GetComponent<RobotControl>().Find_Nearest_Mothership();

            float distance_Track_Mothership = Mathf.Pow(
                (
                    Mathf.Pow(mothership.transform.position.x - track.transform.position.x, 2) +
                    Mathf.Pow(mothership.transform.position.z - track.transform.position.z, 2))
                , 0.5f);
            float distance_Robot_Mothership = Mathf.Pow(
                (
                    Mathf.Pow(mothership.transform.position.x - robot.transform.position.x, 2) +
                    Mathf.Pow(mothership.transform.position.z - robot.transform.position.z, 2))
                , 0.5f);

            if (distance_Robot_Mothership < distance_Track_Mothership)
            {
                robot.GetComponent<RobotControl>().close_Tracks.Add(track);
            }
        }
    }
}
