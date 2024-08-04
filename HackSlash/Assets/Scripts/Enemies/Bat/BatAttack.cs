using System.Collections;
using UnityEngine;

namespace Enemies.Bat
{
    public class BatAttack : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _damageInterval = 1.0f; // Interval in seconds between each damage tick
        private const string PlayerLayerName = "Player";

        private Coroutine _damageCoroutine;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
            {
                var playerHealth = other.transform.GetComponent<Health>();
                if (playerHealth != null)
                {
                    if (_damageCoroutine == null)
                    {
                        _damageCoroutine = StartCoroutine(DamageOverTime(playerHealth));
                    }
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

        private IEnumerator DamageOverTime(Health playerHealth)
        {
            while (true)
            {
                DamageManager.Damage(playerHealth, _damage);
                yield return new WaitForSeconds(_damageInterval);
            }
        }
    }
}