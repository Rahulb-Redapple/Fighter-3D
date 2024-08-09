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

        AnimatorClipInfo[] m_CurrentClipInfo;

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
            if (Input.GetKeyDown(KeyCode.F))
            {
                anim.SetInteger("MasterStateIndex", 1);
                anim.SetTrigger("MasterStateDecision");
                anim.SetTrigger("AttackStateDecision");
                _randomPunchIndex = Random.Range(101, 103);
                anim.SetInteger("PunchStateIndex", _randomPunchIndex);
                anim.SetTrigger("PunchState");
                _isPunching = true;
            }

            //if(_isPunching)
            //{
            //    m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
            //    float time = m_CurrentClipInfo[0].clip.length;

            //    if (time > 0) 
            //    {
            //        time -= Time.deltaTime; 

            //        if(time <= 0)
            //        {
            //            _isPunching = false;
            //        }
            //    }
            //}

            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetInteger("MasterStateIndex", 0);
                anim.SetTrigger("MasterStateDecision");
                anim.SetInteger("MovementStateIndex", 1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetInteger("MasterStateIndex", 0);
                anim.SetTrigger("MasterStateDecision");
                anim.SetInteger("MovementStateIndex", 2);
            }

            if(Input.GetKeyDown(KeyCode.G))
            {
                anim.SetInteger("MasterStateIndex", 1);
                anim.SetTrigger("MasterStateDecision");
                anim.SetTrigger("AttackStateDecision");
                _randomKickIndex = Random.Range(0, 2);
                anim.SetInteger("KickStateIndex", _randomKickIndex);
                anim.SetTrigger("KickState");
            }
        }

        public void ResetData()
        {
            _isPunching = false;
            IsKicking = false;
        }
    }
}
