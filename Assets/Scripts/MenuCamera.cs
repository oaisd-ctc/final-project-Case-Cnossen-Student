using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween;

public class MenuCamera : MonoBehaviour
{
    public Transform[] cameraPath;
    public float speed = 1; //Units per Second 
    Tween.Tween cameraTween;
    // Start is called before the first frame update
    void Start()
    {
        cameraTween = new Tween.Tween(transform, 5, EasingStyle.Linear, EasingDirection.In);
        transform.position = cameraPath[0].position;
        StartCoroutine(RunCutscene());
    }

    // Update is called once per frame
    void Update()
    {
        cameraTween.TweenFrame();       
    }

    IEnumerator RunCutscene()
    {
        while (true)
        {
            foreach (Transform x in cameraPath)
            {
                cameraTween.duration = (transform.position - x.position).magnitude / speed;
                cameraTween.TweenPositionRotation(x.position, x.rotation);
                yield return new WaitForSeconds(cameraTween.duration);
            }
        }
    }
}