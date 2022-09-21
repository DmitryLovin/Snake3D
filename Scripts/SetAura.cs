using UnityEngine;

public class SetAura : MonoBehaviour {

    [SerializeField]
    private GameObject[] lights;

    private Animator _anim;

    void Start () {
        _anim = GetComponent<Animator>();
        _anim.Play("Rotation");
        int aura = GameObject.Find("_data").GetComponent<data>().aura;
        if (aura < 0)
        {

        }
        else if (aura < 8)
        {
            lights[0].SetActive(true);
            Light l = lights[0].GetComponent<Light>();
            l.color = AuraColors.colors1[aura];
        }
        else if (aura < 16)
        {

            lights[1].SetActive(true);
            Light l = lights[1].GetComponent<Light>();
            l.color = AuraColors.colors2[aura-8,0];
            lights[2].SetActive(true);
            l = lights[2].GetComponent<Light>();
            l.color = AuraColors.colors2[aura-8, 1];
        }
        else {
            lights[3].SetActive(true);
            Light l = lights[3].GetComponent<Light>();
            l.color = AuraColors.colors3[aura-16, 0];
            lights[4].SetActive(true);
            l = lights[4].GetComponent<Light>();
            l.color = AuraColors.colors3[aura-16, 1];
            lights[5].SetActive(true);
            l = lights[5].GetComponent<Light>();
            l.color = AuraColors.colors3[aura - 16, 2];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
