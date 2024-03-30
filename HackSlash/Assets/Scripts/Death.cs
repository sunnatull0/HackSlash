using System.Linq;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Death : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Collider2D _collider;
    private Rigidbody2D _rb;

    private readonly int _aliveParam = Animator.StringToHash("isAlive");
    private MonoBehaviour[] _scripts;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _scripts = GetComponents<MonoBehaviour>();
    }


    public void Die()
    {
        DeactivateBehaviour();
        DeactivateCollisions();
        PlayDeathAnimation();
    }

    private void DeactivateCollisions()
    {
        // var characters = FindObjectsOfType<MonoBehaviour>().OfType<ICharacter>();
        var characters = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ICharacter>();
        
        foreach (var character in characters)
        {
            Physics2D.IgnoreCollision(_collider, character.GetCollider(), true);
        }
    }

    private void DeactivateBehaviour()
    {
        _rb.velocity = Vector2.zero;
        foreach (var script in _scripts)
        {
            if (script != this)
            {
                script.enabled = false;
            }
        }
    }

    private void PlayDeathAnimation()
    {
        _animator.SetBool(_aliveParam, false);
    }
}