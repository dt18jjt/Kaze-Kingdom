using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private static ScreenShot instance;
    private Camera cam;
    private bool takeScreenshotOnNextFrame;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cam = gameObject.GetComponent<Camera>();
    }
    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Screenshot.png", byteArray);
            Debug.Log("screenshot");

            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;
        }
    }
    private void TakeScreenshot(int width, int height)
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }
    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}
