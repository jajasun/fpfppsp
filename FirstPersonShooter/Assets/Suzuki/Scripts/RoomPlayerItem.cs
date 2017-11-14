using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayerItem : MonoBehaviour {
  private PhotonPlayer player = null;

  [SerializeField]
  private Text playerName = null;
  [SerializeField]
  private GameObject youIcon = null;
  [SerializeField]
  private GameObject masterIcon = null;
  [SerializeField]
  private GameObject kickButton = null;
  [SerializeField]
  private GameObject readyIcon = null;
  [SerializeField]
  private GameObject changeTeamButton = null;
  [SerializeField]
  private Color blueTeamColor = Color.blue;
  [SerializeField]
  private Color redTeamColor = Color.red;

  public void Refresh(PhotonPlayer player) {
    this.player = player;
    playerName.text = player.NickName;
    youIcon.SetActive(player.IsLocal);
    masterIcon.SetActive(player.IsMasterClient);
    if (PhotonNetwork.isMasterClient && !player.IsMasterClient) {
      kickButton.SetActive(true);
    }
    else {
      kickButton.SetActive(false);
    }
    object isReady = null;
    player.CustomProperties.TryGetValue("isReady", out isReady);
    readyIcon.SetActive((bool)isReady);
    changeTeamButton.SetActive(player.IsLocal);
    ChangeColor();
  }

  public void OnKickButtonClicked() {
    if (PhotonNetwork.isMasterClient) {
      PhotonNetwork.CloseConnection(player);
    }
  }

  public void OnChangeTeamButtonClicked() {
    switch (player.GetTeam()) {
      case PunTeams.Team.red:
        player.SetTeam(PunTeams.Team.blue);
        break;
      case PunTeams.Team.blue:
        player.SetTeam(PunTeams.Team.red);
        break;
      default:
        break;
    }
    ChangeColor();
  }

  private void ChangeColor() {
    switch (player.GetTeam()) {
      case PunTeams.Team.red:
        GetComponent<Image>().color = redTeamColor;
        break;
      case PunTeams.Team.blue:
        GetComponent<Image>().color = blueTeamColor;
        break;
      default:
        break;
    }
  }

}
