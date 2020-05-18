using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CameraPlayback : MonoBehaviour
{

    Queue<HeadPoseEventEmitter.HeadPoseEvent> headPoseEventQueue = new Queue<HeadPoseEventEmitter.HeadPoseEvent>();
    float startTime = 0.0f;
    HeadPoseEventEmitter.HeadPoseEvent headpose;
    public Transform CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader("headpose.csv"))
            {
                string line;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    Debug.Log(line+"\n");

                    string[] sArray = line.Split(';');

                    float time = float.Parse(sArray[0]);
                    Vector3 pos = StringToVector3(sArray[1]);
                    Quaternion rot = StringToQuaternion(sArray[2]);

                    HeadPoseEventEmitter.HeadPoseEvent hEvent = new HeadPoseEventEmitter.HeadPoseEvent(time, pos, rot);

                    headPoseEventQueue.Enqueue(hEvent);
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        startTime = Time.time;


    }

    private Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    private Quaternion StringToQuaternion(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Quaternion
        Quaternion result = new Quaternion(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]),
            float.Parse(sArray[3])
            );

        return result;
    }

    void Update() 
    {
        if(headPoseEventQueue.Count > 0)
        {
            if(headpose == null)
            {
                headpose = headPoseEventQueue.Dequeue();
            }

            float time = Time.time - startTime;

            if(headpose.Time <= time)
            {
                Debug.Log(headpose.Pos);
                UpdateHeadPose(headpose);
                headpose = null;
            }
        }
    }

    private void UpdateHeadPose(HeadPoseEventEmitter.HeadPoseEvent headPoseEvent)
    {
        CameraTransform.SetPositionAndRotation(headPoseEvent.Pos, headpose.Rot);
    }
}
