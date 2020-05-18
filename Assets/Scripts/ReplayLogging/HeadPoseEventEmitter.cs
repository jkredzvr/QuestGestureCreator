using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeadPoseEventEmitter : MonoBehaviour
{

    public class HeadPoseEvent : EventArgs
    {
        public float Time;
        public Vector3 Pos;
        public Quaternion Rot;

        public HeadPoseEvent(float time, Vector3 pos, Quaternion rot)
        {
            Time = time;
            Pos = pos;
            Rot = rot;
        }
    }

    [SerializeField] private Camera headsetCamera;
    private float startTime;
    public static event EventHandler<HeadPoseEvent> PublishHeadPose;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        InvokeRepeating("PublishHeadPoseEvent", 1f, .01f);  //1s delay, repeat every 1s               
    }

    //Every x seconds emit event with
    //timestamp
    //Headpose rotation and position
    private void PublishHeadPoseEvent()
    {
        float deltaTime = Time.time - startTime;

        Vector3 pos = headsetCamera.transform.position;
        Quaternion rot = headsetCamera.transform.rotation;

        //Debug.Log($"{deltaTime} {pos} {rot}");

        HeadPoseEvent e = new HeadPoseEvent(deltaTime, pos, rot);
        EventHandler<HeadPoseEvent> handler = PublishHeadPose;
        if (handler != null)
        {
            handler(this, e);
        }
    }
}
