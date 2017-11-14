using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.PunBehaviour {
  [SerializeField]
  private GameObject playerPrefab = null;

  private void Start() {
    PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0.0f, 1.0f, 2.0f), Quaternion.identity, 0);
  }
}
