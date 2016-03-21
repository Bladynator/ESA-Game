using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class PhotoShot : MonoBehaviour
{
    Texture2D selfie;
    string fileLocation;
    string myScreenshotLocation;

    public void TakePhoto()
    {
        selfie = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        selfie.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);

        selfie.Apply();
        byte[] bytes = selfie.EncodeToPNG();
        string filename = "Screenshot" + DateTime.Now.ToString() + ".png";

        fileLocation = Path.Combine(Application.persistentDataPath, filename);
        File.WriteAllBytes(fileLocation, bytes);

        string myFolderLocation = "/mnt/sdcard/DCIM/Camera/";
        myScreenshotLocation = myFolderLocation + filename;

        System.IO.File.Move(fileLocation, myScreenshotLocation);


        /*
        AndroidJavaClass classPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject objActivity = classPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        
        AndroidJavaClass classUri = new AndroidJavaClass("android.net.Uri");
        
        AndroidJavaObject objIntent = new AndroidJavaObject("android.content.Intent", new object[2] { "android.intent.action.MEDIA_MOUNTED", classUri.CallStatic<AndroidJavaObject>("parse", "file:///mnt/sdcard/TestFolder/SubFolder/" + filename) });
        
        objActivity.Call("sendBroadcast", objIntent);
        */
    }


}
