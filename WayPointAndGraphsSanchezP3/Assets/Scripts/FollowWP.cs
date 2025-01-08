using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 10.0f;
    public float rotSpeed = 10.0f;
    GameObject tracker;
    public float lookAhead = 10.0f; 
    // Start is called before the first frame update
    void Start()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = true;
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;
    }

    void ProgressTracker()
    {

        if (Vector3.Distance(tracker.transform.position, this.transform.position) > lookAhead) return;
        if (Vector3.Distance(tracker.transform.position, waypoints[currentWP].transform.position) < 3)
            currentWP++;

        if (currentWP >= waypoints.Length)
            currentWP = 0;

        tracker.transform.LookAt(waypoints[currentWP].transform);
        tracker.transform.Translate(0, 0, (speed + 20) * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        ProgressTracker();

       Quaternion lookatWP = Quaternion.LookRotation(tracker.transform.position - this.transform.position);

       this.transform.rotation = Quaternion.Slerp(transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

       this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
