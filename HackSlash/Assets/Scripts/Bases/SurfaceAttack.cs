using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class SurfaceAttack : MonoBehaviour
{
    [HideInInspector] public bool AttackStarted;

    [SerializeField] protected float _damage = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    [SerializeField] private bool Draw;

    private LayerMask _playerLayer;
    protected const string PlayerLayerName = "Player";

    protected bool playerhit;

    protected virtual void Start()
    {
        _playerLayer = LayerMask.GetMask(PlayerLayerName);
    }

    [HideInInspector] public bool PlayerDetected;
    private void Update()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        var hit = Physics2D.OverlapCircle(_attackPoint.position, _attackRadius, _playerLayer);
        PlayerDetected = hit != null;
    }

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

    protected virtual void Damage(Health targetHealth)
    {
        DamageManager.Damage(targetHealth, _damage);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer(PlayerLayerName))
            return;

        var playerHealth = other.transform.GetComponent<Health>();
        DamageManager.Damage(playerHealth, _damage);
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !Draw)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
}