using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;
using TMPro;
using Random = UnityEngine.Random;

namespace PhotonMultiplayer
{
    public class PhotonRoomHelper : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform _roomItemParent;
        [SerializeField] private PhotonRoomItem _photonRoomItem;
        [SerializeField] private GameObject _roomPanel;
        [SerializeField] private TMP_Text _roomNameText;

        private List<PhotonRoomItem> _photonRoomItemsList = new List<PhotonRoomItem>();

        public void JoinOrCreateRoom()
        {
            if (!PhotonNetwork.IsConnected)
                return;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.BroadcastPropsChangeToAll = true;
            roomOptions.MaxPlayers = 2; // TODO :: From SO
            PhotonNetwork.JoinOrCreateRoom($"Room {Random.Range(100, 1000)} ", roomOptions, TypedLobby.Default); // TODO :: Random room name generation    
        }

        public override void OnCreatedRoom()
        {
            Debug.Log($"<color=green> CREATED ROOM {PhotonNetwork.CurrentRoom.Name} SUCCESSFULLY </color>");
        }

        public override void OnJoinedRoom()
        {
            Debug.Log($"<color=green> JOINED ROOM {PhotonNetwork.CurrentRoom.Name} SUCCESSFULLY </color>");
            _roomPanel.SetActive(true);
            _roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            ExTransforms.DestroyChildren(_roomItemParent);
            _photonRoomItemsList.Clear();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log($"<color=red> Room creation failed due to {message} </color>");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            foreach(RoomInfo roomInfo in roomList)
            {
                if(roomInfo.RemovedFromList)
                {
                    int index = _photonRoomItemsList.FindIndex(x => x.roomInfo.Name == roomInfo.Name);
                    if(!Equals(index, -1))
                    {
                        _photonRoomItemsList[index].Cleanup();
                        _photonRoomItemsList.RemoveAt(index);
                    }
                }
                else
                {
                    int index = _photonRoomItemsList.FindIndex(x => x.roomInfo.Name == roomInfo.Name);
                    if (Equals(index, -1))
                    {
                        PhotonRoomItem roomItem = Instantiate(_photonRoomItem, _roomItemParent);
                        if (roomItem != null)
                        {
                            roomItem.SetRoomInfo(roomInfo);
                            _photonRoomItemsList.Add(roomItem);
                        }
                    }
                }
            }
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom(true);
            _roomPanel.SetActive(false);

            Debug.Log($"<color=yellow> LEAVED CURRENT ROOM :: {PhotonNetwork.CurrentRoom.Name} </color>");
        }
    }
}
