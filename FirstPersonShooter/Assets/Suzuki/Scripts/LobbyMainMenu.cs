using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMainMenu : Photon.PunBehaviour {
  [SerializeField]
  private byte maxPlayersPerRoom = 4;
  [SerializeField]
  private Text roomName = null;
  [SerializeField]
  private Text playerName = null;

  #region Unity Callbacks
  private void OnEnable() {
    playerName.text = string.Format("Player Name: {0}", PhotonNetwork.playerName);
  }
  #endregion

  public void OnRoomCreateButtonClicked() {
    maxPlayersPerRoom = (byte)Mathf.Clamp(maxPlayersPerRoom, 1, 16);
    Debug.LogFormat("<color=green>MainMenu.OnRoomCreateButtonClicked() was called.\nCalling PhotonNetwork.CreateRoom(null, new RoomOptions() {{ maxPlayers = {0} }}, null);</color>", maxPlayersPerRoom);
    PhotonNetwork.CreateRoom(roomName.text, new RoomOptions() { MaxPlayers = maxPlayersPerRoom }, null);
  }

}
