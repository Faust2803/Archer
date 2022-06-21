using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.PlayerControls
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private GameObject _touchMarker;
        [SerializeField] private ArcherController _player;


        private void Start()
        {
            _touchMarker.transform.position = transform.position;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 touchPosition = Input.mousePosition;
                var targetVector = touchPosition - transform.position;
                if (targetVector.magnitude < 100)
                {
                    _touchMarker.transform.position = touchPosition;
                    _player.Move(Vector2.SignedAngle(targetVector.normalized, Vector3.up));
                }
            }
            else
            {
                _touchMarker.transform.position = transform.position;
                _player.Move(0);
            }
        }
    }
}