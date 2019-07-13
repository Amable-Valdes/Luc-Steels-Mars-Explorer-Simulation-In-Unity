using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopiaRobotControl : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject theTrack;
    
    private float forwardMovement;
    private float lateralMovement;

    private float lastTimeUpdated;

    public GameObject mineral_Loaded;
    public List<GameObject> close_Minerals;
    public List<GameObject> close_Tracks;
    public List<GameObject> obstacles;
    public bool inWarehouse;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastTimeUpdated = 0;
        mineral_Loaded = null;
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
            mineral_Loaded = close_Minerals[0];
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
        Instantiate(theTrack, new Vector3(this.transform.position.x, 0.25f, this.transform.position.z + 1), Quaternion.identity);
        Instantiate(theTrack, new Vector3(this.transform.position.x + 1, 0.25f, this.transform.position.z), Quaternion.identity);
    }

    private GameObject Find_Nearest_Mothership()
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
        //GameObject track = Find_Farthest_Track();
        //Move_Down_Gradient(track);
        //Destroy(track);
        try
        {
            GameObject mothership = Find_Nearest_Mothership();
            GameObject track = Find_Farthest_Track(mothership);
            Move_Down_Gradient(track);
            
        }
        catch
        {
            Debug.Log("Error");
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            forwardMovement = Random.Range(-10f, 11f);
            lateralMovement = Random.Range(-10f, 11f);
            Move_Randomly();
        }
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

    private Vector3 posicionEsperada = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

    private void Move_Down_Gradient(GameObject track)
    {
        /*
        if (this.transform.position.x > track.transform.position.x)
        {
            forwardMovement = -10;
        }
        else
        {
            forwardMovement = +10;
        }

        if (this.transform.position.z > track.transform.position.z)
        {
            lateralMovement = -10;
        }
        else
        {
            lateralMovement = +10;
        }*/

        if (posicionEsperada == new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity) || this.transform.position == posicionEsperada)
        {
            //this.transform.position = new Vector3(track.transform.position.x, this.transform.position.y, track.transform.position.z);
            transform.Translate(new Vector3(Time.fixedDeltaTime * (track.transform.position.x - this.transform.position.x), 0f, Time.fixedDeltaTime * (track.transform.position.z - this.transform.position.z)), Space.World);
            posicionEsperada = track.transform.position;
            Destroy(track);
            close_Tracks.Remove(track);
        }
        
    }

    private void Move_Randomly() {

        if ((Time.time - lastTimeUpdated) > 1f)
        {
            forwardMovement = Random.Range(-10f, 11f);
            lateralMovement = Random.Range(-10f, 11f);
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
        close_Tracks = new List<GameObject>();
        close_Minerals = new List<GameObject>();
        obstacles = new List<GameObject>();
        inWarehouse = false;
    }
}