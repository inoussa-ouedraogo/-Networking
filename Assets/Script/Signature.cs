using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signature : NetworkBehaviour
{

    // Assign this in the Inspector: the cube prefab (must match the one in Spawnable Prefabs).
    [Header("Assign the Cube Prefab here (NetworkIdentity)")]
    public GameObject cubePrefab;

    // We'll cache the Button component once we find it in the scene.
    private Button spawnButton;

    // 1) This runs on every client when their player object spawns.
    public override void OnStartLocalPlayer()
    {
        // Find the UI Button by name (or tag) in the scene.
        // Make sure your Button is actually active/enabled when this runs.
        spawnButton = GameObject.Find("SpawnCubeButton")?.GetComponent<Button>();

        if (spawnButton != null)
        {
            // Add a listener so that OnSpawnButtonClicked() runs when the local user clicks.
            spawnButton.onClick.AddListener(OnSpawnButtonClicked);
        }
        else
        {
            Debug.LogError("Could not find a Button named 'SpawnCubeButton' in the scene.");
        }
    }

    // 2) Called on the client when they click the button.
    //    This simply tells the server “please spawn a cube for me.”
    private void OnSpawnButtonClicked()
    {
        // Only the local player should ever call this.
        if (!isLocalPlayer) return;

        // Pick a spawn position and rotation however you like.
        // For simplicity, we'll spawn at y = 1.5 units in front of the camera.
        Vector3 spawnPos = Vector3.zero;
        Quaternion spawnRot = Quaternion.identity;

        // This sends a message to the server to run CmdSpawnCube on our behalf.
        CmdSpawnCube(spawnPos, spawnRot);
    }

    // 3) This method runs on the server. It will actually instantiate + spawn.
    [Command]
    private void CmdSpawnCube(Vector3 position, Quaternion rotation)
    {
        if (cubePrefab == null)
        {
            Debug.LogError("cubePrefab is not assigned on the PlayerSpawner!");
            return;
        }

        // Instantiate the prefab **on the server** at the requested position.
        GameObject cubeInstance = Instantiate(cubePrefab, position, rotation);

        // Tell Mirror to spawn it on all clients.
        NetworkServer.Spawn(cubeInstance);
    }
}

