using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    // [Header] และ [Tooltip] ช่วยให้การตั้งค่าใน Inspector ง่ายขึ้น
    [Header("UI Buttons")]
    public Button closeButton;
    public Button leftArrowButton;
    public Button rightArrowButton;

    [Header("Tutorial Pages")]
    public GameObject[] tutorialPages; 
    
    private int currentPageIndex = 0;
    private int totalPages = 0;

    void Start()
    {
        totalPages = tutorialPages.Length;
        
        if (totalPages == 0)
        {
            Debug.LogError("Tutorial Pages array is empty! Cannot run.");
            return;
        }

        // กำหนดการทำงานของปุ่ม
        closeButton.onClick.AddListener(OnCloseButtonClick);
        leftArrowButton.onClick.AddListener(OnLeftArrowButtonClick);
        rightArrowButton.onClick.AddListener(OnRightArrowButtonClick);

        // ตรวจสอบและบังคับเปิดทุกหน้าเพื่อให้สคริปต์ควบคุมได้
        ForceEnableAllPages();

        currentPageIndex = 0;
        ShowCurrentPage();
        // ไม่ต้องมี UpdateArrowButtons() ในโหมด Looping เพราะปุ่มจะเปิดตลอด
    }

    private AudioSource GetMusicPlayer()
    {
        // ค้นหา AudioSource ใน Scene (ซึ่งตอนนี้คือ MusicPlayer)
        return FindObjectOfType<AudioSource>();
    }

    // ฟังก์ชันต้องเป็น public เพื่อให้ Unity UI เรียกใช้ได้
    public void OnCloseButtonClick()
    {
        // เมื่อกลับไป HomeScene ให้แน่ใจว่า MusicPlayer ยังอยู่
        SceneManager.LoadScene("HomeScene");
    }

    // ฟังก์ชันต้องเป็น public: การกดซ้ายจะวนกลับไปหน้าสุดท้ายถ้าถึงหน้าแรก
    public void OnLeftArrowButtonClick()
    {
        // (3) -> 2 -> 1 -> (3)
        // ใช้ Modulo Arithmetic เพื่อวนกลับไปหน้าสุดท้ายเมื่อถึง Index 0
        currentPageIndex = (currentPageIndex - 1 + totalPages) % totalPages;
        
        ShowCurrentPage();
    }

    // ฟังก์ชันต้องเป็น public: การกดขวาจะวนกลับไปหน้าแรกถ้าถึงหน้าสุดท้าย
    public void OnRightArrowButtonClick()
    {
        // 1 -> 2 -> 3 -> (1)
        // ใช้ Modulo Arithmetic เพื่อวนกลับไปหน้าแรกเมื่อถึง Index สุดท้าย
        currentPageIndex = (currentPageIndex + 1) % totalPages;
        
        ShowCurrentPage();
    }

    private void ShowCurrentPage()
    {
        // 1. ปิดทุกหน้า
        for (int i = 0; i < totalPages; i++)
        {
            if (tutorialPages[i] != null)
            {
                tutorialPages[i].SetActive(false);
            }
        }
        
        // 2. เปิดเฉพาะหน้าปัจจุบัน
        if (tutorialPages[currentPageIndex] != null)
        {
            tutorialPages[currentPageIndex].SetActive(true);
        }
    }

    private void ForceEnableAllPages()
    {
        // ฟังก์ชันนี้บังคับเปิด Object ทุกหน้าใน Hierarchy
        for (int i = 0; i < totalPages; i++)
        {
            if (tutorialPages[i] != null)
            {
                tutorialPages[i].SetActive(true);
            }
        }
        // ในโหมด Looping เราไม่ต้องสนใจสถานะเริ่มต้นของ Page ใน Hierarchy แล้ว
    }

    void OnDestroy()
    {
        // ลบ Listener เพื่อป้องกัน Error
        if (closeButton != null) closeButton.onClick.RemoveListener(OnCloseButtonClick);
        if (leftArrowButton != null) leftArrowButton.onClick.RemoveListener(OnLeftArrowButtonClick);
        if (rightArrowButton != null) rightArrowButton.onClick.RemoveListener(OnRightArrowButtonClick);
    }
}