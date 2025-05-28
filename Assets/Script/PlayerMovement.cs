using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private void Update()
    {
        if (isLocalPlayer)
        {
            Debug.Log("teste");
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 playerMouve = new Vector3(h * 0.25f, v * 0.25f, 0f);
            transform.position = playerMouve+transform.position;
        }
    }
}