using UnityEngine;

namespace Player
{
    public class PlayerDeathAnimation : MonoBehaviour
    {

        [SerializeField] private DeathAnimation _playerDeathAnimation;
        
        private void PlayFadeOutAnimation() // used in AnimationEvent.
        {
            _playerDeathAnimation.PlayFadeOutAnimation();
        }

        private void Destroy()
        {
            Destroy(transform.parent.gameObject);
        }
        
    }
}
