using System;
using Architecture;
using UnityEngine;

namespace UI
{
    public class TutorialPanel : UIPanel
    {
        public event Action CompleteTutorial;
        
        private Vector2 touchStartPos;
        private float swipeThreshold = 50f;
        
        private void Update()
        {
            CheckSwipe();
        }

        private void CheckSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Began)
                {
                    touchStartPos = touch.position;
                }
            
                if (touch.phase == TouchPhase.Ended)
                {
                    Vector2 swipeDelta = touch.position - touchStartPos;
                    if (swipeDelta.magnitude > swipeThreshold)
                    {
                        CompleteTutorial?.Invoke();
                    }
                }
            }
        }
    }
}
