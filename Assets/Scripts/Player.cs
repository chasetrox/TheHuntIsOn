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
    [SerializeField] public bool isHunter;

    GameObject mainCamera;
    NetworkAnimator anim;

    void Start()
    {
    	Debug.Log("Starting player");
        anim = GetComponent<NetworkAnimator>();
        mainCamera = Camera.main.gameObject;

        EnablePlayer();
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        anim.animator.SetFloat ("Speed", Input.GetAxis ("Vertical"));
        anim.animator.SetFloat ("Strafe", Input.GetAxis ("Horizontal"));
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
            PlayerCanvas.canvas.Initialize(isHunter);
            mainCamera.SetActive(false);
        }

        onToggleShared.Invoke(true);

        if(isLocalPlayer) {
            Debug.Log("Invoking local");
            onToggleLocal.Invoke(true);
        } else {
            onToggleRemote.Invoke(true);
        }
    }

    void Respawn()
    {
        if (isLocalPlayer) 
        {
            Transform spawn = NetworkManager.singleton.GetStartPosition ();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }

        EnablePlayer ();
    }


    void DoNothing() { }

    public void Die()
    {
        if (isLocalPlayer) {
        	UnityAction respawn = new UnityAction(Respawn);
        	UnityAction doNothing = new UnityAction(DoNothing);
            PlayerCanvas.canvas.WriteGameStatusText("You Died!");
            PlayerCanvas.canvas.playAgainPrompt(respawn, doNothing);
        }

        DisablePlayer();
        Debug.Log("Player died!");
    }

    public void Won()
    {
        if (isLocalPlayer) {
            UnityAction respawn = new UnityAction(Respawn);
        	UnityAction doNothing = new UnityAction(DoNothing);
            PlayerCanvas.canvas.WriteGameStatusText("You Won!");
            PlayerCanvas.canvas.playAgainPrompt(respawn, doNothing);
        }

        DisablePlayer();
        Debug.Log("Player Won!");
    }


}
