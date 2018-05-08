using UnityEngine;
using UnityEngine.UI;

public class SwapIconOnButtonPress : MonoBehaviour
{

    public Sprite defaultSprite, swapSprite;
    public string buttonName = "walk";

    private Image img;

	void Start ()
    {
        img = GetComponent<Image>();

        if (img == null)
            print("SwapIconOnButtonPress has no access to UI image?");
    }
	
	void Update ()
    {
        if (Input.GetButton(buttonName))
            img.sprite = swapSprite;
        else
            img.sprite = defaultSprite;
	}
}
