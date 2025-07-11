using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

namespace PhotonMultiplayer
{
    public class PhotonPlayerHelper : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PhotonPlayerItem _photonPlayerItem;
        [SerializeField] private Transform _playerItemParent;
        [SerializeField] private PhotonRoomHelper _photonRoomHelper;

        [SerializeField] private TMP_Text _readyOrNotStatusText;

        private List<PhotonPlayerItem> _photonPlayerItemsList = new List<PhotonPlayerItem>();

        private bool _isReady = false;

        public override void OnEnable()
        {
            base.OnEnable();
            SetReadyStatus(false);
            GetCurrentRoomPlayers();
        }

        public override void OnDisable()
        {
            base.OnDisable();
            foreach(PhotonPlayerItem playerItem in _photonPlayerItemsList)
            {
                Destroy(playerItem.gameObject);
            }
            _photonPlayerItemsList.Clear();
        }

        private void SetReadyStatus(bool status)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                _readyOrNotStatusText.text = "START";
            }
            else
            {
                _isReady = status;
                _readyOrNotStatusText.text = _isReady ? "READY" : "NOT READY";
            }            
        }

        private void GetCurrentRoomPlayers()
        {
            if (!PhotonNetwork.IsConnected)
                return;
            if (Equals(PhotonNetwork.CurrentLobby, null) || Equals(PhotonNetwork.CurrentRoom.Players, null))
                return;

            foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) 
            {
                AddPlayerListing(playerInfo.Value);
            }
        }

        private void AddPlayerListing(Player player)
        {
            int index = _photonPlayerItemsList.FindIndex(x => x.PhotonPlayer == player);
            if(!Equals(index, -1))
            {
                _photonPlayerItemsList[index].SetPlayerInfo(player);
            }
            else
            {
                PhotonPlayerItem playerItem = Instantiate(_photonPlayerItem, _playerItemParent);
                if (playerItem != null)
                {
                    playerItem.SetPlayerInfo(player);
                    _photonPlayerItemsList.Add(playerItem);
                }
            }           
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            AddPlayerListing(newPlayer);    
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            int index = _photonPlayerItemsList.FindIndex(x => x.PhotonPlayer == otherPlayer);
            if (!Equals(index, -1))
            {
                _photonPlayerItemsList[index].Cleanup();
                _photonPlayerItemsList.RemoveAt(index);
            }
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            _photonRoomHelper.LeaveRoom();
        }

        public void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < _photonPlayerItemsList.Count; i++) 
                {
                    if (_photonPlayerItemsList[i].PhotonPlayer != PhotonNetwork.LocalPlayer)
                    {
                        if (!_photonPlayerItemsList[i].IsReady)
                        {
                            Debug.Log($"Player :: {_photonPlayerItemsList[i].PhotonPlayer.NickName} is not ready.");
                            return;
                        }                            
                    }
                }
                if (PhotonNetwork.CurrentRoom.PlayerCount != PhotonNetwork.CurrentRoom.MaxPlayers)
                {
                    Debug.Log("NOT ENOUGH PLAYERS TO START GAME");
                    //return;
                }                    
                //else
                {
                    Debug.Log("GAME STARTED");
                    PhotonNetwork.LoadLevel(1);
                }
            }
        }

        public void OnClickReadyStatus()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartGame();
            }
            else
            {
                SetReadyStatus(!_isReady);
                base.photonView.RPC(nameof(RPC_HandleReadyState), RpcTarget.All, PhotonNetwork.LocalPlayer, _isReady);
            }            
        }

        [PunRPC]
        private void RPC_HandleReadyState(Player player, bool state)
        {
            int index = _photonPlayerItemsList.FindIndex(x => x.PhotonPlayer == player);
            if (!Equals(index, -1))
            {
                _photonPlayerItemsList[index].SetLocalPlayerReadyStatus(state);
            }
        }
    }
}
