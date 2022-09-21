using UnityEngine;
using UnityEngine.UI;

public class background : MonoBehaviour {

    public void TurnOff() {
        GetComponent<Image>().enabled = false;
        Time.timeScale = 1;
    }
}
