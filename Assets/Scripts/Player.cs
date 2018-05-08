using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        /*
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        */

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
            Debug.Log("DIED");
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

    void QuitGame() 
    { 
    	SceneManager.LoadScene("Pregame Scene");
    }

    void RespawnAction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Respawn();
    }

    // Render a prompt, pass actions that the quit/play again button should invoke
    void RenderPlayAgainPrompt()
    {
        UnityAction respawn = new UnityAction(RespawnAction);
        UnityAction quitGame = new UnityAction(QuitGame); 
        PlayerCanvas.canvas.playAgainPrompt(respawn, quitGame);
    }


}
