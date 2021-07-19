using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField] private float minIntensityRange;
    [SerializeField] private float maxIntensityRange;
    [SerializeField] private float maxIntensityRangeJump;
    [SerializeField] private int minFlickeringSpeedInFrames;
    [SerializeField] private int maxFlickeringSpeedInFrames;
    [SerializeField] private float minInnerRadiusRange;
    [SerializeField] private float maxInnerRadiusRange;
    [SerializeField] private float minOuterRadiusRange;
    [SerializeField] private float maxOuterRadiusRange;
    private Light2D light2d;

    public float MinIntensityRange { get => minIntensityRange; set => minIntensityRange = value; }
    public float MaxIntensityRange { get => maxIntensityRange; set => maxIntensityRange = value; }
    public float MaxIntensityRangeJump { get => maxIntensityRangeJump; set => maxIntensityRangeJump = value; }
    public int MinFlickeringSpeedInFrames { get => minFlickeringSpeedInFrames; set => minFlickeringSpeedInFrames = value; }
    public int MaxFlickeringSpeedInFrames { get => maxFlickeringSpeedInFrames; set => maxFlickeringSpeedInFrames = value; }
    public float MinInnerRadiusRange { get => minInnerRadiusRange; set => minInnerRadiusRange = value; }
    public float MaxInnerRadiusRange { get => maxInnerRadiusRange; set => maxInnerRadiusRange = value; }
    public float MinOuterRadiusRange { get => minOuterRadiusRange; set => minOuterRadiusRange = value; }
    public float MaxOuterRadiusRange { get => maxOuterRadiusRange; set => maxOuterRadiusRange = value; }

    private void Awake()
    {
        light2d = GetComponent<Light2D>();
    }
    private void FixedUpdate()
    {
        
        if (Time.frameCount % Random.Range(maxFlickeringSpeedInFrames, minFlickeringSpeedInFrames) == 0)
        {
            float tempIntensity = Random.Range(minIntensityRange, maxIntensityRange);
            if (Mathf.Abs(tempIntensity - light2d.intensity) > maxIntensityRangeJump)
            {
                if (light2d.intensity + maxIntensityRangeJump > maxIntensityRange)
                {
                    light2d.intensity -= maxIntensityRangeJump;
                }
                else
                {
                    light2d.intensity += maxIntensityRangeJump;
                }
            }
            else
            {
                light2d.intensity = tempIntensity;
            }
            light2d.pointLightInnerRadius = Random.Range(minInnerRadiusRange, maxInnerRadiusRange); 
            light2d.pointLightOuterRadius = Random.Range(minOuterRadiusRange, maxOuterRadiusRange);

        }
        //else if(frame count) itd
        //przydatne do rozbijania drogich funkcji na rozne klatki
    }
}
