using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Orc
{
    [RequireComponent(typeof(OrcAttack))]
    public class OrcBehaviour : Enemy
    {
        private OrcAttack _orcAttack;

        [Header("Ground Check!")] [SerializeField]
        private Transform _groundPoint;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private LayerMask _groundLayer;

        private BoxCollider2D _myColliderr;

        // OVERRIDES.
        protected override void Start()
        {
            _myColliderr = GetComponent<BoxCollider2D>();
            _orcAttack = GetComponent<OrcAttack>();
            SFXManager.Instance.PlaySFX(SFXType.OrcSpawn);
            base.Start();
            FirstJump();
        }


        private void SetSortingLayerToDefault()
        {
            // Get all SpriteRenderer components in this GameObject and its children
            SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();

            // Loop through each SpriteRenderer and set the sorting layer to "Default"
            foreach (SpriteRenderer spriteRenderer in renderers)
            {
                spriteRenderer.sortingLayerName = "Default"; // Set this to your desired sorting layer
            }
        }


        private List<Vector2> _spawnPositions = new List<Vector2>
        {
            new (-50f, -30f),
            new (20f, -30f),
            new (-15f, -30f),
            new (60f, -30f)
        };

        [SerializeField] private float _firstForceValue = 20f;
        [SerializeField] private float _colliderEnableTime = 1f;

        private void FirstJump()
        {
            _myColliderr.enabled = false;
            ChangePositionTo(GetRandomVector(_spawnPositions));
            float chosenForceValue = (Random.value > 0.5f) ? _firstForceValue : _firstForceValue * 1.2f;
            _rb.AddForce(Vector2.up * chosenForceValue);
            StartCoroutine(EnableColliderAfterTime(_myColliderr, 1f));
        }

        private Vector2 GetRandomVector(List<Vector2> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        private IEnumerator EnableColliderAfterTime(Collider2D collider, float time)
        {
            yield return new WaitForSeconds(time);
            collider.enabled = true;
            SetSortingLayerToDefault();
        }

        private void ChangePositionTo(Vector2 pos)
        {
            transform.position = pos;
        }


        protected override void MoveTowardsPlayer()
        {
            if (_orcAttack.AttackStarted)
                return;

            base.MoveTowardsPlayer();
        }

        protected override void FlipTowardsPlayer()
        {
            if (_orcAttack.AttackStarted)
                return;

            base.FlipTowardsPlayer();
        }


        private void Update()
        {
            if (!_orcAttack.AttackStarted && IsNearPlayer() && _orcAttack.PlayerDetected)
            {
                _orcAttack.StartAttackSystem();
            }
        }


        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayer);
        }
    }
}