using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public float tutorialTime;
    public int day;
    public Text dayText;
    public GameObject mission1Panel;
    public GameObject playerDay1;


    private void Start() {
        switch (day) {
            case 1:
                StartGameDayOne();
                break;
            case 2:
                StartGameDayTwo();
                break;
            case 3:
                StartGameDayThree();
                break;
        }
    }

    public void StartGameDayOne() {
        StartCoroutine(Tutorial());
        dayText.text = "Day 1";
    }

    public void StartGameDayTwo() {
        StartCoroutine(NoTutorial());
        dayText.text = "Day 2";
    }

    public void StartGameDayThree() {
        StartCoroutine(NoTutorial());
        dayText.text = "Day 3";
    }

    public IEnumerator Tutorial() {
        tutorialPanel.SetActive(true);
        yield return new WaitForSeconds(tutorialTime);
        tutorialPanel.SetActive(false);

        switch (day) {
            case 1:
                mission1Panel.SetActive(true);
                playerDay1.GetComponent<PlayerController>().canMove = true;
                break;
        }
    }

    public IEnumerator NoTutorial() {
        yield return new WaitForSeconds(tutorialTime);
        mission1Panel.SetActive(true);
        playerDay1.GetComponent<PlayerController>().canMove = true;

    }


}
