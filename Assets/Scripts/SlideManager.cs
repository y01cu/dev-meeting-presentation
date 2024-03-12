using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public GameObject[] slides;
    public int currentSlide = 0;

    public void NextSlide()
    {
        if (currentSlide < slides.Length - 1)
        {
            slides[currentSlide].SetActive(false);
            currentSlide++;
            slides[currentSlide].SetActive(true);
        }
    }

    public void PreviousSlide()
    {
        if (currentSlide > 0)
        {
            slides[currentSlide].SetActive(false);
            currentSlide--;
            slides[currentSlide].SetActive(true);
        }
    }
}