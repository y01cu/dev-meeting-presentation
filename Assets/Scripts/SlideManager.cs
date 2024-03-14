using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    public GameObject[] slides;
    public int currentSlide = 0;

    private bool isTransitioning = false;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Start()
    {
        UpdateSlideIndex();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            NextSlide();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            PreviousSlide();
        }
    }

    public void NextSlide()
    {
        if (currentSlide < slides.Length - 1 && !isTransitioning)
        {
            SetAsFull();
            slides[currentSlide + 1].SetActive(true);
            slides[currentSlide].transform.DOLocalMoveY(1200, 0.2f, false).onComplete += () =>
            {
                slides[currentSlide].SetActive(false);
                currentSlide++;
                slides[currentSlide].SetActive(true);
                SetAsEmpty();
                UpdateSlideIndex();
            };
        }
    }

    public void PreviousSlide()
    {
        if (currentSlide > 0 && !isTransitioning)
        {
            SetAsFull();
            slides[currentSlide - 1].SetActive(true);
            slides[currentSlide - 1].transform.DOLocalMoveY(0, 0.2f, false).onComplete += () =>
            {
                slides[currentSlide].SetActive(false);
                currentSlide--;
                SetAsEmpty();
                UpdateSlideIndex();
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

    private void UpdateSlideIndex()
    {
        textMeshProUGUI.text = (currentSlide + 1).ToString();
    }
}