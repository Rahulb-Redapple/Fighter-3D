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

        private void Awake()
        {
            hitReceivers.ForEach(hitReceiver => hitReceiver.Init(this));
        }

        private void Start()
        {
            _anim.SetInteger(GameConstants.IDLE_INDEX, Random.Range(0, 6));
            _anim.SetTrigger(GameConstants.IDLE_STATE);
        }

        internal void PerformHitAction()
        {
            Debug.Log("hit anim start");
            _anim.SetInteger(GameConstants.MASTER_STATE_INDEX, 0);
            _anim.SetTrigger(GameConstants.MASTER_STATE_DECISION);
            _anim.SetInteger(GameConstants.HIT_STATE_INDEX, 102);
            _anim.SetTrigger(GameConstants.HIT_STATE);
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
