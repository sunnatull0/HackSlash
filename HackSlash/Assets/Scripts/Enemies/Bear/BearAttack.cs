using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearBehaviour))]
    public class BearAttack : SurfaceAttack
    {

        private BearBehaviour _bearBehaviour;

        protected override void Start()
        {
            base.Start();
            _bearBehaviour = GetComponent<BearBehaviour>();
        }

        public override void FinishAttack() // Used in AnimationEvent.
        {
            base.FinishAttack();

            PlayerDetected = false;
        }
    }
}
