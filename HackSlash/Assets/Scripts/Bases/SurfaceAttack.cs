using System.Collections;
using UnityEngine;

public class SurfaceAttack : MonoBehaviour
{
    [HideInInspector] public bool AttackStarted;

    [HideInInspector] public bool PlayerDetected;
    [SerializeField] protected float _damage = 1f;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius = 2f;
    [SerializeField] private float _delayBeforeAttack = 0.5f;
    [SerializeField] private float _damageInterval = 1.0f; // Interval in seconds between each damage tick
    [SerializeField] private bool Draw;

    private LayerMask _playerLayer;
    protected const string PlayerLayerName = "Player";

    protected bool playerhit;
    private Death _death;

    private Coroutine _damageCoroutine;

    protected virtual void Start()
    {
        _death = GetComponent<Death>();
        _playerLayer = LayerMask.GetMask(PlayerLayerName);
    }

    private bool IsAlive() => _death.enabled;

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
        if (_attackSoundType == SFXType.TrollAttack || _attackSoundType == SFXType.TrollAttackBoss)
        {
            SFXManager.Instance.PlaySFX(_attackSoundType);
        }

        yield return new WaitForSeconds(_delayBeforeAttack);

        Attack();
    }

    [SerializeField] private SFXType _attackSoundType;

    private void Attack()
    {
        if (!IsAlive())
            return;

        if (_attackSoundType != SFXType.TrollAttack && _attackSoundType != SFXType.TrollAttackBoss)
        {
            SFXManager.Instance.PlaySFX(_attackSoundType);
        }

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

    private IEnumerator DamageOverTime(Health playerHealth)
    {
        while (true)
        {
            Damage(playerHealth);
            yield return new WaitForSeconds(_damageInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
        {
            var playerHealth = other.transform.GetComponent<Health>();
            if (_damageCoroutine == null)
            {
                _damageCoroutine = StartCoroutine(DamageOverTime(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
        {
            if (_damageCoroutine != null)
            {
                StopCoroutine(_damageCoroutine);
                _damageCoroutine = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null || !Draw)
            return;

        Gizmos.DrawSphere(_attackPoint.position, _attackRadius);
    }
}
