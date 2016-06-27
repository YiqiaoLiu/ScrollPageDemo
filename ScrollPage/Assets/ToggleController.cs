using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ToggleController : MonoBehaviour {

    public ScrollController scroll;
    public Toggle togglePrefab;
    public ToggleGroup toggleGroup;

    public List<Toggle> toggles = new List<Toggle>();

    public void PageChanged(int pageNumber, int currentIndex) {
        if (pageNumber != toggles.Count) {
            if (pageNumber > toggles.Count) {
                int addToggleNumber = pageNumber - toggles.Count;
                for (int i = 0; i < addToggleNumber; i++) {
                    toggles.Add(CreateToggle());
                }
            }
            if (pageNumber < toggles.Count) {
                while (pageNumber < toggles.Count) {
                    Toggle removeToggle = toggles[toggles.Count - 1];
                    toggles.Remove(removeToggle);
                    Destroy(removeToggle.gameObject);
                }
            }
        }

        toggles[currentIndex].isOn = true;
    }

    private Toggle CreateToggle() {
        Toggle tempToggle;
        tempToggle = Instantiate(togglePrefab);
        tempToggle.gameObject.SetActive(true);
        tempToggle.transform.SetParent(toggleGroup.transform);
        return tempToggle;
    }
}
