using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Image _healthBar;
        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
        }

        private void OnDisable()
        {
            //Destroy(_canvas);
        }
        
        public void UpdateHealthBar()
        {
            _healthBar.fillAmount = _health.Healthh / _health._startHealth;
        }
    }
}
