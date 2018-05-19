using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour {

	public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //asyncOperation.allowSceneActivation = false;
    }
}
