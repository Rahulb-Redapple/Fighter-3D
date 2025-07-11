using Fighting3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private Animator _anim;

        [SerializeField] private List<HitReceiver> hitReceivers = new List<HitReceiver>();

        private HitType _hitType;
        private EssentialConfigData _essentialConfigData;

        internal void Init(EssentialConfigData essentialConfigData)
        {
            _essentialConfigData = essentialConfigData;
           // hitReceivers.ForEach(hitReceiver => hitReceiver.Init(this, _essentialConfigData));
        }


        private void Start()
        {
            _anim.SetInteger(GameConstants.IDLE_INDEX, Random.Range(0, 6));
            _anim.SetTrigger(GameConstants.IDLE_STATE);
        }

        internal void PerformHitAction(HitType hitType)
        {
            _hitType = hitType;
         
            _anim.SetInteger(GameConstants.MASTER_STATE_INDEX, 2);
            _anim.SetTrigger(GameConstants.MASTER_STATE_DECISION);
            _anim.SetInteger(GameConstants.HIT_STATE_INDEX, (int)_hitType /*Random.Range(101, 104)*/);
            _anim.SetTrigger(GameConstants.HIT_STATE);

            Debug.Log($"Got Hit of type :: {_hitType}");
        }
    }

    internal static class GameConstants
    {
        #region Animation Keys

        internal const string MASTER_STATE_INDEX = "MasterStateIndex";
        internal const string MASTER_STATE_DECISION = "MasterStateDecision";
        internal const string HIT_STATE_INDEX = "HitStateIndex";
        internal const string HIT_STATE = "HitState";
        internal const string IDLE_STATE = "IdleState";
        internal const string IDLE_INDEX = "IdleIndex";

        #endregion Animation Keys
    }
}
