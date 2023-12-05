using System;
using UnityEngine;

namespace GamePlay
{
    public class TriggerZoneSphere : MonoBehaviour
    {
        public event Action OnPlayerCollision;
        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerController>())
            {
                OnPlayerCollision?.Invoke();
            }
        }
    }
}
