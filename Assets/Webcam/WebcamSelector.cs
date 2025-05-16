using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WebcamSelector : MonoBehaviour
{
    public WebCamDevice Device;
    public string DeviceName;
    
    public Button Button;
    
    public Action OnSelected;

    private void Start()
    {
        Button.onClick.AddListener(() => OnSelected?.Invoke());
    }
    
    public void SetDevice(WebCamDevice device)
    {
        Device = device;
        DeviceName = device.name;
        Button.GetComponentInChildren<TMP_Text>().text = DeviceName;
    }
}
