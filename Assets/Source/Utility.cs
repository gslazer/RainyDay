using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static void CaptureScreenshot()
    {
        string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fileName = "ScreenShot/SCREENSHOT-" + timestamp + ".png";
        ScreenCapture.CaptureScreenshot(fileName);
    }
}
