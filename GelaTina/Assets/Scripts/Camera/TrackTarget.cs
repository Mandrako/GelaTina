using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class TrackTarget : MonoBehaviour
{
    public Transform target;
    public float dampTime = 0.15f;
    [Range(1, 5)]
    public int zoomLevel = 1;
    public Vector2 offset;

    private Camera _camera;
    private Vector3 _velocity = Vector3.zero;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        _camera.orthographicSize = _camera.orthographicSize / zoomLevel;

        if (target)
        {
            transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        }
    }

    void Update()
    {
        if (target)
        {
            Vector3 offsetTarget = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z);

            Vector3 point = _camera.WorldToViewportPoint(offsetTarget);
            Vector3 delta = offsetTarget - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
        }
    }
}
