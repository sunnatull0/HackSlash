using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Enemies.Boar
{
    [RequireComponent(typeof(BoarAttack))]
    public class BoarBehaviour : Enemy
    {
        private BoarAttack _boarAttack;

        [SerializeField] private float _detectDistance = 2f;
        private Vector2 _raycastDir;
        private RaycastHit2D _hit;

        [HideInInspector] public float DefaultSpeed;
        [HideInInspector] public bool PlayerDetected;


        // OVERRIDES.
        protected override void Start()
        {
            _boarAttack = GetComponent<BoarAttack>();
            DefaultSpeed = moveSpeed;

            base.Start();
        }

        protected override void MoveLeftToRight()
        {
            base.MoveLeftToRight();

            if (PlayerDetected && !_boarAttack.AttackStarted)
                _boarAttack.StartAttackSystem();
        }

        protected override void Flip()
        {
            base.Flip();

            if (!IsOutsideOfBorders &&
                _boarAttack.AttackStarted) // Stop enemy if it run into border while attacking.
            {
                StopEnemy();
            }
        }


        
        private void Update()
        {
            if (!PlayerDetected)
                DetectPlayer();
        }


        private void DetectPlayer()
        {
            _raycastDir = isMovingRight ? myTransform.right : -myTransform.right;
            _hit = Physics2D.Raycast(myTransform.position, _raycastDir, _detectDistance, playerLayer);

            PlayerDetected = _hit.collider != null;
        }


        public void ChangeSpeed(float value)
        {
            moveSpeed = value;
        }

        public void StopEnemy() // Used in AnimationEvent
        {
            ChangeSpeed(0f);
        }

        [SerializeField] public SpriteRenderer _bodySprite;
        [SerializeField] public Color _regularColor, _angryColor;
        [SerializeField] public float _colorChangeDuration;
        public IEnumerator ChangeSpriteColor(Color startColor, Color endColor, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                _bodySprite.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
                yield return null;
            }

            _bodySprite.color = endColor;
        }
        
    }
}