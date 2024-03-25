using System;
using UnityEngine;

namespace Enemies.Bear
{
    [RequireComponent(typeof(BearBehaviour))]
    public class BearAttack : SurfaceAttack
    {

        private BearBehaviour _bearBehaviour;


        private void Start()
        {
            _bearBehaviour = GetComponent<BearBehaviour>();
        }

        public override void FinishAttack() // Used in AnimationEvent.
        {
            base.FinishAttack();

            _bearBehaviour.PlayerDetected = false;
        }
    }
}
