using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherController : MonoBehaviour
{
    [SerializeField] private Animator _aimator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _pointer;
    
    [SerializeField] private bool _onleyWalk = true;
    
    [SerializeField] private float _maxSpeed = 3.5F;
    [SerializeField] private float _maxWalkSpeed = 2F;
    [SerializeField] private float _maxBackWalkSpeed = -2F;
    
    [SerializeField] private bool _isShooting = false;
    [SerializeField] private bool _isBackWalk = false;
    
    private float _speed = 0;

    

    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_speed > 0)
            {
                SpeedControll(-0.5F, 0, _maxSpeed);
            }
            else
            {
                SpeedControll(0.5F, _maxBackWalkSpeed, 0);
            }
            _aimator.SetFloat("mySpeed", _speed);
            _aimator.SetBool("isShooting", true);
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            _aimator.SetBool("isShooting", false);
        }
    }

    private void Walk(Vector3 position)
    {
        if(_isBackWalk)
        {
            SpeedControll(-0.1F, _maxBackWalkSpeed, 0);
        }
        else
        {
            var max = _maxSpeed;
            if (_onleyWalk)
            {
                max =  _maxWalkSpeed;
            }
            
            SpeedControll(0.1F, 0, max);
        }

        _navMeshAgent.speed = _speed;
        _aimator.SetFloat("mySpeed", _speed); 
        _navMeshAgent.SetDestination(position);

    }

    private void Stop()
    {
        if (_speed == 0)
            return;
        if(_isBackWalk)
        {
            SpeedControll(+0.1F, _maxBackWalkSpeed, 0);
        }
        else
        {
            SpeedControll(-0.1F, 0, _maxSpeed);
        }

        _navMeshAgent.speed = _speed;
        _aimator.SetFloat("mySpeed", _speed);
    }

    private void Kick()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (_speed > 0)
            {
                _speed = Mathf.Clamp(_speed - 0.5F, 0, 10);
            }
            else
            {
                _speed = Mathf.Clamp(_speed + 0.5F, -1.1F, 0);
            }
            _aimator.SetFloat("mySpeed", _speed);
            _aimator.SetBool("isKick", true);
        }
        if (!Input.GetKey(KeyCode.Z))
        {
            _aimator.SetBool("isKick", false);
        }
    }
    
    private void SpeedControll(float velosity, float min, float max)
    {
        _speed = Mathf.Clamp(_speed + velosity, min, max);
    }
    
    public void Move(float angle)
    {
        if(_isShooting)
            return;

        if (angle == 0)
        {
            Stop();
        }else
        {
            if (angle > 45)
            {
                angle = 45;
            }
            if (angle < -45)
            {
                angle = -45;
            }
            _pointer.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0,angle,0));
            _pointer.transform.position = transform.position;
            _pointer.transform.Translate(Vector3.forward*2);
            Walk(_pointer.transform.position);
        }
       
    }
}
