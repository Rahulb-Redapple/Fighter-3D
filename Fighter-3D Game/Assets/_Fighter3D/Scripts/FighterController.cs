using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighting3D
{
    public class FighterController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private List<HitBox> _hitBoxList = new List<HitBox>();

        [SerializeField] private GameObject _opponent;

        public EssentialConfigData _essentialConfigData;
        public HitMappingConfig _hitMappingConfig;
        public PlayerInputActions _input;

        private AttackType _attackType = AttackType.NONE;
        private HitType _hitType = HitType.NONE;

        internal AttackType AttackType { get { return _attackType; } }  

        private bool _isPunching = false;
        internal bool IsPunching
        {
            get { return _isPunching; }
            set { _isPunching = value; }
        }
        private bool _iskicking = false;
        internal bool IsKicking
        {
            get { return _iskicking; }
            set { _iskicking = value; }
        }

        public int idleIndex;
        public int punchIndex;
        public int kickIndex;


        private void Awake()
        {
            _input = GetComponent<PlayerInputActions>();
            _hitBoxList.ForEach(hitBox => { hitBox.Init(this); });
        }

        private void Start()
        {
            idleIndex = 3;
            anim.SetInteger("IdleIndex", idleIndex);
            anim.SetTrigger("IdleState");
        }

        private void Update()
        {
            Movement();
            PunchAction();
            KickAction();           
        }

        private void Movement()
        {
            if (_input.moveRight)
            {
                TriggerAction(0, PlayerAction.MOVE, 1);
                _input.moveRight = false;
            }

            if (_input.moveLeft)
            {
                TriggerAction(0, PlayerAction.MOVE, 2);
                _input.moveLeft = false;
            }
        }

        private void PunchAction()
        {
            if (_input.punch)
            {
                punchIndex = 101;
                TriggerAction(1, PlayerAction.PUNCH, punchIndex);
                _isPunching = true;
                _input.punch = false;
            }
        }

        private void KickAction()
        {
            if (_input.kick)
            {
                kickIndex = 1;
                TriggerAction(1, PlayerAction.KICK, kickIndex);
                _input.kick = false;
                _iskicking = true;
            }
        }

        private int DetermineAttack(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.PUNCH:
                    List<PunchType> punchTypes = _hitMappingConfig.HitMapsCollectionDict.Keys.ToList();
                    return (int)punchTypes[Random.Range(0, punchTypes.Count)];

                case AttackType.KICK:
                    break;
            }
            return 0;
        }

        private bool IsCharacterCloseToOther()
        {
            //Debug.Log(Vector3.Distance(transform.position, _opponent.transform.position));
            if(Vector3.Distance(transform.position, _opponent.transform.position) <= 0.843f)
            {
                //Debug.Log("close");
                return false;
            }
            else
            {
                //Debug.Log("far");
                return true;
            }
        }

        public void ResetData()
        {
            _isPunching = false;
            IsKicking = false;
        }

        private void TriggerAction(int masterStateId, PlayerAction action, int actionId)
        {
            anim.SetInteger("MasterStateIndex", masterStateId);
            anim.SetTrigger("MasterStateDecision");

            switch (action) 
            {
                case PlayerAction.MOVE:
                    anim.SetInteger("MovementStateIndex", actionId);
                    break;

                case PlayerAction.KICK:
                    anim.SetTrigger("AttackStateDecision");
                    anim.SetInteger("KickStateIndex", actionId/*, _randomKickIndex*/);
                    anim.SetTrigger("KickState");
                    break;

                case PlayerAction.PUNCH:
                    anim.SetTrigger("AttackStateDecision");
                    anim.SetInteger("PunchStateIndex", actionId /*_randomPunchIndex*/);
                    anim.SetTrigger("PunchState");
                    break;
            }
        }

        internal void TriggerTakeHitAction(HitType hitType)
        {
            _hitType = hitType;

            anim.SetInteger("MasterStateIndex", 2);
            anim.SetTrigger("MasterStateDecision");
            anim.SetInteger("HitStateIndex", (int)_hitType /*Random.Range(101, 104)*/);
            anim.SetTrigger("HitState");

            Debug.Log($"Got Hit of type :: {_hitType}");
        }
    }
}
