using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class ObstacleMovementController : MonoBehaviour
    {
        private Obstacle _obstacle;
        private TriggerZoneSphere _triggerZoneSphere;

        private void Awake()
        {
            _obstacle = gameObject.GetComponentInChildren<Obstacle>();
            _triggerZoneSphere = gameObject.GetComponentInChildren<TriggerZoneSphere>();
            _triggerZoneSphere.OnPlayerCollision += MoveToTarget;
        }

        private void OnDestroy()
        {
            _triggerZoneSphere.OnPlayerCollision -= MoveToTarget;
        }

        private void MoveToTarget()
        {
            _obstacle.transform.DOMove(_triggerZoneSphere.transform.position, 1f);
        }
        
    }
}