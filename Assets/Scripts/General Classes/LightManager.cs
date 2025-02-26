using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D flashLight;
    private float defaultIntensity;
    [SerializeField] private bool isOn;

    private void Start()
    {
        defaultIntensity = flashLight.intensity;
        if (isOn) TurnOn();
        else TurnOff();
    }
    private void TurnOn()
    {
        flashLight.intensity = 0;
        isOn = false;
    }
    private void TurnOff()
    {
        flashLight.intensity = defaultIntensity;
        isOn = true;
    }

    public void SwapLight()
    {
        if (isOn) TurnOff();
        else TurnOn();
    }
}
