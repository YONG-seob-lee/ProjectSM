using Managers;
using Systems;
using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SM_UnitBase : MonoBehaviour
    {
        private ESM_UnitType _unitType;
        
        [Header("Components")]
        protected Rigidbody2D _rigidbody;
        protected SpriteRenderer _spriteRenderer;

        [Header("Unit Settings")]
        public float moveSpeed = 5f;
        protected Vector2 _movementDiraction = Vector2.zero;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            if (!_rigidbody)
            {
                SM_Log.ERROR("[SM_UnitBase] Rigidbody2D 누락.");
            }

            if (!_spriteRenderer)
            {
                SM_Log.ERROR("[SM_UnitBase] SpriteRenderer 누락.");
            }
        }

        public virtual void Initialize(ESM_UnitType unitType)
        {
            _unitType = unitType;
        }
        protected virtual void Update()
        {
            HandleMovement();
            UpdateSpriteDirection();
        }

        public ESM_UnitType GetUnitType() => _unitType;
        protected virtual void HandleMovement()
        {
            _rigidbody.linearVelocity = _movementDiraction * moveSpeed;
        }

        protected virtual void UpdateSpriteDirection()
        {
            if (_movementDiraction.x != 0)
            {
                _spriteRenderer.flipX = _movementDiraction.x < 0;
            }
        }

        public virtual void SetMovement(Vector2 direction)
        {
            _movementDiraction = direction.normalized;
        }
    }
}