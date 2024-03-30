using System.Collections;
using Interfaces;
using UnityEngine;

public class SurfaceAttack : MonoBehaviour, IDamageable
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
        var hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _playerLayer);
        playerhit = hit != null;
        if (playerhit)
        {
            var playerHealth = hit.GetComponent<Health>();
            Damage(playerHealth);
        }
    }

    public virtual void FinishAttack() // Used in AnimationEvents.
    {
        AttackStarted = false;
    }

    public virtual void Damage(Health targetHealth)
    {
        targetHealth.TakeDamage(_damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !Draw)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
}