using Interfaces;
using UnityEngine;

namespace Enemies.Bat
{
    public class BatAttack : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _damage;
        private const string PlayerLayerName = "Player";

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
            {
                var playerHealth = other.transform.GetComponent<Health>();
                Damage(playerHealth);
            }
        }

        public void Damage(Health targetHealth)
        {
            targetHealth.TakeDamage(_damage);
        }
    }
}