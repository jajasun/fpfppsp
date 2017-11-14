using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonPanel : MonoBehaviour {
  [SerializeField]
  private Text connectionState = null;

  private ClientState clientStateCache = ClientState.Disconnected;

  private void Update() {
    if (clientStateCache != PhotonNetwork.connectionStateDetailed) {
      clientStateCache = PhotonNetwork.connectionStateDetailed;
      connectionState.text = string.Format("Client State: {0}", clientStateCache.ToString());
    }
  }
}
