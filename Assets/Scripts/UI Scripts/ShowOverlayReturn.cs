using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowOverlayReturn : MonoBehaviour
{
    float destroyTime = 2f;

    public GameObject objectToVisualise;
    public Button returnButton;
    public string sceneToLoad;

    void Start()
    {
        
        objectToVisualise.SetActive(false);
    }

    private void TaskOnClick()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        Debug.Log("ReturnButton Pressed!");
    }

    void Update()
    {
        ShowReturnButtonOverlayOnClick();
    }

    IEnumerator ShowButtonForSomeTime()
    {
        objectToVisualise.SetActive(true);

        returnButton.onClick.AddListener(TaskOnClick);

        yield return new WaitForSeconds(destroyTime);

        objectToVisualise.SetActive(false);

        Debug.Log("Corroutine RAN");
    }

    private void ShowReturnButtonOverlayOnClick()
    {
        if (Input.GetMouseButtonUp(0) && objectToVisualise.activeSelf == false )
        {
            StartCoroutine(ShowButtonForSomeTime());  
        }
        else if (objectToVisualise.activeSelf == true && Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            
            objectToVisualise.SetActive(false); 
        }
    }

    //private void ShowSettingsButtonOverlayOnTouch()
    //{
        
    //    if (Input.touchCount > 0 && objectToVisualise.activeSelf == false)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (Input.touchCount == 2)
    //        {
    //            touch = Input.GetTouch(1);

    //            if (touch.phase == TouchPhase.Began)
    //            {
                    
    //            }

    //            if (touch.phase == TouchPhase.Ended)
    //            {
    //                StartCoroutine(ShowButtonForSomeTime());
    //                Debug.Log("TOUCHED!!!");
    //            }
    //        }

            
    //    }
    //    else if (objectToVisualise.activeSelf == true && Input.touchCount > 0)
    //    {
    //        Touch touch = Input.GetTouch(0);

    //        if (Input.touchCount == 2)
    //        {
    //            touch = Input.GetTouch(1);

    //            if (touch.phase == TouchPhase.Began)
    //            {

    //            }

    //            if (touch.phase == TouchPhase.Ended)
    //            {
    //                StopAllCoroutines();
    //                objectToVisualise.SetActive(false);
    //            }
    //        }
            
            
    //    }

    //}

}
