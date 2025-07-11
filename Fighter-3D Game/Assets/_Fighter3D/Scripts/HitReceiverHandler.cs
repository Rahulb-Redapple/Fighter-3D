using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fighting3D
{
    public class HitReceiverHandler : MonoBehaviour
    {
        private PhotonView _photonView;
        private FighterController _controller;
        public HitMappingConfig _hitMappingConfig;

        [SerializeField] private List<HitReceiver> _hitReceiversList = new List<HitReceiver>();

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
            _controller = GetComponent<FighterController>();
            _hitReceiversList.ForEach(hitReceiver => hitReceiver.Init(this, PhotonNetwork.LocalPlayer.ActorNumber));
        }

        internal PhotonView GetPhotonView() { return _photonView; }

        [PunRPC]
        internal void ReceiveHit(AttackType attackType, int hitIndex)
        {
            Debug.Log(attackType.ToString());
            _controller.TriggerTakeHitAction(DetermineHit(attackType, hitIndex));
        }

        private HitType DetermineHit(AttackType attackType, int hitIndex)
        {
            switch (attackType)
            {
                case AttackType.PUNCH:
                    PunchType punchType = (PunchType)hitIndex;
                    if (_hitMappingConfig.HitMapsCollectionDict.ContainsKey(punchType))
                    {
                        List<HitType> hitTypes = _hitMappingConfig.HitMapsCollectionDict.GetValueOrDefault(punchType);
                        return hitTypes[/*Random.Range(0, hitTypes.Count)*/0];
                    }
                    break;

                case AttackType.KICK:
                    break;
            }

            return default;
        }

        internal void Cleanup()
        {

        }
    }
}
