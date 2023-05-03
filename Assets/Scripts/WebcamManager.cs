using UnityEngine;

public class WebcamManager : MonoBehaviour
{
    public MeshRenderer _WebcamTexture;

    void Start()
    {
        WebCamTexture webcamTexture = new WebCamTexture();
        WebCamDevice[] devices = WebCamTexture.devices;
        //for (int i = 0; i < devices.Length; i++)
        //    Debug.Log(devices[i].name);

        if (devices.Length > 0)
        {
            webcamTexture.deviceName = devices[0].name;
            webcamTexture.Play();
            _WebcamTexture.material.mainTexture = webcamTexture;
        }
    }

    void Update()
    {
        
    }
}