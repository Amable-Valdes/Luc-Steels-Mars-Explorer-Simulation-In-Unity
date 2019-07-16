using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBehaviour : MonoBehaviour
{
    public int number;

    public float creation_Time;

    public void Create()
    {
        number = 2;
        creation_Time = Time.time;
    }

    void Update()
    {
        if ((Time.time - creation_Time) > 1)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Add()
    {
        Debug.Log(number);
        number = number + 2;
    }

    public void Remove()
    {
        number = number - 1;
        if (number < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void Asimilate_Track(GameObject other)
    {
        int otherNumber = other.GetComponent<TrackBehaviour>().number;
        Destroy(other);
        number = number + otherNumber;
    }
}
