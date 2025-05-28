using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Mirror;
public class CameraMouve : MonoBehaviour
{
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ne s'exécute que sur le client local
       

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v) * speed * Time.deltaTime;

        if (dir != Vector3.zero)
        {

            // Met à jour la position côté client
            transform.position=dir;
            // Informe le serveur pour synchroniser aux autres clients
         
        }
    }
}


