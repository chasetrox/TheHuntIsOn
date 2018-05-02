using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas canvas;

    [Header("Component References")]
    [SerializeField] Image reticule;
    [SerializeField] Text gameStatusText;
    [SerializeField] Text healthValue;
    [SerializeField] PlayAgainModal playAgainScreen;
    [SerializeField] UIFader damageImage;

    //Ensure there is only one PlayerCanvas
    void Awake()
    {
        if (canvas == null)
            canvas = this;
        else if (canvas != this)
            Destroy (gameObject);
    }

    //Find all of our resources
    void Reset()
    {
        reticule = GameObject.Find ("Reticle").GetComponent<Image> ();
        gameStatusText = GameObject.Find ("Player Info Text").GetComponent<Text> ();
        healthValue = GameObject.Find ("Health Text").GetComponent<Text> ();
//        logText = GameObject.Find ("LogText").GetComponent<Text> ();
        //deathAudio = GameObject.Find ("DeathAudio").GetComponent<AudioSource> ();
    }

    public void Initialize()
    {
        reticule.enabled = true;
        gameStatusText.text = "";
    }

    public void HideReticule()
    {
        reticule.enabled = false;
    }

    public void FlashDamageEffect()
    {
        damageImage.Flash ();
    }


    public void SetHealth(int amount)
    {
        healthValue.text = "Health: "+amount.ToString ();
    }

    public void WriteGameStatusText(string text)
    {
        gameStatusText.text = text;
    }

    public void playAgainPrompt(UnityAction yesAction, UnityAction quitAction)
    {
    	playAgainScreen.Choice(yesAction, quitAction);
    }
}
