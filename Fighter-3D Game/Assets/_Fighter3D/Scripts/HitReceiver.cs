using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class HitReceiver : MonoBehaviour
    {
        private AIController _controller;

        internal void Init(AIController aIController)
        {
            _controller = aIController;
        }

        internal void ReceiveHitType(PunchType punchType)
        {
            Debug.Log(punchType.ToString());
            _controller.PerformHitAction();
        }
    }
}
