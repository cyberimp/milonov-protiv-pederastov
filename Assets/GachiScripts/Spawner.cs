using System.Collections;
using System.Collections.Generic;
using GachiScripts;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pedic;

    private WayPointsController _wayPoints;
    private AudioSource _player;
    private float _cd;
    
    // Start is called before the first frame update
    private void Start()
    {
        _wayPoints = GetComponent<WayPointsController>();
        _player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        _cd += Time.deltaTime;
        if (_cd < 1.0f) return;
        _cd = 0;
        _player.Play();
        var newPedic = Instantiate(pedic, transform, false);
        newPedic.transform.position += new Vector3(Random.Range(-.25f, .25f), Random.Range(-.25f, .25f));
        var wayPoints = newPedic.GetComponent<WayPointsController>();
        wayPoints.waypoints = _wayPoints.waypoints;
        wayPoints.onFinish = _wayPoints.onFinish;
    }
}
