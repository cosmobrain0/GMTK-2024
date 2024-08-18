using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBar : MonoBehaviour
{
    public Scalable rightEnd;
    public Scalable leftEnd;
    [Range(0, 0.25f)]
    public float maxTurns;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rightPercentage = PercentageAcross(rightEnd);
        float leftPercentage = PercentageAcross(leftEnd);
        float rotation = (leftPercentage - rightPercentage) * maxTurns * 2f * Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, rotation * Mathf.Rad2Deg);
    }

    float PercentageAcross(Scalable s)
    {
        return (s.CurrentScale - s.smallScale) / (s.largeScale - s.smallScale);
    }
}
