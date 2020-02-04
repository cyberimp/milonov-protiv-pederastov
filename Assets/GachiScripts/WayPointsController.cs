using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace GachiScripts
{
    [Serializable]
    public class GoEvent : UnityEvent<GameObject>
    {
    }

    public class WayPointsController : MonoBehaviour
    {
        [SerializeField] public Vector3[] waypoints;
        [SerializeField] public GoEvent onFinish;
        private bool _finished = false;
        private Vector2 _oldPosition;
        private int _targetWaypoint = 0;

        private Rigidbody2D _moveBody;

        //   [SerializeField]
        private bool _isMoving = false;

        // Use this for initialization
        void Start()
        {
        }

        private void OnEnable()
        {
            _moveBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!_moveBody || _targetWaypoint >= waypoints.Length) return;
            Vector2 oldWayPos;
            if (_targetWaypoint == 0)
                oldWayPos = Vector2.zero;
            else
                oldWayPos = new Vector2(waypoints[_targetWaypoint - 1].x,
                    waypoints[_targetWaypoint - 1].y);
            var newPosition = new Vector2(waypoints[_targetWaypoint].x,
                                  waypoints[_targetWaypoint].y) - oldWayPos;
            if (_isMoving) //We are on way from one waypoint to another, shouldn't miss it
            {
                if ((_moveBody.position - _oldPosition).SqrMagnitude() <= newPosition.SqrMagnitude()) return;
                _isMoving = false;
                //moveBody.velocity = Vector2.zero;
                _targetWaypoint++;
            }
            else //Let's find another waypoint!
            {
                _oldPosition = _moveBody.position;
                newPosition.Normalize();
                var velocity = newPosition * waypoints[_targetWaypoint].z;
                _moveBody.velocity = velocity;
                if (!_finished && waypoints[_targetWaypoint].z <= float.Epsilon)
                {
                    _finished = true;
                    onFinish.Invoke(gameObject);
                }

                _isMoving = true;
            }
        }
    }
}