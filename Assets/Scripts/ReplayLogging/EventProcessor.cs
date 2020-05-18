using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventProcessor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HeadPoseEventEmitter.PublishHeadPose += HeadPoseEventEmitter_PublishHeadPose;     
    }

    private void HeadPoseEventEmitter_PublishHeadPose(object sender, HeadPoseEventEmitter.HeadPoseEvent e)
    {
        WriteCharacters($"{e.Time};{e.Pos};{e.Rot}\r\n");
    }

    private void OnHeadPoseEvent(float time, Vector3 pos, Quaternion rot)
    {
        //write async
        WriteCharacters($"{time};{pos};{rot}\n");
    }

    static async void WriteCharacters(string text)
    {
        using (StreamWriter writer = File.AppendText("headpose.csv"))
        {
            await writer.WriteAsync(text);
        }
    }

    void OnDestroy()
    {
        HeadPoseEventEmitter.PublishHeadPose -= HeadPoseEventEmitter_PublishHeadPose;
        print("Script was destroyed");
    }
}
