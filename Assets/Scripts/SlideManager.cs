using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public GameObject[] slides;
    public int currentSlide = 0;

    private bool isTransitioning = false;

    public void NextSlide()
    {
        if (currentSlide < slides.Length - 1 && !isTransitioning)
        {
            SetAsFull();
            slides[currentSlide + 1].SetActive(true);
            slides[currentSlide].transform.DOLocalMoveY(1200, 1, false).onComplete += () =>
            {
                slides[currentSlide].SetActive(false);
                currentSlide++;
                slides[currentSlide].SetActive(true);
                SetAsEmpty();
            };
        }
    }

    private void SetAsFull()
    {
        isTransitioning = true;
    }

    private void SetAsEmpty()
    {
        isTransitioning = false;
    }

    private IEnumerator AnimateSlideWithDirection(bool isForward)
    {
        yield return new WaitForSeconds(.5f);
        NextSlide();
    }

    public void PreviousSlide()
    {
        if (currentSlide > 0 && !isTransitioning)
        {
            SetAsFull();
            slides[currentSlide - 1].SetActive(true);
            slides[currentSlide - 1].transform.DOLocalMoveY(0, 1, false).onComplete += () =>
            {
                slides[currentSlide].SetActive(false);
                currentSlide--;
                SetAsEmpty();
            };
        }
    }
}