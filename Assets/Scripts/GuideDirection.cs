using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideDirection : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(.02f, 1, (pointB.position - pointA.position).magnitude);
        transform.position = (pointB.position + pointA.position) / 2;
        transform.LookAt(pointB.position);
    }
}
