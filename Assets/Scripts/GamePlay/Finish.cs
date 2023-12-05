using Dreamteck.Splines;
using UnityEngine;

namespace GamePlay
{
    public class Finish : MonoBehaviour
    {
        public void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<SplineFollower>())
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.StartDance();
                Debug.Log("Win");
            }
        }
    }
}