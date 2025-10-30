using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeSceneController : MonoBehaviour
{
    // ตัวแปรสำหรับปุ่มและกราฟิก
    public Button playButton;
    public Button soundButton;
    public Button tutorialButton;

    public Sprite play_press_graphic;
    public Sprite tutorial_press_graphic;
    public Sprite sound_on_graphic;
    public Sprite sound_mute_graphic;

    // ตัวแปรสำหรับจัดการเสียง
    private AudioSource gameSound;
    private bool isSoundOn = true;

    // ตัวแปรสำหรับกราฟิกที่เคลื่อนไหว
    public GameObject[] animatedElements;

    void Start()
    {
        // ค้นหา AudioSource ใน Scene
        gameSound = FindObjectOfType<AudioSource>();

        // กำหนดการทำงานของปุ่ม
        playButton.onClick.AddListener(OnPlayButtonClick);
        soundButton.onClick.AddListener(OnSoundButtonClick);
        tutorialButton.onClick.AddListener(OnTutorialButtonClick);
    }

    void Update()
    {
        // ทำให้ Element ด้านหลังเคลื่อนไหว
        foreach (GameObject element in animatedElements)
        {
            float newY = Mathf.Sin(Time.time) * 0.1f;
            element.transform.localPosition = new Vector3(element.transform.localPosition.x, element.transform.localPosition.y + newY, element.transform.localPosition.z);
        }
    }

    private void OnPlayButtonClick()
    {
        // เปลี่ยนกราฟิกและโหลดซีน
        playButton.GetComponent<Image>().sprite = play_press_graphic;
        SceneManager.LoadScene("TutorialScene");
    }

    private void OnSoundButtonClick()
    {
        if (gameSound == null)
        {
            return;
        }
        isSoundOn = !isSoundOn;
        if (isSoundOn)
        {
            gameSound.UnPause();
            soundButton.GetComponent<Image>().sprite = sound_on_graphic;
        }
        else
        {
            gameSound.Pause();
            soundButton.GetComponent<Image>().sprite = sound_mute_graphic;
        }
    }

    private void OnTutorialButtonClick()
    {
        // เปลี่ยนกราฟิกและโหลดซีน
        tutorialButton.GetComponent<Image>().sprite = tutorial_press_graphic;
        SceneManager.LoadScene("TutorialScene");
    }
}