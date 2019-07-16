using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject theTrack;
    
    private float forwardMovement;
    private float lateralMovement;

    private float lastTimeUpdated;

    public GameObject mineral_Loaded;
    private GameObject track_To_Follow;
    private int tracks_waited;
    public const int NUM_MAX_TRACKS_WAITED = 4;

    public List<GameObject> close_Robots;
    public List<GameObject> close_Minerals;
    public List<GameObject> close_Tracks;
    public List<GameObject> obstacles;
    public bool inWarehouse;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastTimeUpdated = 0;
        tracks_waited = NUM_MAX_TRACKS_WAITED;

        mineral_Loaded = null;
        track_To_Follow = null;
        close_Minerals = new List<GameObject>();
        close_Tracks = new List<GameObject>();
        obstacles = new List<GameObject>();
        inWarehouse = false;
    }

    void FixedUpdate()
    {

        //	(1) IF  detect an obstacle 
        //      THEN change direction
        if (obstacles.Count > 0)
        {
            Evade_Obstacles();
        }
        //	(2) IF  carrying samples 
        //          AND at the base 
        //      THEN drop samples
        else if (mineral_Loaded != null && inWarehouse)
        {
            Unload_Mineral();
            tracks_waited = NUM_MAX_TRACKS_WAITED;
        }
        //	(3) IF  carrying samples 
        //          AND NOT at the base 
        //		THEN drop 2 crumbs 
        //		     AND travel up gradient 
        else if (mineral_Loaded != null && !inWarehouse)
        {
            Travel_To_Mothership();
        }
        //	(4) IF  detect a sample
        //          AND NOT carring mineral
        //		    AND NOT at the base 
        //		THEN pick sample up
        else if (close_Minerals.Count > 0 && mineral_Loaded == null && !inWarehouse)
        {
            if (Is_Close_To(close_Minerals[0].transform.position))
            {
                mineral_Loaded = close_Minerals[0];
                track_To_Follow = null;
            }
            else
            {
                transform.Translate(
                    new Vector3(close_Minerals[0].transform.position.x - this.transform.position.x,
                    0f,
                    close_Minerals[0].transform.position.z - this.transform.position.z)
                    * Time.deltaTime * 3, Space.World);
            }
        }
        //	(5) IF  sense crumbs 
        //		THEN pick up 1 crumb 
        //		     AND travel down gradient
        else if (close_Tracks.Count > 0)
        {
            Follow_Track();
        }
        //	(6) IF true 
        //      THEN move randomly
        else
        {
            Move_Randomly();
        }

        clearSensors();
    }

    private void Evade_Obstacles()
    {
        foreach (GameObject obstacle in obstacles) {
            if (this.transform.position.x > 0)
            {
                forwardMovement = -10;
            }
            else
            {
                forwardMovement = +10;
            }

            if (this.transform.position.z > 0)
            {
                lateralMovement = -10;
            }
            else
            {
                lateralMovement = +10;
            }

            UpdatePosition();
        }
    }

    private void Travel_To_Mothership()
    {
        GameObject mothership = Find_Nearest_Mothership();
        Move_Up_Gradient(mothership);
        UpdatePosition();
        if (tracks_waited < NUM_MAX_TRACKS_WAITED)
        {
            tracks_waited++;
        }
        else
        {
            GameObject newTrack = Instantiate(theTrack, new Vector3(this.transform.position.x, 0.25f, this.transform.position.z), Quaternion.identity);
            newTrack.GetComponent<TrackBehaviour>().Create();
            tracks_waited = 0;
        }
    }

    public GameObject Find_Nearest_Mothership()
    {
        GameObject[] motherships = GameObject.FindGameObjectsWithTag("Mothership");
        GameObject nearest = null;
        float distance_Nearest = Mathf.Infinity;
        float x = 0;
        float z = 0;
        float distance = 0;
        foreach (GameObject mothership in motherships)
        {
            x = mothership.transform.position.x - this.transform.position.x;
            z = mothership.transform.position.z - this.transform.position.z;

            distance = Mathf.Pow( ( Mathf.Pow(x,2) + Mathf.Pow(z,2) ) , 0.5f );

            if (distance_Nearest > distance)
            {
                nearest = mothership;
                distance_Nearest = distance;
            }
        }
        return nearest;
    }

    private void Move_Up_Gradient(GameObject mothership)
    {
        if (this.transform.position.x > mothership.transform.position.x)
        {
            forwardMovement = -10;
        }
        else
        {
            forwardMovement = +10;
        }

        if (this.transform.position.z > mothership.transform.position.z)
        {
            lateralMovement = -10;
        }
        else
        {
            lateralMovement = +10;
        }

        UpdatePosition();
    }

    private void Unload_Mineral()
    {
        mineral_Loaded.transform.position = new Vector3(this.transform.position.x, 0.2f, this.transform.position.z);
        mineral_Loaded = null;
    }

    

    private void Follow_Track()
    {
        if (track_To_Follow == null && close_Tracks.Count > 0)
        {
            GameObject mothership = Find_Nearest_Mothership();
            track_To_Follow = Find_Farthest_Track(mothership);
        }
        else if (track_To_Follow != null)
        {
            if (Is_Close_To(track_To_Follow.transform.position))
            {
                track_To_Follow.GetComponent<TrackBehaviour>().Remove();
                track_To_Follow = null;
            }
            else
            {
                transform.Translate(
                    new Vector3(track_To_Follow.transform.position.x - this.transform.position.x,
                    0f,
                    track_To_Follow.transform.position.z - this.transform.position.z)
                    * Time.deltaTime * 3, Space.World);
            }
        }
        close_Tracks = new List<GameObject>();
    }

    bool Is_Close_To(Vector3 position)
    {
        float lim_x_pos = position.x + 1;
        float lim_x_neg = position.x - 1;

        float lim_z_pos = position.z + 1;
        float lim_z_neg = position.z - 1;

        bool validate_x = this.transform.position.x < lim_x_pos && this.transform.position.x > lim_x_neg;
        bool validate_z = this.transform.position.z < lim_z_pos && this.transform.position.z > lim_z_neg;

        if (validate_x && validate_z) { return true; }
        return false;
    }

    private GameObject Find_Farthest_Track(GameObject mothership)
    {
        GameObject farthest = null;
        float distance_Farthest = 0;
        float x = 0;
        float z = 0;
        float distance = 0;
        foreach (GameObject track in close_Tracks)
        {
            if(track.gameObject != null) { 
                x = track.transform.position.x - mothership.transform.position.x;
                z = track.transform.position.z - mothership.transform.position.z;

                distance = Mathf.Pow((Mathf.Pow(x, 2) + Mathf.Pow(z, 2)), 0.5f);

                if (distance_Farthest < distance)
                {
                    farthest = track;
                    distance_Farthest = distance;
                }
            }
        }
        return farthest;
    }

    private void Move_Randomly() {

        if ((Time.time - lastTimeUpdated) > 1f)
        {
            if (0.5 > Random.Range(0f, 1f)) { forwardMovement = Random.Range(0f, 10f); } else { forwardMovement = -Random.Range(0f, 10f); }
            if (0.5 > Random.Range(0f, 1f)) { lateralMovement = Random.Range(0f, 10f); } else { lateralMovement = -Random.Range(0f, 10f); }
            lastTimeUpdated = Time.time;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.Translate(new Vector3(Time.fixedDeltaTime * forwardMovement, 0f, Time.fixedDeltaTime * lateralMovement), Space.World);
        if (mineral_Loaded != null)
        {
            mineral_Loaded.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
        }
    }

    private void clearSensors()
    {
        close_Robots = new List<GameObject>();
        close_Tracks = new List<GameObject>();
        close_Minerals = new List<GameObject>();
        obstacles = new List<GameObject>();
        inWarehouse = false;
    }
}