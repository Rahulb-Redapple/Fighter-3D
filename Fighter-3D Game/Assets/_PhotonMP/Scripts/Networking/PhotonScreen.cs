using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonMultiplayer
{
    public class PhotonScreen : MonoBehaviour
    {
        [SerializeField] private PhotonPlayerAvatarScreen _photonPlayerAvatarScreen;

        [SerializeField] private TMP_Text _playerNameText;

        [SerializeField] private Canvas _photonScreenCanvas;


        internal void SetCanvasVisibilityDuringGameplay(bool isVisible)
        {
            _photonScreenCanvas.enabled = isVisible;
        }

        public void InitializePhoton()
        {
            PhotonConnectHelper.instance.ConnectToServer();
            PhotonConnectHelper.instance.OnConnectionSuccess += Instance_OnConnectionSuccess;
        }

        private void Instance_OnConnectionSuccess()
        {
            //GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.MULTIPLAYER, null });

            SetCanvasVisibilityDuringGameplay(true);

            _playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
            Debug.Log($"<color=green> WELCOME TO MULTIPLAYER </color>");
        }

        public void ShowAvatarSelection()
        {
            _photonPlayerAvatarScreen.Init();
        }

        public void Disconnect()
        {
            PhotonConnectHelper.instance.DisconnectFromServer();
            PhotonConnectHelper.instance.OnConnectionSuccess -= Instance_OnConnectionSuccess;

            //GameHelper.Instance.InvokeAction(GameConstants.ChangeGameState, new object[] { GameStates.MENU, null });

            SetCanvasVisibilityDuringGameplay(false);
        }
    }
}
