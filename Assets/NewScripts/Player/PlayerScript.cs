using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace PlayerScripts
{
    public enum PlayerState
    {
        idle,
        walk,
        attack,
        interact,
        stagger
    }

    public class PlayerScript : MonoBehaviour
    {
        #region AUTRES SCRIPTS
        [SerializeField] internal PlayerMovementScript movements;

        [SerializeField] internal PlayerInputScript inputs;

        [SerializeField] internal PlayerColisionScript colisions;

        [SerializeField] internal PlayerInteraction interactions;
        #endregion

        #region VARIABLES
        internal Vector2 _deplacement;
        internal Rigidbody2D _rb;
        internal float _speed = 4;
        internal float _sprintSpeed = 6;
        public PlayerState _actualState;
        internal Animator _anim;
        internal bool _action;
        internal bool _isSprinting = false;
        [SerializeField]
        internal float _minInteractTime = 1f;
        internal bool _nextIneractTime;
        internal bool _waitForInput = false;

        [SerializeField]
        internal FloatValue _health;

        [SerializeField]
        internal EventSystem _events;

        [SerializeField]
        internal Inventory _inventory;

        [SerializeField]
        internal UIScript _ui;

        [SerializeField]
        internal GameObject _hitPoint;
        public float _hitPointDistance = 1f;
        public float _attackRadius = 0f;

        public LayerMask _EnemyLayerMask;

        public LayerMask _BreakableLayerMask;
        #endregion

        #region PROPRIETES
        public Vector2 Deplacement
        {
            set {_deplacement = value; }
            get { return _deplacement;  }
        }
        #endregion

        private void Awake()
        {
            if(movements == null) movements = GetComponent<PlayerMovementScript>();
            if (inputs == null) inputs = GetComponent<PlayerInputScript>();
            if (colisions == null) colisions = GetComponent<PlayerColisionScript>();
            if (interactions == null) interactions = GetComponent<PlayerInteraction>();
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();

            _events.OnItemReceive += _events_OnItemReceive;
        }


        private void Start()
        {
            _nextIneractTime = true;
            _actualState = PlayerState.idle;
        }

        private void Update()
        {

            inputs.GetInput();

            if (_actualState == PlayerState.interact)
            {
                if (!_waitForInput && _action)
                        interactions.ClearItem();
                return;
            }

            if (_action)
                interactions.Attack();
                           
            if (Deplacement != Vector2.zero)
                movements.MovePlayer();
            else
                movements.IdlePlayer();
                     
            // to remove
        }

        internal void SetState(PlayerState newState)
        {
            if (_actualState != newState)
            {
                _actualState = newState;
            }
        }

        internal bool ApplyDamage(float amount)
        {
            _health.runTimeValue -= amount;
            _events.TriggerHealthUpdate();
            return true;
        }

        #region RECEPTION EVENTS
        private void _events_OnItemReceive(object sender, EventSystem.ItemEventArgs e)
        {
            interactions.GiveItem(e.receivedItem);
        }

        #endregion

        public void Knock(float knockTime, float damage)
        {
            if (ApplyDamage(damage))
            {
                StartCoroutine(KnockCo(knockTime));
            }
        }

        private IEnumerator KnockCo(float knockTime)
        {
            if (_rb != null)
            {
                yield return new WaitForSeconds(knockTime);
                
                _rb.velocity = Vector2.zero;
                SetState(PlayerState.idle);
            }

        }
    }
}