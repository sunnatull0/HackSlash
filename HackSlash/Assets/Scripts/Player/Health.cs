using UnityEngine;


public class Health : MonoBehaviour
{
    
    [SerializeField] private float _startHealth = 3f;
    private float _health;


    private void Start()
    {
        _health = _startHealth;
    }


    public void TakeDamage(float damage)
    {
        _health -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health <= 0f)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log("Died!");
    }
    
}