using UnityEngine;
using System.Collections;

public class followPlayer : MonoBehaviour {

    public GameObject player;

    private void LateUpdate ()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
