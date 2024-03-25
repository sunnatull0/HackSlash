using Player;
using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcBehaviour))]
    public class OrcAttack : SurfaceAttack
    {
        private OrcBehaviour _orcBehaviour;

        [SerializeField] private float _pushForce = 100f;
        [Range(0f, 1f)] [SerializeField] private float _verticalForce = 0.5f;

        // OVERRIDES.
        protected override void Damage(Collider2D playerCollider, float damage)
        {
            base.Damage(playerCollider, damage);

            if (playerhit)
            {
                PushPlayer(playerCollider);
            }
        }


        public override void FinishAttack() // Used in AnimationEvent.
        {
            base.FinishAttack();

            _orcBehaviour.PlayerDetected = false;
        }


        private void Start()
        {
            _orcBehaviour = GetComponent<OrcBehaviour>();
        }

        private void PushPlayer(Component playerCollider)
        {
            var playerRb = playerCollider.GetComponent<Rigidbody2D>();
            var playerBehaviour = playerCollider.GetComponent<PlayerBehaviour>();

            var dir = (playerCollider.transform.position - transform.position).normalized;
            dir.y += _verticalForce; // Set constant Y direction.

            playerRb.AddForce(dir * _pushForce, ForceMode2D.Impulse);
            playerBehaviour.BeingPushed = true;
        }
    }
}