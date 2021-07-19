using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Flashlight : MonoBehaviour
{
    /*############TODO############################
    -100,75,50,25,0 %
    -Przenoszenie barteii do kolejnego poziomu
    -Podnoszenie latarki
    -odstraszanie potworów
    -włączanie znaków
    -śledzenie szlaku
     */
    [SerializeField] private SceneCollector sceneManager;
    [SerializeField] private Light2D light2d;
    [SerializeField] private GameObject flashlightLight;
    [SerializeField] private GameObject fakeFlashlight;
    [SerializeField] private LightFlickering lightFlickering;
    [SerializeField] private AudioSource flashlightReload;
    [SerializeField] private float baseBatteryTimeInSeconds = 60;//i to
    [SerializeField] private float angle;


    private float batteryPercent;
    private float batteryTimeInSeconds;//to
    private float flashlightTimeOn;
    private bool playReloadSound;

    public float Angle { get => angle; set => angle = value; }

    private void Awake()
    {
        if (sceneManager.getCurrentSceneName() == "Lvl1")
        {
            batteryTimeInSeconds = baseBatteryTimeInSeconds;
        }
        else
        {
            batteryTimeInSeconds = PlayerStats.BatteryLevel;
        }
        playReloadSound = false;
    }
    private void FixedUpdate()
    {

        //50 times / second
        FollowMousePosition();
        UpdateBatteryStatus();
        PlayReloadSound();
        if (Time.timeScale == 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        PlayerStats.BatteryLevel = batteryTimeInSeconds;
    }
    private void PlayReloadSound()
    {
        if (playReloadSound)
        {
            flashlightReload.Play();
            playReloadSound = false;
        }
    }
    private void UpdateBatteryStatus()
    {
        flashlightTimeOn++;
        if (batteryTimeInSeconds > 0 && flashlightTimeOn % 50 == 0)
        {
            batteryTimeInSeconds --;
        }
        batteryPercent =  batteryTimeInSeconds / baseBatteryTimeInSeconds * 100; 
        ChangeLightningIntensity();
    }
    private void ChangeLightningIntensity()
    {
        if (batteryPercent > 75 && batteryPercent < 100)
        {
            flashlightLight.transform.localScale = new Vector3(1, 1, 1);
            light2d.intensity = 1;
            lightFlickering.enabled = false;

        }
        else if (batteryPercent > 50 && batteryPercent < 75)
        {
            flashlightLight.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
            light2d.intensity = 0.85f;
            lightFlickering.enabled = false;

        }
        else if (batteryPercent > 25 && batteryPercent < 50)
        {
            flashlightLight.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            light2d.intensity = 0.7f;
            lightFlickering.enabled = true;
            lightFlickering.MinFlickeringSpeedInFrames = 45;
            lightFlickering.MaxFlickeringSpeedInFrames = 70;
            lightFlickering.MaxIntensityRangeJump = 0.2f;
            lightFlickering.MinIntensityRange = light2d.intensity;

        }
        else if (batteryPercent > 0 && batteryPercent < 25)
        {
            flashlightLight.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            light2d.intensity = 0.5f;
            lightFlickering.enabled = true;
            lightFlickering.MinFlickeringSpeedInFrames = 3;
            lightFlickering.MaxFlickeringSpeedInFrames = 7;
            lightFlickering.MaxIntensityRangeJump = 0.7f;
            lightFlickering.MinIntensityRange = light2d.intensity;


        }
        else if (batteryPercent == 0 )
        {
            gameObject.SetActive(false);
            fakeFlashlight.SetActive(true);
        }
    }
    private void FollowMousePosition()
    {
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    public void RefillBattery()
    {
        gameObject.SetActive(true);
        playReloadSound = true;
        batteryTimeInSeconds = baseBatteryTimeInSeconds;
        UpdateBatteryStatus();
        gameObject.SetActive(false);

    }
    public void PickupFlashlight()
    {
        playReloadSound = false;
        batteryTimeInSeconds = baseBatteryTimeInSeconds;
        gameObject.SetActive(true);
        UpdateBatteryStatus();
    }
}
