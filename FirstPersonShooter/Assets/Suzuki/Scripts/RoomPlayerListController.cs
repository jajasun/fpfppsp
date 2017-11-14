using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomPlayerListController : Photon.PunBehaviour {
  [SerializeField]
  private GameObject roomPlayerItemPrefab = null;
  [SerializeField]
  private GameObject roomPlayerList = null;

  private Dictionary<int, RoomPlayerItem> players = new Dictionary<int, RoomPlayerItem>();

  private void RefreshPlayerList() {
    List<int> activePlayersId = new List<int>();

    foreach (var player in PhotonNetwork.playerList) {
      if (!players.ContainsKey(player.ID)) {
        var newItem = Instantiate(roomPlayerItemPrefab);
        newItem.transform.SetParent(roomPlayerList.transform);

        var roomPlayerItem = newItem.GetComponent<RoomPlayerItem>();
        players.Add(player.ID, roomPlayerItem);

        var properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("isReady", false);
        player.SetCustomProperties(properties);
      }
      players[player.ID].Refresh(player);
      activePlayersId.Add(player.ID);
    }

    foreach (var player in players.Reverse()) {
      if (!activePlayersId.Contains(player.Key)) {
        players.Remove(player.Key);
        Destroy(player.Value.gameObject);
      }
    }
  }

  private void CleanUpList() {
    players = new Dictionary<int, RoomPlayerItem>();
    foreach (Transform child in roomPlayerList.transform) {
      Destroy(child.gameObject);
    }
  }

  #region Photon Callbacks
  public override void OnJoinedRoom() {
    Debug.Log("<color=green>RoomPlayerListController.OnJoinedRoom() was called by PUN.</color>");
    CleanUpList();
    RefreshPlayerList();
  }

  public override void OnLeftRoom() {
    Debug.Log("<color=green>RoomPlayerListController.OnLeftRoom() was called by PUN.</color>");
    CleanUpList();
  }

  public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
    Debug.LogFormat("<color=green>RoomPlayerListController.OnPhotonPlayerConnected() was called by PUN.\nNew player name is: {0}</color>", newPlayer.NickName);
    RefreshPlayerList();
  }

  public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) {
    Debug.LogFormat("<color=green>RoomPlayerListController.OnPhotonPlayerDisconnected() was called by PUN.\nDisconnected player name is: {0}</color>", otherPlayer.NickName);
    RefreshPlayerList();
  }

  public override void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
    Debug.LogFormat("<color=green>RoomPlayerListController.OnMasterClientSwitched() was called by PUN.\nNew master client is: {0}</color>", newMasterClient.NickName);
    RefreshPlayerList();
  }

  public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {
    Debug.LogFormat("<color=green>RoomPlayerListController.OnPhotonPlayerPropertiesChanged() was called by PUN.</color>");
    RefreshPlayerList();
  }

  #endregion

}
