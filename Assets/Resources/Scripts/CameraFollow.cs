using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public int m_CameraStartHight = 20;
    public Vector2 m_MinMaxZoom;

    private float camHeight;
    private Transform plT;

    void Start()
    {
        camHeight = m_CameraStartHight;
        plT = gameObject.transform;
    }

    void Update()
    {
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        camHeight += -mouseScroll;
        camHeight = Mathf.Clamp(camHeight, m_MinMaxZoom.x, m_MinMaxZoom.y);

        if (!Camera.main.orthographic)
        {
            Camera.main.transform.position = new Vector3(plT.position.x, camHeight, plT.position.z);
        }
        else if (Camera.main.orthographic)
        {
            Camera.main.transform.position = new Vector3(plT.position.x, 20f, plT.position.z); ;
            Camera.main.orthographicSize = camHeight;
        }
    }
}
