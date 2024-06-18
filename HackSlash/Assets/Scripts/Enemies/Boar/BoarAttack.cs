using System.Collections;
using UnityEngine;

namespace Enemies.Boar
{
    [RequireComponent(typeof(BoarBehaviour))]
    public class BoarAttack : MonoBehaviour
    {
        private BoarBehaviour _boarBehaviour;

        [SerializeField] private float _damage;
        [SerializeField] private float _attackSpeedMultiplier = 0.5f;
        [SerializeField] private float _delayBeforeAttack = 0.5f;
        [SerializeField] private float _delayAfterAttack = 0.5f;
        private const string PlayerLayerName = "Player";

        public bool AttackStarted { get; private set; }
        public bool IsAttacking { get; private set; }


        private void Start()
        {
            _boarBehaviour = GetComponent<BoarBehaviour>();
        }


        public void StartAttackSystem()
        {
            if (AttackStarted) return;


            _boarBehaviour.StopEnemy();
            StartCoroutine(StartAttackingAfterDelay());
            AttackStarted = true;
        }

        private IEnumerator StartAttackingAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeAttack);
            SFXManager.Instance.PlaySFX(SFXType.BoarAttack);

            Attacking();
        }


        private void Attacking()
        {
            IsAttacking = true;
            _boarBehaviour.ChangeSpeed(_boarBehaviour.DefaultSpeed * _attackSpeedMultiplier); // Increase speed.
        }


        public void StopAttack() // Used in AnimationEvent.
        {
            IsAttacking = false;

            StartCoroutine(ResetAttackAfterDelay()); // ResetAttack.
        }

        private IEnumerator ResetAttackAfterDelay()
        {
            yield return new WaitForSeconds(_delayAfterAttack);

            // Resetting.
            _boarBehaviour.PlayerDetected = false;
            AttackStarted = false;
            _boarBehaviour.ChangeSpeed(_boarBehaviour.DefaultSpeed);
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
            {
                var playerHealth = other.transform.GetComponent<Health>();
                DamageManager.Damage(playerHealth, _damage);
            }
        }
    }
}