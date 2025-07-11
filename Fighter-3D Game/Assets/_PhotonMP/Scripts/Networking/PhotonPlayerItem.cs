using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonMultiplayer
{
    public class PhotonPlayerItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_Text _playerNameText;
        [SerializeField] private Image _masterClientImage;
        [SerializeField] private Image _playerIconImage;
        [SerializeField] private Image _playerReadyStatusImage;

        private IconDataConfig _iconDataConfig;
        private Player _photonPlayer;
        private Sprite _playerIconSprite;

        private int _index = 0;
        private bool _isReady = false;

        internal Player PhotonPlayer => _photonPlayer;
        internal bool IsReady => _isReady;

        private void Awake()
        {
            _iconDataConfig = Resources.Load<IconDataConfig>(nameof(IconDataConfig));
        }

        internal void SetLocalPlayerReadyStatus(bool status)
        {
            _isReady = status;
            _playerReadyStatusImage.color = _isReady ? Color.green : Color.red;
        }

        internal void SetPlayerInfo(Player player)
        {
            _photonPlayer = player;
            _playerNameText.text = _photonPlayer.NickName;

            SetPlayerCustomProperties(_photonPlayer);
            CheckIfPlayerIsMaster(_photonPlayer);
        }

        private void SetPlayerCustomProperties(Player player)
        {
            _index = player.CustomProperties.ContainsKey(GameConstants.IconKey) ? (int)player.CustomProperties[GameConstants.IconKey] : 0;
            _playerIconSprite = _iconDataConfig.iconSpritesList[_index];
            _playerIconImage.sprite = _playerIconSprite;
        }

        private void CheckIfPlayerIsMaster(Player player)
        {
            _masterClientImage.enabled = player.IsMasterClient ? true : false;
            _playerReadyStatusImage.enabled = player.IsMasterClient ? false : true;
        }

        internal void Cleanup()
        {
            Destroy(gameObject);
        }
    }
}
