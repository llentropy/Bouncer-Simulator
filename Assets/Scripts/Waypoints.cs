using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public List<Transform> wayPointsList;

    private static Waypoints _instance;
    public static Waypoints Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Waypoints();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
}
