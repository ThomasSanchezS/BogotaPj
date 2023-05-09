using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class EnergyBar : MonoBehaviour
{
    public float energy = 100.0f;
    public float maxEnergy = 100.0f;
    public float decreaseRate = 1.0f;
    public float threshold = 50.0f;
    public float visibilityMultiplier = 1.0f;
    public float startingSpeed;
    public float startingRunningSpeed;
    public Camera cam;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public PlayerMovement playerMovement;
    PostProcessVolume m_Volume;
    Vignette m_Vignette;
    public SetParameterByID setParameterByID;

    void Start()
    {
        setParameterByID.volume = 0f;
        slider.maxValue = energy;
        slider.value = energy;
        fill.color = gradient.Evaluate(1f);
        playerMovement = GetComponent<PlayerMovement>();
        startingSpeed = playerMovement.speed;
        startingRunningSpeed = playerMovement.runSpeed;
         m_Vignette = ScriptableObject.CreateInstance<Vignette>();
         m_Vignette.enabled.Override(true);
         m_Vignette.intensity.Override(0.1f);
         m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_Vignette);
    }

    void Update()
    {
        DecreaseEnergy();
        //UpdateUI();
        CheckThreshold();
        AdjustSpeedAndVisibility();
    }

    void DecreaseEnergy()
    {
        energy -= decreaseRate * Time.deltaTime;

        if (energy < 0)
        {
            energy = 0;
        }
        slider.value = energy;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        float vol= Mathf.Round(slider.normalizedValue * 100f) / 100f;
        setParameterByID.volume = 1 - vol;
    }

    /*void UpdateUI()
    {
        energyBarUI.fillAmount = energy / maxEnergy;
    }*/

    void CheckThreshold()
    {
        if (energy < threshold)
        {
            // Play sound or show warning message
        }
    }

    void AdjustSpeedAndVisibility()
    {
        if (energy < maxEnergy / 2)
        {
            playerMovement.speed = startingSpeed *((2 * energy) / maxEnergy);
            playerMovement.runSpeed = startingRunningSpeed*((2 * energy) / maxEnergy);
            m_Vignette.intensity.value = 0.4f - (0.3f * (Mathf.Pow((2*energy/maxEnergy),2)));
            if(playerMovement.speed <= 2f){
                playerMovement.speed = 2f;
            }
            if(playerMovement.runSpeed <= 2.5f){
                 playerMovement.runSpeed = 2.5f;
            }
            // Reduce visibility using e.g. post-processing effects
        }
        else
        {
            playerMovement.speed = startingSpeed / 1;
            playerMovement.runSpeed = startingRunningSpeed/1;
            m_Vignette.intensity.value = 0.1f;
            // Reset visibility to normal
        }
    }

    public void AddEnergy(float energyAmount){

        energy += energyAmount;

        if(energy >= maxEnergy){

            energy = maxEnergy;
        }
    }
}
