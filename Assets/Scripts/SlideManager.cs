using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SlideManager : MonoBehaviour
{
    private GameObject[] _slides;

    [SerializeField] private Transform slidesParent;

    private int _currentSlide = 0;

    private bool _isTransitioning = false;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private float _transitionSpeed = 0.3f;

    private void Start()
    {
        UpdateSlideIndex();
        SetUpSlides();
    }

    private void SetUpSlides()
    {
        var parentTransform = slidesParent.transform;

        _slides = new GameObject[parentTransform.childCount];

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _slides[i] = parentTransform.GetChild(parentTransform.childCount - (i + 1)).gameObject;

            // Activate only the first slide and deactivate the rest
            if (i != 0)
            {
                _slides[i].SetActive(false);
            }
            else
            {
                _slides[i].SetActive(true);
            }
        }

        Debug.Log("Total slide count: " + _slides.Length);
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
        if (_currentSlide < _slides.Length - 1 && !_isTransitioning)
        {
            SetAsFull();
            _slides[_currentSlide + 1].SetActive(true);
            _slides[_currentSlide].transform.DOLocalMoveY(1200, _transitionSpeed).onComplete += () =>
            {
                _slides[_currentSlide].SetActive(false);
                _currentSlide++;
                _slides[_currentSlide].SetActive(true);
                SetAsEmpty();
                UpdateSlideIndex();
            };
        }
    }

    public void PreviousSlide()
    {
        if (_currentSlide > 0 && !_isTransitioning)
        {
            SetAsFull();
            _slides[_currentSlide - 1].SetActive(true);
            _slides[_currentSlide - 1].transform.DOLocalMoveY(0, _transitionSpeed).onComplete += () =>
            {
                _slides[_currentSlide].SetActive(false);
                _currentSlide--;
                SetAsEmpty();
                UpdateSlideIndex();
            };
        }
    }

    private void SetAsFull()
    {
        _isTransitioning = true;
    }

    private void SetAsEmpty()
    {
        _isTransitioning = false;
    }

    private void UpdateSlideIndex()
    {
        textMeshProUGUI.text = (_currentSlide + 1).ToString();
    }
}