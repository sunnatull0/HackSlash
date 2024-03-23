using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    [SerializeField] private bool Draw;

    [HideInInspector] public bool AttackStarted;

    
    
    public void StartAttackSystem()
    {
        AttackStarted = true;

        StartCoroutine(IEStartAttack());
    }


    private IEnumerator IEStartAttack()
    {
        yield return new WaitForSeconds(_delayBeforeAttack);

        Attack();
    }

    protected void Attack()
    {
        Debug.Log("Attack!");
        var hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _playerLayer);
        if (hit != null)
        {
            //var playerHealth = hit.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(damage);
            Debug.Log("hit");
        }
    }

    public virtual void FinishAttack()
    {
        AttackStarted = false;
    }


    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !Draw)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
}