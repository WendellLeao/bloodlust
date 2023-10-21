using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class CharacterAnimationController : MonoBehaviour
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Dash = Animator.StringToHash("Dash");
        private static readonly int DrainBlood = Animator.StringToHash("DrainBlood");

        private Animator _animator;

        public void Begin(Animator animator)
        {
            _animator = animator;
        }
        
        public void Stop()
        {}
        
        public void SetIsRunning(bool isRunning)
        {
            _animator.SetBool(IsRunning, isRunning);
        }
        
        public void TriggerDash()
        {
            _animator.SetTrigger(Dash);
        }

        public void TriggerDrainBlood()
        {
            _animator.SetTrigger(DrainBlood);
        }
    }
}