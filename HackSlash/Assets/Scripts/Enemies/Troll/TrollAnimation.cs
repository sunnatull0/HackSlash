using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Enemies.Troll
{
    [RequireComponent(typeof(TrollAttack))]
    public class TrollAnimation : CharacterAnimationBase
    {
        private TrollAttack _trollAttack;
        private readonly int JumpingParam = Animator.StringToHash("isJumping");
        [SerializeField] private List<SpriteRenderer> _sprites;
        [SerializeField] private Color _changeToColor;
        [SerializeField] private float _duration = 1.5f;
        
        protected override void Start()
        {
            _trollAttack = GetComponent<TrollAttack>();

            base.Start();
        }

        private void Update()
        {
            HandleRunAnimation(_trollAttack.AttackStarted);
            HandleAttackAnimation(_trollAttack.AttackStarted);
            HandleJumpAttackAnimation();
        }

        

        private bool _previousJumpingState;

        private void HandleJumpAttackAnimation()
        {
            if (_trollAttack.JumpAttackStarted == _previousJumpingState) return;
            
            
            animator.SetBool(JumpingParam, _trollAttack.JumpAttackStarted);
            _previousJumpingState = _trollAttack.JumpAttackStarted;
        }


        public void ChangeColor()
        {
            StartCoroutine(ChangeColorTemporarily(_changeToColor, _duration));
        }

        private IEnumerator ChangeColorTemporarily(Color targetColor, float duration)
        {
            foreach (var renderer in _sprites)
            {
                renderer.color = targetColor;
            }

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;

                // Lerp from blue back to the original color
                foreach (var renderer in _sprites)
                {
                    renderer.color = Color.Lerp(targetColor, Color.white, t);
                }

                yield return null;
            }
            foreach (var renderer in _sprites)
            {
                renderer.color = Color.white;
            }
            
        }
        
    }
}