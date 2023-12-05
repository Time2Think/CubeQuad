using System;
using DefaultNamespace;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class PlayerController : MonoBehaviour
    {
        public event Action Die;
        public event Action Win;
        private readonly float _swipeRange = 2.8f; 

        [SerializeField]
        private float _swipeSpeed = 1f; 
        [SerializeField] 
        private float _sensitivity = 1f;
        [SerializeField] 
        private SplineFollower _splineFollower;
    
        private Animator _animator;
        private HashStringToInt _hashAnimation;
        private bool _isSwiping = false;
        private Vector2 _startTouchPos;
        private float _startObjectX;
    
        [Inject]
        private void Construct(HashStringToInt hashAnimation)
        {
            _hashAnimation = hashAnimation;
        }

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
            _splineFollower.follow = false;
        }

   
        public void InitPlayer(SplineComputer presentLevel)
        {
            _splineFollower.spline = presentLevel;
            StartIdle();
        }

        public void StartIdle()
        {
            gameObject.transform.localRotation = new Quaternion(0f, 0f, 0f,0f);
            _animator.StopPlayback();
            _animator.CrossFade(_hashAnimation.Idle,0.1f);
            _animator.Play(_hashAnimation.Idle);
            _splineFollower.follow = false;
        }
        
        public void StartRun()
        {
            _animator.StopPlayback();
            _animator.CrossFade(_hashAnimation.Run,0.1f);
            _animator.Play(_hashAnimation.Run);
            _splineFollower.follow = true;
        }
        
        public void StartDie()
        {
            Die?.Invoke();
            _animator.StopPlayback();
            _animator.CrossFade(_hashAnimation.Lose,0.1f);
            _animator.Play(_hashAnimation.Lose);
            _splineFollower.follow = false;
        }
        
        public void StartDance()
        {
            Win?.Invoke();
            gameObject.transform.localRotation = new Quaternion(0f, 180f, 0f,0f);
            _animator.StopPlayback();
            _animator.CrossFade(_hashAnimation.Win,1f);
            _animator.Play(_hashAnimation.Win);
            _splineFollower.follow = false;
        }
    
        private void Update()
        {
            HandleSwipeInput();
        }

        private void HandleSwipeInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
           
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        StartSwipe(touch.position);
                        break;

                    case TouchPhase.Moved:
                        if (_isSwiping)
                        { 
                            MoveObject(touch.position);
                        }
                        break;

                    case TouchPhase.Ended:
                        _isSwiping = false;
                        break;
                }
            }
        }

        private void StartSwipe(Vector2 touchPos)
        {
            _isSwiping = true;
            _startTouchPos = touchPos;
            _startObjectX = transform.localPosition.x;
        }

        private void MoveObject(Vector2 touchPos)
        {
            var _halfScreen = Screen.width / 2;
            float swipeDistanceX = (touchPos.x - _startTouchPos.x) / _halfScreen * _sensitivity;
            float linearSpeed = swipeDistanceX * _swipeSpeed;
            float newPositionX = _startObjectX + linearSpeed;
            transform.localPosition = new Vector3(Mathf.Clamp(newPositionX, -_swipeRange, _swipeRange), transform.localPosition.y, transform.localPosition.z);
        }
    }
}