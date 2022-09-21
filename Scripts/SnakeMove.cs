using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMove : MonoBehaviour {

    public float speed = 0f;
    public Vector3 curSide = new Vector3(0,0,-1f);
    private Vector3 tmpCurSide;
    [SerializeField]
    private GameObject tail,shield,walls;
    [SerializeField]
    private GameObject[] asp;

    public int rot = 0;
    private int roty=0;
    private Vector3 nextPointV;

    public int score = 0;

    [SerializeField]
    private Text score_tx;
    [SerializeField]
    private Canvas canv;

    private bool isShield;

    private void Start()
    {
        nextPointV = transform.position + transform.forward;
    }

    private void nextPoint() {
        if (roty != 0)
        {
            if (transform.forward == Camera.main.GetComponent<CamMove>().direct)
            {
                Camera.main.GetComponent<CamMove>().direct -= (curSide + tmpCurSide);
            }
            else if (transform.forward == -Camera.main.GetComponent<CamMove>().direct)
            {
                Camera.main.GetComponent<CamMove>().direct += (curSide + tmpCurSide);
            }
            curSide = tmpCurSide;
            transform.rotation *= Quaternion.Euler(roty * 90, 0, 0);

            //Camera.main.GetComponent<CamMove>().direct = transform.forward;
            //Camera.main.GetComponent<CamMove>().newAngle *= Quaternion.Euler(roty * 90, 0, 0);
            tail.GetComponent<TailMove>().newPoints(0);
            roty = 0;
        }
        else if(rot !=0){
            transform.rotation *= Quaternion.Euler(roty * 90, 90 * rot, 0);
            //Camera.main.GetComponent<CamMove>().newAngle *= Quaternion.Euler(roty * 90, 0, -90 * rot);
            tail.GetComponent<TailMove>().newPoints(rot);
            rot = 0;
        }
        nextPointV = transform.position + transform.forward;
    }

    public void SetDirection(Vector2 key) {
        Vector2 tmpDir;
        Vector3 camDir = Camera.main.GetComponent<CamMove>().direct;
        camDir = new Vector3(Mathf.RoundToInt(camDir.x), Mathf.RoundToInt(camDir.y), Mathf.RoundToInt(camDir.z));
        if (curSide.x != 0){
            if (camDir.y != 0){
                tmpDir.x = transform.forward.z * camDir.y * curSide.x;
                tmpDir.y = transform.forward.y * camDir.y;
            }
            else{
                tmpDir.x = transform.forward.y * camDir.z * curSide.x * (-1);
                tmpDir.y = transform.forward.z * camDir.z;
            }
        }
        else if (curSide.y != 0){
            if (camDir.z != 0){
                tmpDir.x = transform.forward.x * camDir.z * curSide.y;
                tmpDir.y = transform.forward.z * camDir.z;
            }
            else{
                tmpDir.x = transform.forward.z * camDir.x * curSide.y * (-1);
                tmpDir.y = transform.forward.x * camDir.x;
            }
        }
        else{
            if (camDir.x != 0){
                tmpDir.x = transform.forward.y * camDir.x * curSide.z;
                tmpDir.y = transform.forward.x * camDir.x;
            }
            else{
                tmpDir.x = transform.forward.x * camDir.y * curSide.z * (-1);
                tmpDir.y = transform.forward.y * camDir.y;
            }
        }
        Vector2 tmpAv = tmpDir + key;
        if (Mathf.RoundToInt(tmpAv.x) == 0 || Mathf.RoundToInt(tmpAv.y) == 0){}
        else{
            rot = Mathf.RoundToInt(tmpAv.x) * Mathf.RoundToInt(tmpAv.y) * -1;
        }
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetDirection(new Vector2(1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            SetDirection(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetDirection(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            SetDirection(new Vector2(0, -1));
        }

        if (transform.position == nextPointV) {
            nextPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPointV, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag){
            case "Border": {
                    if (curSide == other.GetComponent<Barrirer>().dir1)
                    {
                        tmpCurSide = other.GetComponent<Barrirer>().dir2;
                    }
                    else
                    {
                        tmpCurSide = other.GetComponent<Barrirer>().dir1;
                    }
                    roty = 1;
                    break;
                }
            case "Tail": case "Stone": {
                    if (isShield)
                    {
                        return;
                    }
                    else if (GameObject.Find("_data").GetComponent<data>().shields > 0)
                    {
                        GameObject tmp = Instantiate(shield, gameObject.transform);
                        isShield = true;
                        StartCoroutine("ShieldOff", tmp);
                        GameObject.Find("_data").GetComponent<data>().shields--;
                        return;
                    }
                    speed = 0;
                    GetComponent<BoxCollider>().enabled = false;
                    canv.GetComponent<PauseMenu>().Lose();
                    if (GameObject.Find("_data").GetComponent<data>().doubleCoins > 0)
                    {
                        GameObject.Find("_data").GetComponent<data>().doubleCoins--;
                        score *= 2;
                    }
                    GameObject.Find("_data").GetComponent<data>().coins += score;
                    string tmp_sc = GameObject.Find("_data").GetComponent<data>().coins.ToString();
                    string path3 = Application.dataPath + "/coins";
                    StreamWriter writer = new StreamWriter(path3, false);
                    writer.Write(EnDeCrypt.Encrypt(tmp_sc));
                    writer.Close();
                    tmp_sc = GameObject.Find("_data").GetComponent<data>().doubleCoins.ToString();
                    path3 = Application.dataPath + "/doubleC";
                    writer = new StreamWriter(path3, false);
                    writer.Write(EnDeCrypt.Encrypt(tmp_sc));
                    writer.Close();
                    break;
                }
            case "Apple": {
                    other.transform.parent.GetComponent<AppleSpawn>().AddPoint(other.transform.position);
                    Destroy(other.gameObject);
                    score++;
                    score_tx.text = "Score     " + score;
                    speed += 0.1f;
                    tail.GetComponent<TailMove>().distance += 2;
                    walls.GetComponent<TestWalls>().InitSpawn();
                    asp[Random.Range(0, 6)].GetComponent<AppleSpawn>().InitSpawn();
                    break;
                }
            case "Shield": {
                    other.transform.parent.GetComponent<AppleSpawn>().AddPoint(other.transform.position);
                    Destroy(other.gameObject);
                    GameObject.Find("_data").GetComponent<data>().shields++;
                    asp[Random.Range(0, 6)].GetComponent<AppleSpawn>().InitSpawn();
                    break;
                }
            case "IceCream": {
                    other.transform.parent.GetComponent<AppleSpawn>().AddPoint(other.transform.position);
                    Destroy(other.gameObject);
                    if (speed > 4) speed--;
                    else speed = 3;
                    asp[Random.Range(0, 6)].GetComponent<AppleSpawn>().InitSpawn();
                    break;
                }
            case "Melon": {
                    other.transform.parent.GetComponent<AppleSpawn>().AddPoint(other.transform.position);
                    Destroy(other.gameObject);
                    score += 5;
                    score_tx.text = "Score     " + score;
                    speed += 0.1f;
                    tail.GetComponent<TailMove>().distance += 2;
                    walls.GetComponent<TestWalls>().InitSpawn();
                    asp[Random.Range(0, 6)].GetComponent<AppleSpawn>().InitSpawn();
                    break;
                }
            case "Chest": {
                    other.transform.parent.GetComponent<AppleSpawn>().AddPoint(other.transform.position);
                    Destroy(other.gameObject);
                    score += 25;
                    score_tx.text = "Score     " + score;
                    speed += 0.1f;
                    tail.GetComponent<TailMove>().distance += 2;
                    walls.GetComponent<TestWalls>().InitSpawn();
                    asp[Random.Range(0, 6)].GetComponent<AppleSpawn>().InitSpawn();
                    break;
                }
        }
    }

    IEnumerator ShieldOff(GameObject _go) {
        yield return new WaitForSeconds(1f);
        _go.transform.GetChild(0).GetComponent<Animator>().Play("Destroy");
        _go.transform.parent = null;
        //GetComponent<Renderer>().material.color = defColor;
        isShield = false;
        yield return new WaitForSeconds(2f);
        _go.GetComponent<Animator>().Play("goDown");
        yield return new WaitForSeconds(1f);
        Destroy(_go);
    }
}
