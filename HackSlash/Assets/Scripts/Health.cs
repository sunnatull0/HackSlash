using UnityEngine;

[RequireComponent(typeof(Death))]
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

        if (transform.CompareTag("Player")) // Update UI, if it is a Player.
        {
            HealthUI.Instance.UpdateHealthUI(Healthh);
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