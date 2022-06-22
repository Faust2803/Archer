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

    [SerializeField] private bool _isShooting = false;
    [SerializeField] private bool _isBackWalk = false;
    [SerializeField] private bool _isKick = false;

    private Action _kickFinishCallback;
    private Action _shootFinishCallback;
    
    
    private float _speed = 0;

    public bool OnleyWalk
    {
        get => _onleyWalk;
        set => _onleyWalk = value;
    }
    
    public bool IsShooting
    {
        get => _isShooting;
        set => _isShooting = value;
    }

    public void StartKick(Action kickFinishCallback)
    {
        _isKick = true;
        _kickFinishCallback = kickFinishCallback;
        StartCoroutine(Kick());
        
    }

    public void StopKick()
    {
        _isKick = false;
        _kickFinishCallback();
    }

    public void StartAiming(Action shootFinishCallback)
    {
        _isShooting = true;
        _shootFinishCallback = shootFinishCallback;
        StartCoroutine(Shoot());
    }

    public void FinishShoot()
    {
        _isShooting = false;
        _shootFinishCallback();
    }

    public void StartShooting()
    {
        // создаем стрелу
    }

    private IEnumerator Shoot()
    {
        while (_speed != 0)
        {
            if (_speed > 0)
            {
                SpeedControll(-0.1F, 0, _maxSpeed);
            }
            
            _aimator.SetFloat("mySpeed", _speed);
            _navMeshAgent.speed = _speed;
            
            yield return null;
        }
        _aimator.SetBool("isShooting", true);
    }

    private void Walk(Vector3 position)
    {
        var max = _maxSpeed;
        if (_isBackWalk)
        {
            max =  1;
        }
        else
        {
            if (_onleyWalk)
            {
                max =  _maxWalkSpeed;
            }
        }

        SpeedControll(0.1F, 0, max);
        
        _navMeshAgent.speed = _speed;
        _aimator.SetFloat("mySpeed", _speed); 
        _navMeshAgent.SetDestination(position);

    }

    private void Stop()
    {
        if (_speed > 0)
        {
            SpeedControll(-0.2F, 0, _maxSpeed);
        }
        else
        {
            _speed = 0;
        }
        _navMeshAgent.angularSpeed = 0;
        _navMeshAgent.speed = _speed;
        _aimator.SetFloat("mySpeed", _speed);
    }

    private IEnumerator Kick()
    {
        while (_speed != 0)
        {
            if (_speed > 0)
            {
                _speed = Mathf.Clamp(_speed - 0.5F, 0, 10);
            }
            _aimator.SetFloat("mySpeed", _speed);
          
            yield return null;
        }
        _aimator.SetBool("isKick", true);
    }

    private void SpeedControll(float velosity, float min, float max)
    {
        _speed = Mathf.Clamp(_speed + velosity, min, max);
    }
    
    public void Move(float angle)
    {
        if(_isShooting)
            return;
        if(_isKick)
            return;

        if (angle == 0)
        {
            Stop();
        }else
        {
            if (angle > -55 && angle < 55)
            {
                MoveForward(angle);
            }
            else
            {
                if (angle > -120 && angle < 120)
                {
                    Rotate(angle);
                }
                else
                {
                    MoveBack(angle);
                }
            }
        }
    }

    private void MoveForward(float angle)
    {
        _aimator.SetBool("isBackWalk", false);
        _isBackWalk = false;
        _navMeshAgent.angularSpeed = 45;
        Walk(CalculatePosition(angle));
    }
    
    private void MoveBack(float angle)
    {
        _aimator.SetBool("isBackWalk", true);
        _isBackWalk = true;
        _navMeshAgent.angularSpeed = 0;
        Walk(CalculatePosition(angle));
    }
    
    private void Rotate(float angle)
    {
        _aimator.SetBool("isBackWalk", false);
        _isBackWalk = false;
        _navMeshAgent.angularSpeed = 100;
        _speed = 0.1F;
        _navMeshAgent.SetDestination(CalculatePosition(angle));
    }

    private Vector3 CalculatePosition(float angle)
    {
        _pointer.transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0,angle,0));
        _pointer.transform.position = transform.position;
        _pointer.transform.Translate(Vector3.forward);
        return _pointer.transform.position;
    }
}
