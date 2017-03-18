using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DeathCanvasPrefab;

    private GameObject _player;
    private Death _deathBehaviour;
    private TimeManager _timeManager;
    private FadeManager _fadeManager;
    private EndZone _endZone;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _timeManager = GetComponent<TimeManager>();
        _fadeManager = GetComponentInChildren<FadeManager>();

        if (_player)
        {
            _deathBehaviour = _player.GetComponent<Death>();
            _deathBehaviour.DestroyCallback += OnPlayerDeath;
        }

        var endZoneGO = GameObject.FindGameObjectWithTag("EndZone");
        if (endZoneGO)
        {
            _endZone = endZoneGO.GetComponent<EndZone>();
            _endZone.OnLoadBeginCallback += OnLevelEnd;
        }
    }

    void OnPlayerDeath()
    {
        _fadeManager.StartFadeOut();

        Instantiate(DeathCanvasPrefab);

        _deathBehaviour.DestroyCallback -= OnPlayerDeath;
    }

    void OnLevelEnd()
    {
        _fadeManager.StartFadeOut();

        _endZone.OnLoadBeginCallback -= OnLevelEnd;
    }
}
