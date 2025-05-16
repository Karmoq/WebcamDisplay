using UnityEngine;
using UnityEngine.UI;

public class WebcamDisplay : MonoBehaviour
{
    public string DeviceName;
    
    public RectTransform WebcamDisplayTransform;
    public RawImage WebcamImage;

    private WebCamTexture webcamTexture;

    private int currentRotation = 0;
    public RectTransform MainTransform;
    
    public void InitializeWebcam(WebCamDevice webcamDevice)
    {
        DeviceName = webcamDevice.name;
        
        webcamTexture = new WebCamTexture(DeviceName);
        
        webcamTexture.requestedWidth = 1920;
        webcamTexture.requestedHeight = 1080;
        
        WebcamImage.texture = webcamTexture;
        
        webcamTexture.Play();

        MainTransform.anchorMin = Vector2.zero;
        MainTransform.anchorMax = Vector2.one;
        MainTransform.anchoredPosition = Vector3.zero;

        UpdateSize();
    }

    private void Update()
    {
        if (webcamTexture)
            UpdateSize();
    }

    public void OnDestroy()
    {
        if (webcamTexture)
        {
            webcamTexture.Stop();
            Destroy(webcamTexture);
        }
    }

    [ContextMenu("Rotate")]
    public void Rotate()
    {
        currentRotation = (currentRotation + 1) % 4;
        
        WebcamDisplayTransform.localEulerAngles = new Vector3(0, 0, -currentRotation * 90);
        WebcamImage.SetNativeSize();
        
        UpdateSize();
    }

    private void UpdateSize()
    {
        var mainRect = MainTransform.rect;
        var mainRectRatio = mainRect.width / mainRect.height;
        
        bool isRotated = currentRotation % 2 == 1;
        Vector2 webcamRect = !isRotated ? new Vector2(webcamTexture.width, webcamTexture.height) : new Vector2(webcamTexture.height, webcamTexture.width);
        float webcamRectRatio = webcamRect.x / webcamRect.y;

        WebcamDisplayTransform.anchorMin = Vector2.zero;
        WebcamDisplayTransform.anchorMax = Vector2.one;

        if (webcamRectRatio > mainRectRatio)
        {
            float scale = mainRect.width / webcamRect.x;

            if (!isRotated)
            {
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MainTransform.rect.width);
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, webcamRect.y * scale);
            }
            else
            {
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, MainTransform.rect.width);
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, webcamRect.y * scale);
            }
            
        }
        else
        {
            float scale = mainRect.height / webcamRect.y;
            
            if (!isRotated)
            {
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, webcamRect.x * scale);
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, MainTransform.rect.height);
            }
            else
            {
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, webcamRect.x * scale);
                WebcamDisplayTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, MainTransform.rect.height);
            }
        }
    }
}
