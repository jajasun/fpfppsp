using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : Photon.PunBehaviour {
  private bool isLoggedIn = false;

  [SerializeField]
  private string gameVersion = "1.0.0";
  [SerializeField]
  private PhotonLogLevel logLevel = PhotonLogLevel.Informational;
  [SerializeField]
  private GameObject loginPanel = null;
  [SerializeField]
  private GameObject lobbyPanel = null;
  [SerializeField]
  private GameObject roomPanel = null;

  public string PlayerName {
    get {
      return PhotonNetwork.playerName;
    }
    set {
      PhotonNetwork.playerName = value;
    }
  }

  #region Unity Callbacks
  private void Awake() {
    PhotonNetwork.autoJoinLobby = false;
    PhotonNetwork.automaticallySyncScene = true;
    PhotonNetwork.logLevel = logLevel;
  }

  private void Start() {
    PhotonNetwork.ConnectUsingSettings(gameVersion);
  }
  #endregion

  #region Photon Callbacks
  public override void OnConnectedToMaster() {
    Debug.Log("<color=green>ConnectionManager.OnConnectedToMaster() was called by PUN.</color>");
    if (isLoggedIn) {
      PhotonNetwork.JoinLobby();
    }
    else {
      loginPanel.SetActive(true);
    }
  }

  public override void OnJoinedLobby() {
    Debug.Log("<color=green>ConnectionManager.OnJoinedLobby() was called by PUN.\nNow this client is in a lobby.</color>");
    lobbyPanel.SetActive(true);
    isLoggedIn = true;
  }

  public override void OnLeftLobby() {
    Debug.Log("<color=green>ConnectionManager.OnLeftLobby() was called by PUN.</color>");
    if (!isLoggedIn) {
      loginPanel.SetActive(true);
    }
  }

  public override void OnJoinedRoom() {
    Debug.Log("<color=green>ConnectionManager.OnJoinedRoom() was called by PUN.\nNow this client is in a room.</color>");
    lobbyPanel.SetActive(false);
    roomPanel.SetActive(true);

    var propertiesToSet = new ExitGames.Client.Photon.Hashtable();
    propertiesToSet.Add("readyPlayers", 1);

    PhotonNetwork.room.SetCustomProperties(propertiesToSet);
    PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
  }

  public override void OnLeftRoom() {
    Debug.Log("<color=green>ConnectionManager.OnLeftRoom() was called by PUN.</color>");
    lobbyPanel.SetActive(false);
    roomPanel.SetActive(false);

    var properties = new ExitGames.Client.Photon.Hashtable();
    properties.Add("isReady", true);
    PhotonNetwork.player.SetCustomProperties(properties);
  }

  public override void OnDisconnectedFromPhoton() {
    Debug.LogWarning("<color=green>ConnectionManager.OnDisconnectedFromPhoton() was called by PUN.</color>");
    loginPanel.SetActive(true);
    lobbyPanel.SetActive(false);
    roomPanel.SetActive(false);
    PhotonNetwork.ConnectUsingSettings(gameVersion);
  }
  #endregion

  public void Connect() {
    loginPanel.SetActive(false);
    PhotonNetwork.JoinLobby();
  }

  public void LeaveLobby() {
    Debug.Log("<color=green>ConnectionManager.LeaveLobby() was called.</color>");
    lobbyPanel.SetActive(false);
    isLoggedIn = false;
    PhotonNetwork.LeaveLobby();
  }

  public void LeaveRoom() {
    Debug.Log("<color=green>ConnectionManager.LeaveRoom() was called.</color>");
    PhotonNetwork.LeaveRoom();
  }

}
