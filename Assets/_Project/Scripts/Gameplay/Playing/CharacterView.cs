using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private CharacterAnimationController _characterAnimationController;
        [SerializeField]
        private bool _isFacingRight = true;
        
        private int _originalSortingOrder;
        
        public void Begin()
        {
            _originalSortingOrder = _spriteRenderer.sortingOrder;
            
            _characterAnimationController.Begin(_animator);
        }

        public void Stop()
        {
            _characterAnimationController.Stop();
        }

        public void HandleFlip(Vector2 move)
        {
            if (_isFacingRight && move.x < 0f || !_isFacingRight && move.x > 0f)
            {
                Transform myTransform = transform;
                Vector3 localScale = myTransform.localScale;

                float localScaleX = localScale.x;
                localScaleX *= -1f;
                myTransform.localScale = new Vector3(localScaleX, localScale.y, localScale.z);

                _isFacingRight = !_isFacingRight;
            }
        }

        public void HandleRunningAnimation(Vector2 move)
        {
            Vector2 absoluteMove = new Vector2(Mathf.Abs(move.x), Mathf.Abs(move.y));

            _characterAnimationController.SetIsRunning(absoluteMove.x > 0f || absoluteMove.y > 0f);
        }

        public void TriggerDash()
        {
            _spriteRenderer.sortingOrder = 0;
            
            _characterAnimationController.TriggerDash();
        }

        public void TriggerDrainBlood()
        {
            _characterAnimationController.TriggerDrainBlood();
        }

        public void ResetOrderInLayer()
        { 
            _spriteRenderer.sortingOrder = _originalSortingOrder;
        }
    }
}