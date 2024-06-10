using System;
using System.Linq;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DeathAnimation))]
public class Death : MonoBehaviour
{

    public static event Action OnPlayerDeath;
    private DeathAnimation _deathAnimation;
    private Collider2D _collider;
    private Rigidbody2D _rb;

    private MonoBehaviour[] _scripts;

    private void Start()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _scripts = GetComponents<MonoBehaviour>();
    }


    public void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            SFXManager.Instance.PlaySFX(SFXType.PlayerDeath);
            OnPlayerDeath?.Invoke();
        }
        ResetLayer();
        DeactivateBehaviour();
        DeactivateCollisions();
        _deathAnimation.PlayDeathAnimation();
    }

    private void DeactivateCollisions()
    {
        var characters = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ICharacter>();

        foreach (var character in characters)
        {
            if (character.GetCollider() == _collider)
                continue;

            Physics2D.IgnoreCollision(_collider, character.GetCollider(), true);
        }
    }

    private void DeactivateBehaviour()
    {
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 8f;
        _rb.drag = 3f;
        foreach (var script in _scripts)
        {
            // if (script != this)
            // {
                script.enabled = false;
            // }
        }
    }

    private void ResetLayer()
    {
        gameObject.layer = 0;
    }
    
    private void Destroy() // Used in AnimationEvent.
    {
        Destroy(gameObject);
    }
}