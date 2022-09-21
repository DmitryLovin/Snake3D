using UnityEngine;

public class ChangeLevel : MonoBehaviour {

    public int levelToLoad;

    private void PlayGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
    }
}
