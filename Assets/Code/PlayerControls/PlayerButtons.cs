using System;
using UnityEngine;
using UnityEngine.UI;


namespace Code.PlayerControls
{
    

    public class PlayerButtons : MonoBehaviour
    {
        [SerializeField] private Button _fire;
        [SerializeField] private Button _run;
        [SerializeField] private Button _walk;
        [SerializeField] private Button _kick;
        [Space]
        [SerializeField] private ArcherController _player;

        private void OnEnable()
        {
            _fire.onClick.AddListener(Fire);
            _run.onClick.AddListener(Run);
            _walk.onClick.AddListener(Walk);
            _kick.onClick.AddListener(Kick);
            _walk.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _fire.onClick.RemoveListener(Fire);
            _run.onClick.RemoveListener(Run);
            _walk.onClick.RemoveListener(Walk);
            Walk();
            ReadyToFire();
            ReadyToKick();
        }

        private void Fire()
        {
            _fire.interactable = false;
            _player.StartAiming(ReadyToFire);
        }
        
        private void Kick()
        {
           _kick.interactable = false;
           _player.StartKick(ReadyToKick);
        }
        
        private void ReadyToFire()
        {
            _fire.interactable = true;
        }
        
        private void ReadyToKick()
        {
            _kick.interactable = true;
        }
        
        private void Run()
        {
            _walk.gameObject.SetActive(true);
            _run.gameObject.SetActive(false);
            _player.OnleyWalk = false;
        }
        
        private void Walk()
        {
            _walk.gameObject.SetActive(false);
            _run.gameObject.SetActive(true);
            _player.OnleyWalk = true;
        }
    }
}