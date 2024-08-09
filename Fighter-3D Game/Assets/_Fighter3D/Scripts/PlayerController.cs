using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private List<HitBox> _hitBoxList = new List<HitBox>();

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

        private int _randomPunchIndex = 0;
        internal int RandomPunchIndex => _randomPunchIndex;
        private int _randomKickIndex = 0;
        internal int RandomKickIndex => _randomKickIndex;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            anim.SetInteger("IdleIndex", Random.Range(0, 6));
            anim.SetTrigger("IdleState");
        }

        internal void Init()
        {
            _hitBoxList.ForEach(hitBox => { hitBox.Init(this); });
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                anim.SetInteger("MasterStateIndex", 1);
                anim.SetTrigger("MasterStateDecision");
                anim.SetTrigger("AttackStateDecision");
                _randomPunchIndex = Random.Range(101, 103);
                anim.SetInteger("PunchStateIndex", _randomPunchIndex);
                anim.SetTrigger("PunchState");
                _isPunching = true;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetTrigger("WalkForward");
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetTrigger("WalkBackward");
            }

            if(Input.GetKeyUp(KeyCode.G))
            {
                anim.SetInteger("MasterStateIndex", 1);
                anim.SetTrigger("MasterStateDecision");
                anim.SetTrigger("AttackStateDecision");
                _randomKickIndex = Random.Range(0, 2);
                anim.SetInteger("KickStateIndex", _randomKickIndex);
                anim.SetTrigger("KickState");
            }
        }
    }
}
