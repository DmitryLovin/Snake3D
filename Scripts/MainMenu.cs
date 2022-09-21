using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Animator _anim;
    private GameObject data;
    private bool load;

    public Sprite check_spr;

    public Sprite[] colorsSPR;
    public GameObject[] auraBtns;

    private int colorIndex = 0;
    private GameObject colorBtn;

    private int[] auras,tails,patterns;

    private int curCustMenu=0;

    [SerializeField]
    private GameObject[] customPages; 

    private void Start()
    {
        data = GameObject.Find("_data");
        GetStats();
        GetSettings();
        _anim = GetComponent<Animator>();
        float x_s_f = Screen.width / 1024f;
        float y_s_f = Screen.height / 768f;
        float s_f = (x_s_f + y_s_f) / 2;
        GetComponent<CanvasScaler>().scaleFactor = s_f;
        GameObject.Find("SpaceB").GetComponent<Animator>().Play("LoadingOut");

    }

    public void PlayGame() {
        GameObject.Find("SpaceB").GetComponent<ChangeLevel>().levelToLoad = 1;
        GameObject.Find("SpaceB").GetComponent<Animator>().Play("LoadingIn");
    }

    public void Options(bool onOff) {
        if(onOff)
            _anim.Play("Options");
        else
            _anim.Play("OptionsBack");
    }

    public void CustomMenu(bool onOff) {
        if (onOff)
            GameObject.Find("MainMenu").GetComponent<Animator>().Play("CustOn");
        else
            GameObject.Find("MainMenu").GetComponent<Animator>().Play("CustOff");
    }

    public void TailsMenu(bool onOff) {

    }

    public void PatternsMenu(bool onOff)
    {
        
    }

    public void AuraMenu(bool onOff)
    {
        if (onOff)
            _anim.Play("AuraOn");
        else
            _anim.Play("AuraOff");
    }

    private void GetSettings() {
        string path = Application.dataPath + "/settings.ini";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadToEnd();
            reader.Close();
            load = true;
            string[] types = line.Split('\n');
            for (int i = 0; i < types.Length; i++)
            {
                string[] id_value = types[i].Split('=');
                switch (id_value[0])
                {
                    case "shadows":
                        {
                            if (id_value[1] == "False")
                            {
                                QualitySettings.SetQualityLevel(1, true);
                                data.GetComponent<data>().shadows = false;
                                GameObject.Find("Shadows").GetComponent<Toggle>().isOn = false;
                            }
                            else
                            {
                                QualitySettings.SetQualityLevel(0, true);
                                GameObject.Find("Shadows").GetComponent<Toggle>().isOn = true;
                                data.GetComponent<data>().shadows = true;
                            }
                            break;
                        }
                    case "sound":
                        {
                            if (id_value[1] == "False")
                            {
                                GameObject.Find("Sound").GetComponent<Toggle>().isOn = false;
                                data.GetComponent<data>().sound = false;
                                AudioListener.volume = 0;
                            }
                            else
                            {
                                GameObject.Find("Sound").GetComponent<Toggle>().isOn = true;
                                data.GetComponent<data>().sound = true;
                                AudioListener.volume = 1;
                            }
                            break;
                        }
                }
            }
            load = false;
        }
        else {
            RecreateFile(path, RebuildSettings()); GetSettings(); return;
        }
    }

    private void GetStats()
    {
        string path = Application.dataPath + "/stats";
        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string line = EnDeCrypt.Decrypt(reader.ReadToEnd()); ;
            reader.Close();
            string[] types = line.Split('\n');

            #region readCoins
            if (!Int32.TryParse(types[0], out data.GetComponent<data>().coins)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            GameObject.Find("Coins").GetComponent<Text>().text = data.GetComponent<data>().coins.ToString();
            #endregion

            #region readAuras
            string[] tmpline = types[1].Split(' ');
            if (!Int32.TryParse(tmpline[0], out data.GetComponent<data>().aura)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            auras = new int[tmpline.Length - 1];
            for (int i = 1; i < tmpline.Length; i++)
            {
                if (!Int32.TryParse(tmpline[i], out auras[i - 1])) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            }
            for (int i = 0; i < auras.Length; i++)
            {
                if (auras[i] == 1)
                {
                    auraBtns[i].GetComponent<Image>().sprite = colorsSPR[i];
                    auraBtns[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
            GameObject check = new GameObject();
            check.name = "check_aura";
            Image img_check = check.AddComponent<Image>();
            img_check.sprite = check_spr;
            check.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
            check.GetComponent<RectTransform>().SetParent(auraBtns[data.GetComponent<data>().aura].transform);
            check.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            check.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
            check.SetActive(true);
            #endregion

            #region readTails
            tmpline = types[2].Split(' ');
            if (!Int32.TryParse(tmpline[0], out data.GetComponent<data>().tail)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            tails = new int[tmpline.Length - 1];
            for (int i = 1; i < tmpline.Length; i++)
            {
                if (!Int32.TryParse(tmpline[i], out tails[i - 1])) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            }
            #endregion

            #region readPatterns
            tmpline = types[3].Split(' ');
            if (!Int32.TryParse(tmpline[0], out data.GetComponent<data>().pattern)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            patterns = new int[tmpline.Length - 1];
            for (int i = 1; i < tmpline.Length; i++)
            {
                if (!Int32.TryParse(tmpline[i], out patterns[i - 1])) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            }
            #endregion

            #region readShieldChanse
            int tmpInt;
            if (!Int32.TryParse(types[4], out tmpInt)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            data.GetComponent<data>().shieldChanse = tmpInt * 0.01f;
            #endregion

            #region readIceCreamChanse
            if (!Int32.TryParse(types[5], out tmpInt)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            data.GetComponent<data>().iceChanse = tmpInt * 0.01f;
            #endregion

            #region readMelonChanse
            if (!Int32.TryParse(types[6], out tmpInt)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            data.GetComponent<data>().melChanse = tmpInt * 0.04f;
            #endregion

            #region readShieldsCount
            if (!Int32.TryParse(types[7], out data.GetComponent<data>().shields)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            #endregion

            #region readDoubleCount
            if (!Int32.TryParse(types[8], out data.GetComponent<data>().doubleCoins)) { RecreateFile(path, RebuildFile()); GetStats(); return; }
            GameObject.Find("DoubleCoins").GetComponent<adView>().setDoubleCount();
            #endregion
        }
        else {
            RecreateFile(path, RebuildFile());
            GetStats();
            return;
        }
    }

    public void RecreateFile(string path, string file) {
        StreamWriter writer = new StreamWriter(path,false);
        writer.Write(file);
        writer.Close();
    }

    private string RebuildFile() {
        string file = "0\n";
        file += "0 1 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n";
        file += "0 1 0 0 0 0 0 0 0\n";
        file += "0 1 0 0 0 0 0 0 0\n";
        file += "1\n";
        file += "1\n";
        file += "1\n";
        file += "1\n";
        file += "0\n";
        return EnDeCrypt.Encrypt(file);
    }

    public string CompileFile() {
        string file = data.GetComponent<data>().coins.ToString() + "\n";
        file += data.GetComponent<data>().aura.ToString();
        for (int i = 0; i < auras.Length; i++) {
            file += " " + auras[i];
        }
        file += "\n";

        file += data.GetComponent<data>().tail.ToString();
        for (int i = 0; i < tails.Length; i++)
        {
            file += " " + tails[i];
        }
        file += "\n";

        file += data.GetComponent<data>().pattern.ToString();
        for (int i = 0; i < patterns.Length; i++)
        {
            file += " " + patterns[i];
        }
        file += "\n";

        file += (data.GetComponent<data>().shieldChanse * 100).ToString() + "\n";

        file += (data.GetComponent<data>().iceChanse * 100).ToString() + "\n";

        file += (data.GetComponent<data>().melChanse * 25).ToString() + "\n";

        file += data.GetComponent<data>().shields.ToString() + "\n";

        file += data.GetComponent<data>().doubleCoins.ToString();
        return EnDeCrypt.Encrypt(file);
    }

    private string RebuildSettings() {
        string file = "shadows=True\n";
        file += "sound=True";
        return file;
    }

    private string CompileSettings()
    {
        string file = "shadows="+data.GetComponent<data>().shadows+"\n";
        file += "sound="+data.GetComponent<data>().sound;
        return file;
    }

    public void CheckCh() {
        if (!load) {
            string path = Application.dataPath + "/settings.ini";
            data.GetComponent<data>().shadows = GameObject.Find("Shadows").GetComponent<Toggle>().isOn;
            if (data.GetComponent<data>().shadows)
            {
                QualitySettings.SetQualityLevel(0, true);
            }
            else {
                QualitySettings.SetQualityLevel(1, true);
            }
            data.GetComponent<data>().sound = GameObject.Find("Sound").GetComponent<Toggle>().isOn;
            if (data.GetComponent<data>().sound)
            {
                AudioListener.volume = 1;
            }
            else {
                AudioListener.volume = 0;
            }
            RecreateFile(path, CompileSettings());
        }
    }

    public void SetAura(GameObject _go) {
        string[] btnSprName = _go.GetComponent<Image>().sprite.name.Split('-');
        if (btnSprName[0] == "Unknown")
        {
            string[] tmp = _go.name.Split('-');
            Int32.TryParse(tmp[tmp.Length-2], out colorIndex);
            colorBtn = _go;
            GameObject buyBtn = GameObject.Find("AuraMenu").transform.Find("Menu/Buy").gameObject;
            buyBtn.GetComponent<Animator>().Play("Buy");
            if (tmp.Length == 3)
            {
                string color = ColorUtility.ToHtmlStringRGBA(AuraColors.colors1[colorIndex]);
                buyBtn.transform.Find("Background/BuyText").GetComponent<Text>().text = "Buy <color=#" + color + ">" + tmp[0] + "</color> color";
            }
            else if (tmp.Length == 4)
            {
                string color1 = ColorUtility.ToHtmlStringRGBA(AuraColors.colors2[colorIndex-8,0]);
                string color2 = ColorUtility.ToHtmlStringRGBA(AuraColors.colors2[colorIndex-8,1]);
                buyBtn.transform.Find("Background/BuyText").GetComponent<Text>().text = "Buy <color=#" + color1 + ">" + tmp[0] + "</color>-<color=#"+color2+">"+tmp[1]+"</color> color";
            }
            else {
                string color1 = ColorUtility.ToHtmlStringRGBA(AuraColors.colors3[colorIndex-16,0]);
                string color2 = ColorUtility.ToHtmlStringRGBA(AuraColors.colors3[colorIndex-16,1]);
                string color3 = ColorUtility.ToHtmlStringRGBA(AuraColors.colors3[colorIndex-16,2]);
                buyBtn.transform.Find("Background/BuyText").GetComponent<Text>().text = "Buy <color=#" + color1 + ">" + tmp[0] + "</color>-<color=#" + color2 + ">" + tmp[1] + "</color>-<color=#"+color3+">"+tmp[2]+"</color> color";
            }
            int price;
            Int32.TryParse(tmp[tmp.Length-1], out price);
            buyBtn.transform.Find("BuyBtn/Price").GetComponent<Text>().text = tmp[tmp.Length - 1];
            if (price <= data.GetComponent<data>().coins) {
                buyBtn.transform.Find("BuyBtn").GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }
        else {
            if (GameObject.Find("check_aura"))
            {
                Destroy(GameObject.Find("check_aura"));
            }
            int tmpA = -1;
            string[] tmp = _go.name.Split('-');
            Int32.TryParse(tmp[tmp.Length-2], out tmpA);
            data.GetComponent<data>().aura = tmpA;
            string path = Application.dataPath + "/stats";
            RecreateFile(path, CompileFile());
            GameObject check = new GameObject();
            check.name = "check_aura";
            Image img_check = check.AddComponent<Image>();
            img_check.sprite = check_spr;
            check.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
            check.GetComponent<RectTransform>().SetParent(_go.transform);
            check.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            check.SetActive(true);
        }
    }

    public void BuyAura(bool _b) {
        GameObject buyBtn = GameObject.Find("AuraMenu").transform.Find("Menu/Buy").gameObject;
        buyBtn.GetComponent<Animator>().Play("BuyOff");
        if (_b) {
            int price;
            Int32.TryParse(buyBtn.transform.Find("BuyBtn/Price").GetComponent<Text>().text, out price);
            data.GetComponent<data>().coins -= price;
            colorBtn.GetComponent<Image>().sprite = colorsSPR[colorIndex];
            colorBtn.GetComponent<Image>().color = Color.white;
            auras[colorIndex] = 1;
            GameObject.Find("Coins").GetComponent<Text>().text = data.GetComponent<data>().coins.ToString();
            SetAura(colorBtn);

        }
        buyBtn.transform.Find("BuyBtn").GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    public void CustomSwitch(int _s) {
        if (_s > 0)
        {
            customPages[curCustMenu].GetComponent<Animator>().Play("ToLeft");
            customPages[curCustMenu+1].GetComponent<Animator>().Play("FromRight");
            curCustMenu += _s;
            if (curCustMenu == 1) {
                GameObject.Find("LeftBtn").GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            if (curCustMenu == 2)
            {
                GameObject.Find("RightBtn").GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
        else {
            customPages[curCustMenu].GetComponent<Animator>().Play("ToRight");
            customPages[curCustMenu - 1].GetComponent<Animator>().Play("FromLeft");
            curCustMenu += _s;
            if (curCustMenu == 1)
            {
                GameObject.Find("RightBtn").GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            if (curCustMenu == 0)
            {
                GameObject.Find("LeftBtn").GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    }

}
