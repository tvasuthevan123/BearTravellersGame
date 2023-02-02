using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterLighting : MonoBehaviour
{
    static readonly float[] times = new float[3] {
        0.565f,
        0.338f,
        0.144f
    };

    public Color red;

    public Color yellow;
    
    static readonly Color[] neutralColors = new Color[3] {
        new Color(1f, 42/255f, 0f),
        new Color(1f, 194/255f, 0f),
        Color.white
    };

    static readonly Color[] worstColors = new Color[3] {
        Color.magenta,
        Color.red,
        new Color(48/255f, 48/255f, 48/255f)
    };

    static readonly Color[] bestColors = new Color[3] {
        new Color(7/255f, 99/255f, 38/255f),
        new Color(1f, 194/255f, 0f),
        Color.white
    };

    static readonly GradientAlphaKey[] alphaKey = new GradientAlphaKey[2]{
        new GradientAlphaKey(1, 0),
        new GradientAlphaKey(1, 1)
    };

    ParticleSystem fire;

    GradientColorKey[] colorKey;

    Gradient gradient;

    void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
        colorKey = new GradientColorKey[3] {
            new GradientColorKey(neutralColors[0], times[0]),
            new GradientColorKey(neutralColors[1], times[1]),
            new GradientColorKey(neutralColors[2], times[2]),
        };
        gradient = new Gradient();
        gradient.SetKeys(colorKey, alphaKey);
        var col = fire.colorOverLifetime;
        col.color = gradient;
    }


    // Update is called once per frame
    void Update()
    {
        int friendshipMeter = ((Ink.Runtime.IntValue) StoryManager.instance.GetVariableState("friendship_meter")).value;
        if(friendshipMeter >= 0){
            setGradientBetweenKeys(neutralColors, bestColors, (float)friendshipMeter/15);
        }
        else if(friendshipMeter < 0){
            setGradientBetweenKeys(neutralColors, worstColors, (float)friendshipMeter/-15);
        }
        gradient.SetKeys(colorKey, alphaKey);
        var col = fire.colorOverLifetime;
        col.color = gradient;
    }

    void setGradientBetweenKeys(Color[] a, Color[] b, float t){
        colorKey[0].color = Color.Lerp(a[0], b[0], t);
        colorKey[1].color = Color.Lerp(a[1], b[1], t);
        colorKey[2].color = Color.Lerp(a[2], b[2], t);
    }
}
