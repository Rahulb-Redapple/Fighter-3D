using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighting3D
{
    public class PlayerInputActions : MonoBehaviour, IPunObservable
    {
        [SerializeField] private PhotonView _photonView;

        [Header("Character Input Values")]
        public bool moveLeft;
        public bool moveRight;
        public bool punch;
        public bool kick;

        public void OnMoveLeft(InputValue value)
        {
            if(!_photonView.IsMine)
                return;

            MoveLeftInput(value.isPressed);
        }

        public void OnMoveRight(InputValue value) 
        {
            if (!_photonView.IsMine)
                return;

            MoveRightInput(value.isPressed);
        }

        public void OnPunch(InputValue value) 
        {
            if (!_photonView.IsMine)
                return;

            PunchInput(value.isPressed);
        }

        public void OnKick(InputValue value) 
        {
            if (!_photonView.IsMine)
                return;

            KickInput(value.isPressed);
        }

        private void MoveLeftInput(bool newState)
        {
            moveLeft = newState;
            MoveLeftOverNetwork();
        }

        private void MoveRightInput(bool newState) 
        {
            moveRight = newState;
            MoveRightOverNetwork();
        }

        private void PunchInput(bool newState) 
        {
            punch = newState;
            PerformPunchOverNetwork();
        }

        private void KickInput(bool newState) 
        {
            kick = newState;
            PerformKickOverNetwork();
        }

        #region Photon functions

        private void MoveLeftOverNetwork()
        {
            object[] eventContent = new object[] { _photonView.ViewID, moveLeft };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
            {
                CachingOption = EventCaching.AddToRoomCache,
                Receivers = ReceiverGroup.Others,
            };

            PhotonNetwork.RaiseEvent(1, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }

        private void MoveRightOverNetwork()
        {
            object[] eventContent = new object[] { _photonView.ViewID, moveRight };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
            {
                CachingOption = EventCaching.AddToRoomCache,
                Receivers = ReceiverGroup.Others,
            };

            PhotonNetwork.RaiseEvent(2, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }

        private void PerformPunchOverNetwork()
        {
            object[] eventContent = new object[] { _photonView.ViewID, punch };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
            {
                CachingOption = EventCaching.AddToRoomCache,
                Receivers = ReceiverGroup.Others,
            };

            PhotonNetwork.RaiseEvent(3, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }

        private void PerformKickOverNetwork()
        {
            object[] eventContent = new object[] { _photonView.ViewID, kick };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions()
            {
                CachingOption = EventCaching.AddToRoomCache,
                Receivers = ReceiverGroup.Others,
            };

            PhotonNetwork.RaiseEvent(4, eventContent, raiseEventOptions, SendOptions.SendReliable);
        }

        private void OnEvent(EventData eventData)
        {
            switch (eventData.Code)
            {
                case 1:
                    SyncMoveLeft(eventData);
                    break;

                case 2:
                    SyncMoveRight(eventData);
                    break;

                case 3:
                    SyncPunch(eventData);
                    break;

                case 4:
                    SyncKick(eventData);
                    break;
            }
        }

        private void SyncMoveLeft(EventData eventData)
        {
            object[] data = (object[])eventData.CustomData;

            if (_photonView.ViewID == (int)data[0])
            {
                moveLeft = (bool)data[1];
            }
        }

        private void SyncMoveRight(EventData eventData)
        {
            object[] data = (object[])eventData.CustomData;

            if (_photonView.ViewID == (int)data[0])
            {
                moveRight = (bool)data[1];
            }
        }

        private void SyncPunch(EventData eventData)
        {
            object[] data = (object[])eventData.CustomData;

            if (_photonView.ViewID == (int)data[0])
            {
                punch = (bool)data[1];
            }
        }

        private void SyncKick(EventData eventData)
        {
            object[] data = (object[])eventData.CustomData;

            if (_photonView.ViewID == (int)data[0])
            {
                kick = (bool)data[1];
            }
        }

        #endregion

        private void OnEnable()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }
        private void OnDisable()
        {
            PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
    }
}
