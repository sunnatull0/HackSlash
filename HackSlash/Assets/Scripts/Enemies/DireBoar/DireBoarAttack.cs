using System.Collections;
using UnityEngine;

namespace Enemies.DireBoar
{
    [RequireComponent(typeof(DireBoarBehaviour))]
    public class DireBoarAttack : MonoBehaviour
    {
        private DireBoarBehaviour _direBoarBehaviour;

        [SerializeField] private float _attackSpeedMultiplier = 0.5f;
        [SerializeField] private float _delayBeforeAttack = 0.5f;
        [SerializeField] private float _delayAfterAttack = 0.5f;

        public bool AttackStarted { get; private set; }
        public bool IsAttacking { get; private set; }


        private void Start()
        {
            _direBoarBehaviour = GetComponent<DireBoarBehaviour>();
        }


        public void StartAttackSystem()
        {
            if (AttackStarted) return;


            _direBoarBehaviour.StopEnemy();
            StartCoroutine(StartAttackingAfterDelay());
            AttackStarted = true;
        }

        private IEnumerator StartAttackingAfterDelay()
        {
            yield return new WaitForSeconds(_delayBeforeAttack);

            Attacking();
        }


        private void Attacking()
        {
            IsAttacking = true;
            _direBoarBehaviour.ChangeSpeed(_direBoarBehaviour.DefaultSpeed * _attackSpeedMultiplier); // Increase speed.

            //StartCoroutine(StopAttackAfterDelay()); // Stop Attack.
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
            _direBoarBehaviour.PlayerDetected = false;
            AttackStarted = false;
            _direBoarBehaviour.ChangeSpeed(_direBoarBehaviour.DefaultSpeed);
        }
    }
}