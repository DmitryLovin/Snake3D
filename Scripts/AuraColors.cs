using UnityEngine;

public class AuraColors {

    public static Color[] colors1 = new Color[]{
        new Color32(0,255,0,255),
        new Color32(255,0,0,255),
        new Color32(0,0,255,255),
        new Color32(255,0,255,255),
        new Color32(255,255,0,255),
        new Color32(0,255,255,255),
        new Color32(135,65,0,255),
        new Color32(255,255,255,255)
    };

    public static Color[,] colors2= new Color[,] {
        {new Color32(0,0,255,255), new Color32(255,0,0,255)},
        {new Color32(0,255,0,255), new Color32(255,255,0,255)},
        {new Color32(255,0,0,255), new Color32(255,135,0,255)},
        {new Color32(0,255,255,255), new Color32(0,255,0,255)},
        {new Color32(135,65,0,255), new Color32(135,0,255,255)},
        {new Color32(0,0,255,255), new Color32(0,255,255,255)},
        {new Color32(255,0,255,255), new Color32(255,0,0,255)},
        {new Color32(255,255,0,255), new Color32(0,0,255,255)}
    };

    public static Color[,] colors3 = new Color[,] {
        {new Color32(255,0,0,255), new Color32(0,255,0,255), new Color32(0,0,255,255)},
        {new Color32(255,0,255,255), new Color32(255,255,0,255), new Color32(0,255,255,255)},
        {new Color32(135,65,0,255), new Color32(0,255,0,255), new Color32(135,0,255,255)},
        {new Color32(0,255,255,255), new Color32(0,0,255,255), new Color32(0,255,0,255)},
        {new Color32(255,0,0,255), new Color32(255,135,0,255), new Color32(255,255,0,255)},
        {new Color32(135,0,255,255), new Color32(255,0,0,255), new Color32(255,0,255,255)}
    };
}
