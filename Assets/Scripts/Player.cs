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

        if (Input.GetButtonUp("walk") || Input.GetButtonDown("walk"))
            PlayerCanvas.canvas.ToggleFootsteps();

        anim.animator.SetFloat ("Speed", Input.GetAxis ("Vertical"));
        anim.animator.SetFloat ("Strafe", Input.GetAxis ("Horizontal"));
    }

    void DisablePlayer()
    {
        if (isLocalPlayer) {
            PlayerCanvas.canvas.HideGameUI();
            mainCamera.SetActive(true);
        }

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

            anim.SetTrigger ("Restart");
        }

        EnablePlayer ();
    }


    public void Die()
    {
        if (isLocalPlayer) {
            PlayerCanvas.canvas.WriteGameStatusText("You Died!");
            anim.SetTrigger ("Died");
            // Prompt player after 3 seconds
            Invoke("RenderPlayAgainPrompt", 3f);
        }

        DisablePlayer();
        Debug.Log("Player died!");
    }

    public void Won()
    {
        if (isLocalPlayer) {
            PlayerCanvas.canvas.WriteGameStatusText("You Won!");
            // Prompt player after 3 seconds
            Invoke("RenderPlayAgainPrompt", 3f);
        }
        // Winning player gets to continue playing for 3 seconds before reset
        Invoke("DisablePlayer", 3f);
        Debug.Log("Player Won!");
    }

    void DoNothing() { }

    // Render a prompt, pass actions that the quit/play again button should invoke
    void RenderPlayAgainPrompt()
    {
        UnityAction respawn = new UnityAction(Respawn);
        UnityAction doNothing = new UnityAction(DoNothing); 
        PlayerCanvas.canvas.playAgainPrompt(respawn, doNothing);
    }


}
