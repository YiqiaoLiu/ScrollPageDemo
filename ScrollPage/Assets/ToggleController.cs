using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ToggleController : MonoBehaviour {

    /// <summary>
    /// The ScrollController Script component
    /// </summary>
    public ScrollController scroll;

    /// <summary>
    /// The toggle prefab to instantiate
    /// </summary>
    public Toggle togglePrefab;

    /// <summary>
    /// The toggle group component
    /// </summary>
    public ToggleGroup toggleGroup;

    /// <summary>
    /// The toggle list need to be instantiated
    /// </summary>
    public List<Toggle> toggles = new List<Toggle>();

    /// <summary>
    /// When page number or current page index changed, need change the toggle group
    /// </summary>
    /// <param name="pageNumber">The total page number</param>
    /// <param name="currentIndex">Currnet page index</param>
    public void PageChanged(int pageNumber, int currentIndex) {

        //First check whether the page number is different of the toggle number
        if (pageNumber != toggles.Count) {

            //If the page number is larger than the toggle number, add toggles
            if (pageNumber > toggles.Count) {
                //Calculate how many toggle need to be instantiated
                int addToggleNumber = pageNumber - toggles.Count;
                for (int i = 0; i < addToggleNumber; i++) {
                    toggles.Add(CreateToggle());
                }
            }

            //If the page number is less than the toggle number, delete toggles
            if (pageNumber < toggles.Count) {
                while (pageNumber < toggles.Count) {
                    //Remove the last objecr in the toggle list
                    Toggle removeToggle = toggles[toggles.Count - 1];
                    toggles.Remove(removeToggle);

                    //After remove the object in the list, destroy the game object in the project
                    Destroy(removeToggle.gameObject);
                }
            }
        }

        //Set specific toggle true
        toggles[currentIndex].isOn = true;
    }

    /// <summary>
    /// The constructor of the toggle
    /// </summary>
    /// <returns></returns>
    private Toggle CreateToggle() {
        Toggle tempToggle;

        //Instantiate a new toggle
        tempToggle = Instantiate(togglePrefab);

        //Set the new instantiate game object true
        tempToggle.gameObject.SetActive(true);

        //Set the instantiate toggle inside the toggle group game object
        tempToggle.transform.SetParent(toggleGroup.transform);
        return tempToggle;
    }
}
