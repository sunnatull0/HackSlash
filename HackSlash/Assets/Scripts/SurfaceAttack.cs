using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SurfaceAttack : MonoBehaviour
{
    [HideInInspector] public bool AttackStarted;

    [SerializeField] private float _damage = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    [SerializeField] private bool Draw;

    private LayerMask _playerLayer;
    private const string PlayerLayerName = "Player";

    protected bool playerhit;

    public void StartAttackSystem()
    {
        _playerLayer = LayerMask.GetMask(PlayerLayerName);
        AttackStarted = true;

        StartCoroutine(IEStartAttack());
    }


    private IEnumerator IEStartAttack()
    {
        yield return new WaitForSeconds(_delayBeforeAttack);

        Attack();
    }

    private void Attack()
    {
        Debug.Log("Attack!");
        var hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _playerLayer);
        playerhit = hit != null;
        if (playerhit)
        {
            Damage(hit, _damage);
        }
    }

    public virtual void FinishAttack() // Used in AnimationEvents.
    {
        AttackStarted = false;
    }

    protected virtual void Damage(Collider2D playerCollider, float damage)
    {
        //var playerHealth = playerCollider.GetComponent<PlayerHealth>();
        //playerHealth.TakeDamage(_damage);
        Debug.Log("playerCollider: " + damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !Draw)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
}