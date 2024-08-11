using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private BodyPart _bodyPart;
        [SerializeField] private float _hitRadius = 0.05f;

        private PlayerController _playerController;

        private PunchType _punchType = PunchType.NONE;

        internal void Init(PlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Update()
        {
            RaycastHit hitInfo;
            if(Physics.SphereCast(transform.position, _hitRadius, Vector3.forward, out hitInfo))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out HitReceiver hitReceiver))
                {
                    if(hitReceiver != null)
                    {
                        Debug.Log(hitReceiver.gameObject.name);

                        if(_playerController.IsPunching)
                        {
                            _punchType = (PunchType)_playerController.RandomPunchIndex;
                            hitReceiver.ReceiveHitType(_punchType);
                            _playerController.IsPunching = false;
                        }
                        if(_playerController.IsKicking)
                        {

                        }
                    }
                    else
                    {
                        Debug.Log("No hit receivers found");
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _hitRadius);
        }

    }
}
