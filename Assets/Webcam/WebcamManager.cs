
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class WebcamManager : MonoBehaviour
{
    public GameObject WebcamSelectorPrefab;
    public Transform SelectionListTransform;

    public List<WebcamSelector> WebcamSelectors = new();


    public Transform WebcamDisplayTransform;
    [FormerlySerializedAs("WebcamDisplay")] public GameObject WebcamDisplayPrefab;
    public List<WebcamDisplay> WebcamDisplays = new();


    private void Start()
    {
        WebcamDisplayTransform.DestroyChildren();
        SelectionListTransform.DestroyChildren();
        
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                CreateWebcamSelector(devices[i]);
            }
        }
        else
        {
            Debug.Log("No webcam found.");
        }
    }

    private void CreateWebcamSelector(WebCamDevice device)
    {
        var webcamSelector = Instantiate(WebcamSelectorPrefab, SelectionListTransform).GetComponent<WebcamSelector>();
        
        webcamSelector.SetDevice(device);
        
        WebcamSelectors.Add(webcamSelector);
        
        webcamSelector.OnSelected += () => SelectWebcam(webcamSelector);
    }
    
    public void SelectWebcam(WebcamSelector webcamSelector)
    {
        if (WebcamDisplays.FirstOrDefault(x => x.DeviceName == webcamSelector.DeviceName) is { } existingDisplay)
        {
            WebcamDisplays.Remove(existingDisplay);
            Destroy(existingDisplay.gameObject);
            return;
        }
        
        var webcamDisplay = Instantiate(WebcamDisplayPrefab, WebcamDisplayTransform).GetComponent<WebcamDisplay>();
        webcamDisplay.InitializeWebcam(webcamSelector.Device);
        
        WebcamDisplays.Add(webcamDisplay);
    }
}
