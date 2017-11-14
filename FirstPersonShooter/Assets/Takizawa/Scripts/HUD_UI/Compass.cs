using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーに持たせてHUDのレイダーの管理者に設定します。
/// ※プレイヤーの向きに影響するため、プレイヤーのY軸回転に対応するGameObjectにつけてください。
/// </summary>
/// 
public class Compass : MonoBehaviour {
    public GameObject compassImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        compassImage.transform.rotation = Quaternion.Euler(compassImage.transform.rotation.x, compassImage.transform.rotation.y, this.gameObject.transform.eulerAngles.y);
	}
}
