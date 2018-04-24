using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class Player : NetworkBehaviour
{
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;

    GameObject mainCamera;

    void Start()
    {
        Debug.Log("Start for Player");
        mainCamera = Camera.main.gameObject;

        EnablePlayer();
    }

    void DisablePlayer()
    {
        if (isLocalPlayer)
            mainCamera.SetActive(true);

        onToggleShared.Invoke(false);

        if(isLocalPlayer) {
            onToggleLocal.Invoke(false);
        } else {
            onToggleRemote.Invoke(false);
        }
    }

    void EnablePlayer()
    {
        if (isLocalPlayer) {
            Debug.Log("Is local player");
            PlayerCanvas.canvas.Initialize();
            mainCamera.SetActive(false);
            Debug.Log("main camera.set active false");
        }

        onToggleShared.Invoke(true);

        if(isLocalPlayer) {
            Debug.Log("Invoking local");
            onToggleLocal.Invoke(true);
        } else {
            onToggleRemote.Invoke(true);
        }
    }

    public void Die()
    {
        if (isLocalPlayer) {
            PlayerCanvas.canvas.WriteGameStatusText("You Died!");
        }

        DisablePlayer();
        Debug.Log("Player died!");

    }


}
