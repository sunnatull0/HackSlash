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
        protected override void Start()
        {
            base.Start();
            _orcBehaviour = GetComponent<OrcBehaviour>();
        }
        
        protected override void Damage(Health targetHealth)
        {
            base.Damage(targetHealth);
            PushPlayer(targetHealth);
        }

        public override void FinishAttack() // Used in AnimationEvent.
        {
            base.FinishAttack();

            PlayerDetected = false;
        }



        private void PushPlayer(Health playerHealth)
        {
            PlayerBehaviour playerBehaviour = playerHealth.GetComponent<PlayerBehaviour>();
            playerBehaviour.BeingPushed = true;

            Rigidbody2D playerRb = playerHealth.GetComponent<Rigidbody2D>();

            float direction = playerHealth.transform.position.x - transform.position.x;
            Vector2 pushDirection = new Vector2(_horizontalForce * direction, _verticalForce);
            playerRb.velocity = Vector2.zero;

            playerRb.AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);
        }
    }
}