using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawn : MonoBehaviour
{

    [SerializeField]
    private Vector3 curSide;
    [SerializeField]
    private GameObject apple, stoneSP, shield, icecream, melon,chest;
    [SerializeField]
    private Transform _pl;

    private List<Vector3> curApples = new List<Vector3>();

    private float shChanse, iChanse, pChanse,chChanse;

    private void Start()
    {
        shChanse = GameObject.Find("_data").GetComponent<data>().shieldChanse;
        iChanse = shChanse + GameObject.Find("_data").GetComponent<data>().iceChanse;
        pChanse = iChanse + GameObject.Find("_data").GetComponent<data>().melChanse;
        chChanse = pChanse + ((GameObject.Find("_data").GetComponent<data>().shieldChanse+ GameObject.Find("_data").GetComponent<data>().iceChanse+ (GameObject.Find("_data").GetComponent<data>().melChanse/4)) / 15);
    }

    public void SetPoints(int pattern) {
        List<Vector2> tmpapples = new List<Vector2>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                tmpapples.Add(new Vector2(i + 0.5f, j + 0.5f));
                tmpapples.Add(new Vector2(i + 0.5f, -1 * (j + 0.5f)));
                tmpapples.Add(new Vector2(-1 * (i + 0.5f), j + 0.5f));
                tmpapples.Add(new Vector2(-1 * (i + 0.5f), -1 * (j + 0.5f)));
            }
        }
        for (int i = 0; i < PatternLists.patterns[pattern].Count; i++)
        {
            tmpapples.Remove(PatternLists.patterns[pattern][i]);
        }
        for (int i = 0; i < tmpapples.Count; i++)
        {
            if (curSide.x != 0)
            {
                curApples.Add(new Vector3(curSide.x * 10.4f, tmpapples[i].x, tmpapples[i].y));
            }
            else if (curSide.y != 0)
            {
                curApples.Add(new Vector3(tmpapples[i].x, curSide.y * 10.4f, tmpapples[i].y));
            }
            else
            {
                curApples.Add(new Vector3(tmpapples[i].x, tmpapples[i].y, curSide.z * 10.4f));
            }
        }
        InitSpawn();
    }

    public void InitSpawn() {
        StartCoroutine("SpawnApple");
    }

    public void AddPoint(Vector3 pos) {
        curApples.Add(pos);
    }

    IEnumerator SpawnApple()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            float shRand = Random.Range(0f, 1f);
            int tmpRand = Random.Range(0, curApples.Count);
            if (Vector3.Distance(curApples[tmpRand], _pl.position) > 5)
            {

                if (shRand < shChanse)
                {
                    GameObject tmpApple = Instantiate(shield, transform);
                    tmpApple.transform.position = curApples[tmpRand];
                }
                else if(shRand < iChanse)
                {
                    GameObject tmpApple = Instantiate(icecream, transform);
                    tmpApple.transform.position = curApples[tmpRand];
                }
                else if (shRand < pChanse)
                {
                    GameObject tmpApple = Instantiate(melon, transform);
                    tmpApple.transform.position = curApples[tmpRand];
                }
                else if (shRand < chChanse)
                {
                    GameObject tmpApple = Instantiate(chest, transform);
                    tmpApple.transform.position = curApples[tmpRand];
                }
                else
                {
                    GameObject tmpApple = Instantiate(apple, transform);
                    tmpApple.transform.position = curApples[tmpRand];
                }
                curApples.RemoveAt(tmpRand);
                break;
            }
        }
    }
}
