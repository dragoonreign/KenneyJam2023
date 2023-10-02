using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public enum PlatformType {
        LOOP, 
        BACKTRACK
    }
    public PlatformType _PlatformType;
    public List<Transform> waypoints;
    public GameObject waypointsGO;
    private Transform target;
    private int i = 0;
    public float speed;
    float step;
    bool waypointStart = true;
    bool pausedPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        Transform grabWaypoints = waypointsGO.transform;
        foreach (Transform waypoint in grabWaypoints) waypoints.Add(waypoint);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_PlatformType == PlatformType.BACKTRACK) {
            UpdateWaypoints();
        }
        if (_PlatformType == PlatformType.LOOP) {
            UpdateWaypointsLOOP();
        }
    }

    void UpdateWaypointsLOOP()
    {
        if (i < waypoints.Count) {
            waypointStart = true;
            step = speed * Time.deltaTime; // calculate distance to move
            target = waypoints[i];
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                i++;
            } else {
                // rb.MovePosition(transform.position + target.position * step);
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
        } else {
            i = 0;
        }
    }

    void UpdateWaypoints()
    {
        if (pausedPlatform) return;

        if (waypointStart == true) {
            if (i < waypoints.Count) {
                waypointStart = true;
                step = speed * Time.deltaTime; // calculate distance to move
                target = waypoints[i];
                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    i++;
                } else {
                    // rb.MovePosition(transform.position + target.position * step);
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
            } else {
                StartCoroutine(PausePlatform());
            }
        }

        if (waypointStart == false) {
            if (i > 0) {
                step = speed * Time.deltaTime; // calculate distance to move
                target = waypoints[i-1];
                if (Vector3.Distance(transform.position, target.position) < 0.001f)
                {
                    i--;
                } else {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                }
            } else {
                StartCoroutine(PausePlatform());
            }
        }
    }

    IEnumerator PausePlatform()
    {
        pausedPlatform = true;
        yield return new WaitForSeconds(3f);
        if (waypointStart) {
            waypointStart = false;
        } else {
            waypointStart = true;
        }
        pausedPlatform = false;
    }
}
