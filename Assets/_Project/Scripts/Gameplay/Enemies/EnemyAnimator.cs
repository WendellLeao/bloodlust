using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloodlust
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public void HandleRunningAnimation(bool isMoving)
        {
            _animator.SetBool("IsWalking", isMoving); 
        }
    }
}
