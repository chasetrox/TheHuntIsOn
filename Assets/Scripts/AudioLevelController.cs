using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* Alex Pantuck
 * Comp50
 * 5/8/18
 * AudioLevelController.cs
 * 
 * This script allows the player to independently
 * set audio for different types through the menu
 * 
 */

public class AudioLevelController : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetAmbient()
    {
        float num = GetComponent<Slider>().value;
        mixer.SetFloat("AmbientVol", num);
    }

    public void SetMaster()
    {
        float num = GetComponent<Slider>().value;
        mixer.SetFloat("MasterVol", num);
    }

    public void SetSFX()
    {
        float num = GetComponent<Slider>().value;
        mixer.SetFloat("SFXVol", num);
    }
}
