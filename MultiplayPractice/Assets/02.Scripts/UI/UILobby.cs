using MP.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MP.UI
{
    public class UILobby : UIScreenBase, ILobbyCallbacks
    {
        public const int NOT_SELECTED = -1;

        public int roomListSlotIndexSelected
        {
            get => _roomListSlotIndexSelected;
            set
            {
                _roomListSlotIndexSelected = value;
                _joinRoom.interactable = value > NOT_SELECTED;
            }
        }

        private int _roomListSlotIndexSelected = NOT_SELECTED;
        private RoomListSlot _roomListSlot;
        private List<RoomListSlot> _roomListSlots = new List<RoomListSlot>(20);
        private RectTransform _roomListContent;
        private Button _joinRoom;
        private Button _createRoom;
        private List<RoomInfo> _localRoomList;

        protected override void Awake()
        {
            base.Awake();
            _roomListSlot = Resources.Load<RoomListSlot>("UI/RoomListSlot");
            _roomListContent = transform.Find("Panel/Scroll View - RoomList/Viewport/Content").GetComponent<RectTransform>();
            _joinRoom = transform.Find("Panel/Button - JoinRoom").GetComponent<Button>();
            _createRoom = transform.Find("Panel/Button - CreateRoom").GetComponent<Button>();
        }

        private void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        private void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void OnJoinedLobby()
        {
            throw new System.NotImplementedException();
        }

        public void OnLeftLobby()
        {
            throw new System.NotImplementedException();
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            _localRoomList = roomList;

            for (int i = 0; i < _roomListSlots.Count; i++)
                Destroy(_roomListSlots[i].gameObject);

            _roomListSlots.Clear();

            for (int i = 0; i < roomList.Count; i++)
            {
                RoomListSlot slot = Instantiate(_roomListSlot, _roomListContent);
                slot.roomIndex = i;
                slot.Refresh(roomList[i].Name, roomList[i].PlayerCount, roomList[i].MaxPlayers);
                slot.onSelected += (index) => roomListSlotIndexSelected = index;
                _roomListSlots.Add(slot);
            }
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            throw new System.NotImplementedException();
        }
    }
}