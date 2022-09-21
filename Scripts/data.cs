using UnityEngine;

public class data : MonoBehaviour {

    public bool shadows, sound;
    public int aura, coins, shields, doubleCoins, tail, pattern;
    public float shieldChanse, iceChanse, melChanse;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
