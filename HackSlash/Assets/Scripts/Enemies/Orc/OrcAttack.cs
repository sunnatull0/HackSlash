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
        [Range(0f, 1f)] [SerializeField] private float _horizontalForce = 0.5f;

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
            PlayerBehaviour playerBehaviour = playerCollider.GetComponent<PlayerBehaviour>();
            playerBehaviour.BeingPushed = true;
            
            Rigidbody2D playerRb = playerCollider.GetComponent<Rigidbody2D>();

            float direction = playerCollider.transform.position.x - transform.position.x;
            Vector2 pushDirection = new Vector2(_horizontalForce * direction, _verticalForce);
            playerRb.velocity = Vector2.zero;
            
            playerRb.AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);
        }
    }
}