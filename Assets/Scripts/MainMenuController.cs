using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Animator[] textAnim;
    public GameObject creditsCanvas;
    public GameObject mainCanvas;
    public GameObject day1Canvas;
    public int number = 0;
    public GameObject day2Canvas;
    public GameObject day3Canvas;


    private void Start() {
        if (number != 0) {
            switch (number) {
                case 2:
                    day2Canvas.SetActive(true);
                    break;
                case 3:
                    day3Canvas.SetActive(true);
                    break;
            }
        }
    }

    public void TextAnimationEnter (int number) {
        textAnim[number].SetBool("Animate", true);
    }

    public void TextAnimationExit(int number) {
        textAnim[number].SetBool("Animate", false);
    }

    public void Credits() {
        creditsCanvas.SetActive(true);
        creditsCanvas.GetComponent<Animator>().SetBool("Back", false);
    }

    public void GoBack() {
        creditsCanvas.GetComponent<Animator>().SetBool("Back", true);
    }

    public void StartGame() {
        mainCanvas.GetComponent<Animator>().SetBool("Start", true);
        day1Canvas.SetActive(true);
    }

    public void StartGame(int number) {
        mainCanvas.GetComponent<Animator>().SetBool("Start", true);
        switch (number) {
            case 1:
                day1Canvas.SetActive(true);
                break;
        }
    }

    public void ToDay1() {
        SceneManager.LoadScene("GameLevel", LoadSceneMode.Single);
    }

    public void ToDay2() {
        SceneManager.LoadScene("GameLevel02", LoadSceneMode.Single);
    }

    public void ToDay301() {
        SceneManager.LoadScene("GameLevel03", LoadSceneMode.Single);
    }

    public void ToDay302() {
        SceneManager.LoadScene("GameLevel04", LoadSceneMode.Single);
    }

    public void Finally() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Exit() {
	Application.Quit();
    }

}
