using System;
using UnityEngine;

namespace Enemies
{
    public class DireBoar : Enemy
    {

        private bool defaultMovement = true;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                defaultMovement = false;
                Attack();
            }
        }

        protected override void FixedUpdate()
        {
            if(!defaultMovement)
                return;
            
            base.FixedUpdate();
        }

        private void Attack()
        {
            
            Debug.Log("Attacking!");
        }
    }
}