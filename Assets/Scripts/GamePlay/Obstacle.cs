using Dreamteck.Splines;
using UnityEngine;

namespace GamePlay
{
    public class Obstacle : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<SplineFollower>())
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.StartDie();
                gameObject.SetActive(false);
            }
        }
    }
}
