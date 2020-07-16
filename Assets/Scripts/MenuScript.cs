using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public InstantiatorTest instantiator;
    public GameObject options;
    public GameObject boids;
    public GameObject submarine;
    public MasterBehaviour master;

    public void StartGame()
    {
        //SceneManager.LoadScene("Terrain");
        this.gameObject.transform.parent.gameObject.SetActive(false);
        instantiator.gameObject.SetActive(true);
        boids.SetActive(true);
        submarine.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangePerlinType()
    {
        if (dropdown.options[dropdown.value].text == "2D Perlin Basic")
        {
            instantiator.perlinType = 0;
        } else if (dropdown.options[dropdown.value].text == "2D Perlin Smooth")
        {
            instantiator.perlinType = 1;
        } else if (dropdown.options[dropdown.value].text == "3D Perlin") {
            instantiator.perlinType = 2;
        }
    }

    public void BackButton()
    {
        options.SetActive(false);
        this.gameObject.SetActive(true);
    }

    public void OptionsButton()
    {
        options.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void MuteSounds()
    {
       AudioSource[] audioList = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioList)
        {
            audio.enabled = !audio.enabled;
        }
    }

    public void AdjustCohesion()
    {
        Slider cohesionSlider = GameObject.Find("SliderCohesion").GetComponent<Slider>();
        master.adjustCohesionWeight(cohesionSlider.value);

        TMP_Text text = GameObject.Find("CohesionText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(cohesionSlider.value, 2).ToString();
    }

    public void AdjustSeparation()
    {
        Slider separationSlider = GameObject.Find("SliderSeparation").GetComponent<Slider>();
        master.adjustSeparationWeight(separationSlider.value);

        TMP_Text text = GameObject.Find("SeparationText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(separationSlider.value, 2).ToString();
    }

    public void AdjustAllignement()
    {
        Slider allignementSlider = GameObject.Find("SliderAllignement").GetComponent<Slider>();
        master.adjustAllignementWeight(allignementSlider.value);

        TMP_Text text = GameObject.Find("AlignmentText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(allignementSlider.value, 2).ToString();
    }

    public void AdjustObstacleAvoidance()
    {
        Slider obstacleSlider = GameObject.Find("SliderObstacleAvoidance").GetComponent<Slider>();
        master.adjustObstacleWeight(obstacleSlider.value);

        TMP_Text text = GameObject.Find("ObstacleText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(obstacleSlider.value, 2).ToString();
    }

    public void AdjustSearchRadius()
    {
        Slider radiusSlider = GameObject.Find("SliderSearchRadius").GetComponent<Slider>();
        Boids boidsScript = boids.GetComponent<Boids>();
        boidsScript.searchRadius = radiusSlider.value;

        TMP_Text text = GameObject.Find("SearchText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(radiusSlider.value, 2).ToString();
    }

    public void AdjustTargetTracking()
    {
        Slider targetSlider = GameObject.Find("SliderTargetTracking").GetComponent<Slider>();
        master.adjustTargetTrackingWeight(targetSlider.value);

        TMP_Text text = GameObject.Find("TargetText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(targetSlider.value, 2).ToString();
    }

    public void AdjustHorizontalAllignement()
    {
        Slider horizontalSlider = GameObject.Find("SliderHorizontalAllignement").GetComponent<Slider>();
        master.adjustHorizontalAllignementWeight(horizontalSlider.value);

        TMP_Text text = GameObject.Find("HorizontalText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(horizontalSlider.value, 2).ToString();
    }

    public void AdjustHeightLimitation()
    {
        Slider heightSlider = GameObject.Find("SliderHeightLimitation").GetComponent<Slider>();
        master.adjustHeightLimitationWeight(heightSlider.value);

        TMP_Text text = GameObject.Find("HeightText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(heightSlider.value, 2).ToString();
    }

    public void AdjustMaxDistance()
    {
        Slider maxDistanceSlider = GameObject.Find("SliderMaxDistance").GetComponent<Slider>();
        master.adjustMaxDistance(maxDistanceSlider.value * maxDistanceSlider.value);

        TMP_Text text = GameObject.Find("MaxDistanceText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(maxDistanceSlider.value, 2).ToString();
    }

    public void AdjustMaxHeight()
    {
        Slider maxHeightSlider = GameObject.Find("SliderMaxHeight").GetComponent<Slider>();
        master.adjustMaxHeight(maxHeightSlider.value);

        TMP_Text text = GameObject.Find("MaxHeightText").GetComponent<TMP_Text>();
        text.text = System.Math.Round(maxHeightSlider.value, 2).ToString();
    }

    public void adjustNumberOfBoids()
    {
        Slider boidsSlider = GameObject.Find("SliderBoids").GetComponent<Slider>();
        Boids boidsScript = boids.GetComponent<Boids>();
        boidsScript.numberOfBoids = Mathf.FloorToInt(boidsSlider.value);
    }

    public void resetScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Terrain");
    }
}
