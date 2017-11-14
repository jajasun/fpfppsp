using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : Photon.PunBehaviour {
  [SerializeField]
  private Text roomName = null;
  [SerializeField]
  private Button startButton = null;
  [SerializeField]
  private Button readyButton = null;
  [SerializeField]
  private Button cancelButton = null;
  [SerializeField]
  private Button leaveButton = null;

  #region Unity Callbacks
  private void OnEnable() {
    Debug.Log("<color=green>RoomPanel.OnEnable() was called by Unity.</color>");
    var room = PhotonNetwork.room;
    roomName.text = room.Name;

    leaveButton.interactable = true;
    cancelButton.gameObject.SetActive(false);
    if (PhotonNetwork.isMasterClient) {
      startButton.gameObject.SetActive(true);
      readyButton.gameObject.SetActive(false);
    }
    else {
      startButton.gameObject.SetActive(false);
      readyButton.gameObject.SetActive(true);
    }
  }
  #endregion

  public void OnStartButtonClicked() {
    Debug.Log("<color=green>RoomPanel.OnStartButtonClicked() was called.</color>");
    object readyPlayers = null;
    PhotonNetwork.room.CustomProperties.TryGetValue("readyPlayers", out readyPlayers);
    if ((int)readyPlayers == PhotonNetwork.room.PlayerCount) {
      Debug.Log("<color=green>Every players are ready.\nNow we load the Level.</color>");
      PhotonNetwork.LoadLevel("Play");
    }
    else {
      Debug.LogFormat("<color=green>Some players are not ready.\nReady players: {0}</color>", readyPlayers);
    }
  }

  #region Photon Callbacks
  public override void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
    Debug.Log("<color=green>RoomPanel.OnMasterClientSwitched() was called by PUN.</color>");
    if (PhotonNetwork.isMasterClient) {
      startButton.gameObject.SetActive(true);
      readyButton.gameObject.SetActive(false);
      cancelButton.gameObject.SetActive(false);
      leaveButton.interactable = true;

      object value = null;
      PhotonNetwork.room.CustomProperties.TryGetValue("readyPlayers", out value);

      var properties = new ExitGames.Client.Photon.Hashtable();
      properties.Add("readyPlayers", (int)value - 1);

      PhotonNetwork.room.SetCustomProperties(properties);

      properties = new ExitGames.Client.Photon.Hashtable();
      properties.Add("isReady", false);
      PhotonNetwork.player.SetCustomProperties(properties);
    }
  }
  #endregion

  public void OnReadyButtonClicked() {
    Debug.Log("<color=green>RoomPanel.OnReadyButtonClicked() was called.</color>");
    readyButton.gameObject.SetActive(false);
    cancelButton.gameObject.SetActive(true);
    leaveButton.interactable = false;

    object value = null;
    PhotonNetwork.room.CustomProperties.TryGetValue("readyPlayers", out value);

    var properties = new ExitGames.Client.Photon.Hashtable();
    properties.Add("readyPlayers", (int)value + 1);

    PhotonNetwork.room.SetCustomProperties(properties);

    properties = new ExitGames.Client.Photon.Hashtable();
    properties.Add("isReady", true);
    PhotonNetwork.player.SetCustomProperties(properties);
  }

  public void OnCancelButtonClicked() {
    Debug.Log("<color=green>RoomPanel.OnCancelButtonClicked() was called.</color>");
    readyButton.gameObject.SetActive(true);
    cancelButton.gameObject.SetActive(false);
    leaveButton.interactable = true;

    object value = null;
    PhotonNetwork.room.CustomProperties.TryGetValue("readyPlayers", out value);

    var properties = new ExitGames.Client.Photon.Hashtable();
    properties.Add("readyPlayers", (int)value - 1);

    PhotonNetwork.room.SetCustomProperties(properties);

    properties = new ExitGames.Client.Photon.Hashtable();
    properties.Add("isReady", false);
    PhotonNetwork.player.SetCustomProperties(properties);
  }

}
