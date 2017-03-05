using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Camera/TrackTarget")]
[RequireComponent(typeof(Camera))]
public class TrackTarget : MonoBehaviour
{
    public Transform target;
    public float dampTime = 0.15f;
    [Range(4, 8)]
    public float zoomLevel = 4f;
    public Vector2 offset;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        SetZoomLevel();
    }

    void Start()
    {
        if (target)
        {
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("The TrackTarget script doesn't have a target!", gameObject);
        }
    }

    void Update()
    {
        SetZoomLevel();
        if (target)
        {
            Vector3 offsetTarget = new Vector3(target.position.x + offset.x / zoomLevel, target.position.y + offset.y / zoomLevel, target.position.z);

            Vector3 point = _camera.WorldToViewportPoint(offsetTarget);
            Vector3 delta = offsetTarget - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
        }
    }

    void SetZoomLevel()
    {
        _camera.orthographicSize = (Screen.height / 2) / zoomLevel;
    }
}
