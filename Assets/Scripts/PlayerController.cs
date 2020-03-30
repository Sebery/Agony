using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    private Animator anim;
    private Rigidbody2D rbPlayer;
    public float speed;
    private float xPos;
    private float yPos;
    private bool moving;
    private float lastMoveX;
    private float lastMoveY;
    public CinemachineVirtualCamera virtualCamera;
    public bool zoomIn = false;
    public float zoomInTime;
    public float zoomOutTime;
    public bool canMove = false;
    public GameObject buyHotdogText;
    public GameObject hotDogImage;
    public GameObject itemBg;
    public GameObject mission1Text01;
    public GameObject mission1Text02;
    public GameObject mission1Panel;
    public GameObject giveHotdogText;
    public int phaseOrMissionNumber = 0;
    public GameObject bushPlanted;
    public bool buyPlant = false;
    public GameObject death;



    private void Awake() {
        rbPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if (canMove) {
            PlayerMovement();
        } else {
            rbPlayer.velocity = new Vector2(xPos, yPos) * 0;
            lastMoveX = 1;
            anim.SetFloat("LastMoveX", lastMoveX);
            anim.SetBool("Moving", false);
        }

        CameraZoomOut(phaseOrMissionNumber);
    }

    public void PlayerMovement() {
        xPos = Input.GetAxisRaw("Horizontal");
        yPos = Input.GetAxisRaw("Vertical");

        anim.SetFloat("xPos", xPos);

        if (xPos != 0 || yPos != 0) {
            if (!moving) {
                moving = true;
                anim.SetBool("Moving", moving);
                lastMoveX = xPos;
                lastMoveY = yPos;
                anim.SetFloat("LastMoveX", lastMoveX);
            }

        } else {
            if (moving) {
                moving = false;
                anim.SetBool("Moving", moving);
            }

        }

        rbPlayer.velocity = new Vector2(xPos, yPos) * speed;
    }

    public void CameraZoomIn() {
        if (!zoomIn) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1f, zoomInTime);
        }
    }

    public void CameraZoomOut(int number) {
        if (Input.GetKey(KeyCode.E) && zoomIn) {
            switch (number) {
                case 1:
                    virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5f, zoomOutTime);
                    canMove = true;
                    zoomIn = false;
                    itemBg.SetActive(true);
                    mission1Panel.SetActive(false);
                    mission1Panel.SetActive(true);
                    mission1Text02.SetActive(true);
                    mission1Text01.SetActive(false);
                    hotDogImage.SetActive(true);
                    buyHotdogText.SetActive(false);
                    buyPlant = true;
                    break;
                case 2:
                    mission1Panel.SetActive(false);
                    giveHotdogText.GetComponent<Text>().text = "Mission Completed!";
                    hotDogImage.SetActive(false);
                    itemBg.SetActive(false);
                    StartCoroutine(ToDay2());
                    break;
                case 3:
                    mission1Panel.SetActive(false);
                    bushPlanted.SetActive(true);
                    giveHotdogText.GetComponent<Text>().text = "Mission Completed!";
                    hotDogImage.SetActive(false);
                    itemBg.SetActive(false);
                    StartCoroutine(ToDay3());
                    break;
                case 4:
                    mission1Panel.SetActive(false);
                    giveHotdogText.GetComponent<Text>().text = "Mission Completed!";
                    hotDogImage.SetActive(false);
                    itemBg.SetActive(false);
                    StartCoroutine(Final());
                    break;
                case 5:
                    GetComponent<SpriteRenderer>().enabled = false;
                    death.SetActive(true);
                    giveHotdogText.GetComponent<Text>().text = "You won't woke up again!";
                    StartCoroutine(Final());
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Mission1")) {
            CameraZoomIn();
            zoomIn = true;
            canMove = false;
            Debug.Log("Mission2");
            buyHotdogText.SetActive(true);
            phaseOrMissionNumber = 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Mission2")) {
            CameraZoomIn();
            zoomIn = true;
            canMove = false;
            Debug.Log("Mission2");
            buyHotdogText.SetActive(true);
            phaseOrMissionNumber = 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Mission3")) {
            CameraZoomIn();
            zoomIn = true;
            canMove = false;
            Debug.Log("Mission2");
            buyHotdogText.SetActive(true);
            phaseOrMissionNumber = 1;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Homeless")) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1f, zoomInTime);
            canMove = false;
            giveHotdogText.SetActive(true);
            phaseOrMissionNumber = 2;
            zoomIn = true;
        }

        if (collision.CompareTag("PlantPlace") && buyPlant) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1f, zoomInTime);
            canMove = false;
            giveHotdogText.SetActive(true);
            phaseOrMissionNumber = 3;
            zoomIn = true;
        }

        if (collision.CompareTag("Trash")) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1f, zoomInTime);
            canMove = false;
            giveHotdogText.SetActive(true);
            phaseOrMissionNumber = 4;
            zoomIn = true;
        }

        if (collision.CompareTag("Mission4")) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 1f, zoomInTime);
            canMove = false;
            phaseOrMissionNumber = 5;
            giveHotdogText.SetActive(true);
            zoomIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Homeless")) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5f, zoomOutTime);
            giveHotdogText.SetActive(false);
            zoomIn = false;
        }

        if (collision.CompareTag("PlantPlace") && buyPlant) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5f, zoomOutTime);
            giveHotdogText.SetActive(false);
            zoomIn = false;
        }

        if (collision.CompareTag("Trash")) {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, 5f, zoomOutTime);
            giveHotdogText.SetActive(false);
            zoomIn = false;
        }
    }

    public IEnumerator ToDay2() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu02", LoadSceneMode.Single);
    }

    public IEnumerator ToDay3() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu03", LoadSceneMode.Single);
    }

    public IEnumerator Final() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
    }


}
