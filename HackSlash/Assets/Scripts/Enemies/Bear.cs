using System;
using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Bear : Enemy
    {

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _attackRadius = 2f;
        [SerializeField] private float _delayBeforeAttack = 0.5f;
        [SerializeField] private bool Draw;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StartAttackSystem();
            }
        }

        private void StartAttackSystem()
        {
            StartCoroutine(IEStartAttack());
        }
        
        
        
        private IEnumerator IEStartAttack()
        {
            yield return new WaitForSeconds(_delayBeforeAttack);
            
            Attack();
        }
        
        private void Attack()
        {
            var hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _playerLayer);
            if (hit != null)
            {
                //var playerHealth = hit.GetComponent<PlayerHealth>();
                //playerHealth.TakeDamage(damage);
                Debug.Log("hit");
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null || !Draw)
                return;

            Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
        }
        
    }
}
