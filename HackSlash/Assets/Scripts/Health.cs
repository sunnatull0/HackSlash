using UnityEngine;
using UnityEngine.Serialization;


public class Health : MonoBehaviour
{
    [SerializeField] private float _startHealth = 3f;
    [HideInInspector] public float Healthh;

    private Death _death;
    
    private void Start()
    {
        _death = GetComponent<Death>();
        Healthh = _startHealth;
    }


    public void TakeDamage(float damage)
    {
        Healthh -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (Healthh <= 0f)
        {
            _death.Die();
        }
    }

    private void Die()
    {
        Debug.Log("Died!");
        Collider2D targetCollider = GetComponent<Collider2D>();
        var targetPhysics = GetComponent<Rigidbody2D>();
        targetCollider.enabled = false;
        targetPhysics.gravityScale = 0f;
    }
}