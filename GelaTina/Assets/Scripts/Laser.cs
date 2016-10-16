using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour {
    public int maxDistance = 100;
    public string reflectionTag = "Reflective";
    public int maxPoints = 3;
    public LayerMask hitLayers;
    public Color debugLaserColor = Color.yellow;
    public GameObject laserBeamPrefab;

    private Vector3 _direction;
    private LineRenderer _lineRenderer;
    private List<Vector3> _points;
    private List<LaserBeam> _beams;

    void Start () {
        _points = new List<Vector3>();
        _beams = new List<LaserBeam>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update () {
        DrawLaser();
    }

    void FixedUpdate()
    {
        _direction = -transform.up;

        FindReflectionPoints(transform.position, _direction);
    }

    void DrawLaser()
    {
        for (int i = 0; i < _points.Count - 1; i++)
        {
            if(i < _beams.Count)
            {
                _beams[i].startPoint = _points[i];
                _beams[i].endPoint = _points[i + 1];
            }
            else
            {
                GameObject beamObj = (GameObject)Instantiate(laserBeamPrefab, _points[i], Quaternion.identity, this.transform);
                LaserBeam laserBeam = beamObj.GetComponent<LaserBeam>();

                laserBeam.startPoint = _points[i];
                laserBeam.endPoint = _points[i + 1];

                _beams.Add(laserBeam);
            }
        }
    }

    void FindReflectionPoints(Vector3 startPos, Vector3 startDir)
    {
        Vector3 pos = startPos;
        Vector3 dir = startDir;
        int count = 1;

        _points.Clear();

        Vector3 point = startPos;
        point.z = -0.01f;
        _points.Add(point);

        RaycastHit2D hit;

        do
        {
            hit = Physics2D.Raycast(pos, dir, maxDistance, hitLayers);

            if(hit.collider != null)
            {
                point = hit.point;
                point.z = -0.01f;
                _points.Add(point);

                pos = hit.point + hit.normal * 0.01f;
                dir = Vector3.Reflect(dir, hit.normal);

                count++;
            }

        } while (count < maxPoints && hit.collider != null);
    }

    void OnDrawGizmos()
    {
        if(_points != null)
        {
            for (int i = 0; i < _points.Count - 1; i++)
            {
                Gizmos.color = debugLaserColor;

                Gizmos.DrawLine(_points[i], _points[i + 1]);
            }
        }
    }
}
