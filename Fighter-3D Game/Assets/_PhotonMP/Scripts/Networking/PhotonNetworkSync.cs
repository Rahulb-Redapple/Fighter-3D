using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PhotonMultiplayer
{
    public class PhotonNetworkSync : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private Transform _playerCameraRoot;

        internal PhotonView GetPhotonView()
        {
            return _photonView;
        }

        internal void SetViewId(int id)
        {
            _photonView.ViewID = id;
        }

        internal Transform GetPlayerCameraRoot() 
        {
            return _playerCameraRoot;
        }
    }
}
