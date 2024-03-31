using UnityEngine;

public class DeathAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    
    private readonly int _aliveParam = Animator.StringToHash("isAlive");
    private readonly int _fadeOutParam = Animator.StringToHash("isFadingOut");

    
    public void PlayDeathAnimation()
    {
        _animator.SetBool(_aliveParam, false);
    }
    
    public void PlayFadeOutAnimation() // Used in AnimationEvent.
    {
        _animator.SetBool(_fadeOutParam, true);
    }
    
}
