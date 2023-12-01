using DefaultNamespace;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float _swipeRange = 2.8f; 

    [SerializeField] private float _swipeSpeed = 1f; 
    [SerializeField] private float _sensitivity = 1f; 
    
    private Animator _animator;
    private HashStringToInt _hashAnimation = new HashStringToInt();
    
    private bool _isSwiping = false;
    private Vector2 _startTouchPos;
    private float _startObjectX;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _animator.CrossFade(_hashAnimation.Run,0.1f);
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