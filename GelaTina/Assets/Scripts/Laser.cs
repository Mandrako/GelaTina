using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : MonoBehaviour {
    public int maxDistance = 100;
    public string reflectionTag = "Reflective";
    public int maxPoints = 3;
    public LayerMask hitLayers;
    public Color debugLaserColor = Color.yellow;
    public GameObject laserBeamPrefab;
    public GameObject laserImpactPrefab;

    protected bool _isActive = true;
    protected Vector3 _direction;
    protected List<Vector3> _points;
    protected List<LaserBeam> _beams;
    protected GameObject _laserImpactObject;

    protected virtual void Start () {
        _points = new List<Vector3>();
        _beams = new List<LaserBeam>();
    }

    protected virtual void Update () {
        if (_isActive)
        {
            _direction = -transform.up;
            DrawLaser();
        }
        else
        {
            DeactivateBeams();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_isActive)
        {
            FindReflectionPoints(transform.position, _direction);
        }
    }

    public void Toggle(bool value)
    {
        _isActive = value;
    }

    protected virtual void DrawLaser()
    {
        if (_points.Count - 1 < _beams.Count)
        {
            for (int i = _points.Count - 1; i < _beams.Count; i++)
            {
                _beams[i].gameObject.SetActive(false);
            }    
        }

        for (int i = 0; i < _points.Count - 1; i++)
        {
            if(i < _beams.Count)
            {
                if (_beams[i].gameObject.activeSelf == false)
                {
                    _beams[i].gameObject.SetActive(true);
                }

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

        if(_points.Count > 1)
        {
            DrawLaserImpact(_points[_points.Count - 1]);
        }
    }

    protected virtual void DrawLaserImpact(Vector3 position)
    {
        if(_laserImpactObject == null)
        {
            _laserImpactObject = (GameObject)Instantiate(laserImpactPrefab, position, laserImpactPrefab.transform.rotation);
            _laserImpactObject.transform.SetParent(this.transform);
        }
        else
        {
            _laserImpactObject.SetActive(true);
            _laserImpactObject.transform.position = position;
        }
    }

    protected virtual void DeactivateBeams()
    {
        foreach (LaserBeam beam in _beams)
        {
            if (beam.isActiveAndEnabled)
            {
                beam.gameObject.SetActive(false);
            }
        }

        if(_laserImpactObject != null)
        {
            _laserImpactObject.SetActive(false);
        }
    }

    protected virtual void FindReflectionPoints(Vector3 startPos, Vector3 startDir)
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
        } while (count < maxPoints && hit.collider != null && hit.transform.tag == reflectionTag);
    }

    protected virtual void OnDrawGizmos()
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
