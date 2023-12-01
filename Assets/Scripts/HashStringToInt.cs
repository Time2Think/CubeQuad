using UnityEngine;

namespace DefaultNamespace
{
    public  class HashStringToInt
    {
        private int _idleAnim = Animator.StringToHash("Idle");
        private int _runAnim = Animator.StringToHash("Run");
        private int _winAnim = Animator.StringToHash("Win");
        private int _loseAnim = Animator.StringToHash("Lose");

        public int Idle => _idleAnim;
        public int Run => _runAnim;

        public int Win => _winAnim;

        public int Lose => _loseAnim;
    }
}