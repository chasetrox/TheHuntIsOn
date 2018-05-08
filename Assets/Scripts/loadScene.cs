using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

    public string sceneName;

	public void loadTheScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
