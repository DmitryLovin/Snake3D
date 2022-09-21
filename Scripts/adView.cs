using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using System.IO;

public class adView : MonoBehaviour {


    private int adsCount = 10;
    public GameObject image;
    public GameObject adsCImage,confirm;
    private int doubleCount;

    private void Start()
    {
        StartCoroutine("ChecnInternet");
    }

    IEnumerator ChecnInternet() {
        yield return new WaitForSeconds(1f);
        if (Advertisement.IsReady())
        {
            GetComponent<UnityEngine.UI.Button>().interactable = true;
            adsCImage.GetComponent<Image>().color = Color.white;
        }
        else if(adsCount>0){
            adsCount--;
            StartCoroutine("ChecnInternet");
        }
    }

    public void setDoubleCount() {
        doubleCount = GameObject.Find("_data").GetComponent<data>().doubleCoins;
        adsCImage.GetComponentInChildren<Text>().text = doubleCount + "/10";
    }

    public void canselWatch() {
        confirm.SetActive(false);
        image.SetActive(false);
    }

    public void showAd() {
        if (Advertisement.IsReady()) {
            confirm.SetActive(false);
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    public void ShowApp() {
        if (Advertisement.IsReady())
        {
            image.SetActive(true);
            confirm.SetActive(true);
        }
	}

    private void HandleAdResult(ShowResult result) {
        switch (result) {
            case ShowResult.Skipped: {
                    image.SetActive(false);
                    break;
                }
            case ShowResult.Finished: {
                    if (doubleCount < 9)
                    {
                        doubleCount += 2;
                        GameObject.Find("_data").GetComponent<data>().doubleCoins = doubleCount;
                        adsCImage.GetComponentInChildren<Text>().text = doubleCount + "/10";
                        string path = Application.dataPath + "/stats";
                        GameObject.Find("Canvas").GetComponent<MainMenu>().RecreateFile(path, GameObject.Find("Canvas").GetComponent<MainMenu>().CompileFile());
                    }
                    else if (doubleCount < 10) {
                        doubleCount ++;
                        GameObject.Find("_data").GetComponent<data>().doubleCoins = doubleCount;
                        adsCImage.GetComponentInChildren<Text>().text = doubleCount + "/10";
                        string path = Application.dataPath + "/stats";
                        GameObject.Find("Canvas").GetComponent<MainMenu>().RecreateFile(path, GameObject.Find("Canvas").GetComponent<MainMenu>().CompileFile());
                    }
                    image.SetActive(false);
                    break;
                }
            case ShowResult.Failed: {
                    image.SetActive(false);
                    break;
                }
        }
    }
}
