using UI;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    public float _startHealth = 3f;
    [HideInInspector] public float Healthh;


    private EnemyHealthBar _enemyHealthBar;
    private Death _death;
    
    private void Start()
    {
        _enemyHealthBar = GetComponent<EnemyHealthBar>();
        _death = GetComponent<Death>();
        Healthh = _startHealth;
    }


    public void TakeDamage(float damage)
    {
        Healthh -= damage;
        CheckHealth();

        if (transform.CompareTag("Player")) // Update UI, if it is a Player.
        {
            HealthUI.Instance.UpdateHealthUI(Healthh);
        }
        else
        {
            _enemyHealthBar.UpdateHealthBar();
        }
    }

    private void CheckHealth()
    {
        if (Healthh <= 0f)
        {
            _death.Die();
        }
    }
}