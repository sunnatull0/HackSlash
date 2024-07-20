using Enemies.Troll;
using UI;
using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour
{
    [SerializeField] private SFXType HitSoundType;

    public float _startHealth = 3f;
    [HideInInspector] public float Healthh;


    private EnemyHealthBar _enemyHealthBar;
    private Death _death;

    private TrollAttack _trollAttack;
    private void Start()
    {
        _trollAttack = GetComponent<TrollAttack>();
        _enemyHealthBar = GetComponent<EnemyHealthBar>();
        _death = GetComponent<Death>();
        Healthh = _startHealth;
    }


    public void TakeDamage(float damage)
    {
        if (_trollAttack != null && !_trollAttack.isWaiting)
        {
            damage = 0f;
            HitSoundType = SFXType.TrollHurtNot; // Play another sound, if Troll is not weak.
        }
        
        Healthh -= damage;
        CheckHealth();
        SFXManager.Instance.PlaySFX(HitSoundType);

        if (transform.CompareTag("Player")) // Update, if it is a Player.
        {
            HealthUI.Instance.UpdateHealthUI(Healthh);
            Vibration.VibrateMedium();

            // CameraShake.
            var cameraShakeIntensity = 10f;
            var time = 0.2f;
            CameraShake.Instance.Shake(cameraShakeIntensity, time);
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