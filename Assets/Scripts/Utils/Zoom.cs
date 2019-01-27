using Cinemachine;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float maxZoomOut = 10f;
    public float zoomSpeed = 1f;
    
    private float initialZoom;
    private CinemachineVirtualCamera vcam;
    
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        initialZoom = vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("ZoomOut"))
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, maxZoomOut, zoomSpeed * Time.deltaTime);
        }
        else
        {
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, initialZoom, zoomSpeed * Time.deltaTime);
        }
    }
}
