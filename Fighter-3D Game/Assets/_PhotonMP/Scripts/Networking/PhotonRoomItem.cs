using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using System;
using Photon.Pun;

namespace PhotonMultiplayer
{
    public class PhotonRoomItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _roomNameText;
        [SerializeField] private TMP_Text _playerCountText;

        private RoomInfo _roomInfo;

        internal RoomInfo roomInfo { get { return _roomInfo; } }

        internal void SetRoomInfo(RoomInfo roomInfo)
        {
            _roomInfo = roomInfo;
            _roomNameText.text = _roomInfo.Name;
            UpdatePlayerCountInRoom();
        }

        public void OnClickRoomItem()
        {
            PhotonNetwork.JoinRoom(_roomInfo.Name);
            UpdatePlayerCountInRoom();
        }

        internal void UpdatePlayerCountInRoom()
        {
            _playerCountText.text = _roomInfo.PlayerCount + " / " + _roomInfo.MaxPlayers;
        }

        internal void Cleanup()
        {
            Destroy(gameObject);
        }
    }
}
