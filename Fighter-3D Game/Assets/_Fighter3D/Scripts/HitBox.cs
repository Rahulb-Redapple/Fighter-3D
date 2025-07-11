using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class HitBox : MonoBehaviour
    {
        [SerializeField] private BodyPart _bodyPart;
        [SerializeField] private float _hitRadius = 0.05f;

        private FighterController _controller;

        int _punchIndex = 0;
        int _kickIndex = 0;

        private AttackType _attackType = AttackType.NONE;

        internal void Init(FighterController playerController)
        {
            _controller = playerController;
        }

        private void Update()
        {
            RaycastHit hitInfo;
            if(Physics.SphereCast(transform.position, _hitRadius, Vector3.forward, out hitInfo))
            {
                Debug.Log("1");
                if(hitInfo.collider.gameObject.TryGetComponent(out HitReceiver hitReceiver))
                {
                    Debug.Log("2");
                    if (hitReceiver != null && hitReceiver.GetActorId() != PhotonNetwork.LocalPlayer.ActorNumber)
                    {
                        Debug.Log("3");
                        Debug.Log(hitReceiver.gameObject.name);
                        HitReceiverHandler otherhandler = hitReceiver.GetHandler();
                        _attackType = _controller.AttackType;

                        if(_controller.IsPunching)
                        {
                            _punchIndex = _controller.punchIndex;
                            otherhandler.GetPhotonView().RPC(nameof(otherhandler.ReceiveHit), RpcTarget.All, _attackType, _punchIndex);
                            Debug.Log("4");
                            ResetData();
                        }
                        if(_controller.IsKicking)
                        {
                            _kickIndex = _controller.kickIndex;
                            otherhandler.GetPhotonView().RPC(nameof(otherhandler.ReceiveHit), RpcTarget.All, _attackType, _kickIndex);

                            ResetData();
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

        internal void ResetData()
        {
            _controller.IsPunching = false;
            _controller.IsKicking = false;
            _attackType = AttackType.NONE;
        }
    }
}
