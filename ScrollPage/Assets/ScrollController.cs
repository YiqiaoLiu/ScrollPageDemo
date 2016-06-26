﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollController : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

    private int pageCount = 0;

    /// <summary>
    /// The page-changing speed
    /// </summary>
    private float slideSpeed = 5f;

    /// <summary>
    /// The ScrollRect component
    /// </summary>
    private ScrollRect rect;

    /// <summary>
    /// To check whether the drag is finish
    /// </summary>
    private bool isDrag = false;

    /// <summary>
    /// Store each page offset (now it is static)
    /// </summary>
    private float[] pages = { 0, 0.25f, 0.5f, 0.75f, 1f };

    /// <summary>
    /// The next page position
    /// </summary>
    private float targetPos;

	// Use this for initialization
	void Start () {
        pageCount = this.transform.childCount;

        //Get the ScrollRect Component
        rect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDrag) {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targetPos, Time.deltaTime * slideSpeed);
            Debug.Log("check: " + Time.deltaTime * slideSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData data) {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData data) {
        isDrag = false;

        //Get the current scroll page offset
        float posHorizontal = rect.horizontalNormalizedPosition;

        //This is the check page index
        int pageIndex = 0;

        //Caculating the offset between the page 0 and the current pos
        float dragOffset = Mathf.Abs(pages[pageIndex] - posHorizontal);

        //Caculating the offset between each page and the current pos
        //Chosing the closest page as the next frame changing page
        for (int i = 1; i < 5; i++) {
            float tempOffset = Mathf.Abs(pages[i] - posHorizontal);
            if (tempOffset < dragOffset) {
                dragOffset = tempOffset;
                pageIndex = i;
            }
        }

        //Store the calculated changing page position
        targetPos = pages[pageIndex];

    }
}
