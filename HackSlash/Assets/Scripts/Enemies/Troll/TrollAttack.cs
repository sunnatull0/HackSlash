using System;
using UnityEngine;

namespace Enemies.Troll
{
    public class TrollAttack : SurfaceAttack
    {
        private TrollBehaviour _trollBehaviour;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private float _jumpAttackDistance = 10f;

        [SerializeField] private bool _Draw;


        private void Start()
        {
            _trollBehaviour = GetComponent<TrollBehaviour>();
        }



        public void JumpAttack()
        {
            var leftSide = new Vector2(transform.position.x - (_jumpAttackDistance / 2), 0f);
            var hit = Physics2D.Raycast(leftSide, Vector2.right, _jumpAttackDistance, _playerMask);

            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!_Draw)
                return;

            var origin = new Vector2(transform.position.x - (_jumpAttackDistance / 2), 0f);
            var to = new Vector2(transform.position.x + (_jumpAttackDistance / 2), 0f);
            Gizmos.DrawLine(origin, to);
        }
    }
}