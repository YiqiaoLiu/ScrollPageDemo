using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollController : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

    /// <summary>
    /// Store the last time the number of page
    /// </summary>
    private float lastTimePage;

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
    /// Store each page offset dynamically
    /// </summary>
    private List<float> pages = new List<float>();

    /// <summary>
    /// The next page position
    /// </summary>
    private float targetPos;

	// Use this for initialization
	void Start () {

        //Store the initial number of page
        lastTimePage = this.transform.GetChild(0).childCount;

        //The first calculate of the page offset
        for (float i = 0; i < lastTimePage; i++)
        {
            float temp = i / (lastTimePage - 1);
            pages.Add(temp);
        }

        //Get the ScrollRect Component
        rect = GetComponent<ScrollRect>();
	}
	
	// Update is called once per frame
	void Update () {

        //Calculate the page offset
        CalculatePageOffset();

        //When drag is finish, do the page change
        if (!isDrag) {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targetPos, Time.deltaTime * slideSpeed);
        }
    }

    /// <summary>
    /// Trigger when drag start
    /// </summary>
    /// <param name="data"></param>
    public void OnBeginDrag(PointerEventData data) {
        isDrag = true;
    }

    /// <summary>
    /// Trigger when drag ending
    /// </summary>
    /// <param name="data"></param>
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

    /// <summary>
    /// Calculate each page offset and store them in the page list
    /// </summary>
    private void CalculatePageOffset() {
        
        //Get the number of page
        int pageNumber = this.transform.GetChild(0).childCount;

        //If the page number does not change, just return
        if (pageNumber == lastTimePage) return;

        //If there isn't any page, return immediately
        if (pageNumber == 0) return;

        //Recalculate the offset of each page
        for (float i = 0; i < pageNumber; i++) {
            float temp = i / (pageNumber - 1);
            pages.Add(temp);
        }

    }
}
