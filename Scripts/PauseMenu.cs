using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    private bool pause = false;
    private Animator _anim;
    private int id;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        float x_s_f = Screen.width / 1024f;
        float y_s_f = Screen.height / 768f;
        float s_f = (x_s_f + y_s_f) / 2;
        GetComponent<CanvasScaler>().scaleFactor = s_f;
        GameObject.Find("ChangeB").GetComponent<Animator>().Play("LoadingOut");
        _anim.Play("LevelStart");
        int pattern = Random.Range(0, 4);
        GameObject.Find("Walls").GetComponent<TestWalls>().SetWalls(pattern);
        GameObject.Find("AppleSpawner_XP").GetComponent<AppleSpawn>().SetPoints(pattern);
        GameObject.Find("AppleSpawner_XN").GetComponent<AppleSpawn>().SetPoints(pattern);
        GameObject.Find("AppleSpawner_YP").GetComponent<AppleSpawn>().SetPoints(pattern);
        GameObject.Find("AppleSpawner_YN").GetComponent<AppleSpawn>().SetPoints(pattern);
        GameObject.Find("AppleSpawner_ZP").GetComponent<AppleSpawn>().SetPoints(pattern);
        GameObject.Find("AppleSpawner_ZN").GetComponent<AppleSpawn>().SetPoints(pattern);
    }

    public void OnPause()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            _anim.Play("PauseOn");
            pause = !pause;
        }
        else {
            _anim.Play("PauseOff");
            pause = !pause;
        }
        
    }

    public void Resume() {
        Time.timeScale = 1;
    }

    public void MainMenu() {
        GameObject.Find("player").GetComponent<SnakeMove>().speed = 0;
        Time.timeScale = 1;
        id = 0;
        GameObject.Find("ChangeB").GetComponent<ChangeLevel>().levelToLoad = id;
        GameObject.Find("ChangeB").GetComponent<Animator>().Play("LoadingIn");
        Destroy(GameObject.Find("_data"));
    }

    public void Lose() {
        Time.timeScale = 0;
        _anim.Play("LoseOn");
    }

    public void Restart() {
        GameObject.Find("player").GetComponent<SnakeMove>().speed = 0;
        Time.timeScale = 1;
        id = 1;
        GameObject.Find("ChangeB").GetComponent<ChangeLevel>().levelToLoad = id;
        GameObject.Find("ChangeB").GetComponent<Animator>().Play("LoadingIn");
    }

    public void StartLevel() {
        GameObject.Find("player").GetComponent<SnakeMove>().speed = 3.0f;
    }
}
