using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    // --- UI Buttons ---
    [Header("UI Buttons")]
    public Button closeButton; 
    public Button startGameButton; 
    
    // --- Cosmetic Animation ---
    [Header("Cosmetic Animation")]
    public GameObject content1Card;
    public GameObject content2Card;
    public float scaleSpeed = 1f; 
    public float maxScale = 1.05f; 
    public float phaseOffset = 3.14f; // <-- NEW: ค่าชดเชยเฟส (3.14f คือครึ่งวงกลม หรือ 180 องศา)

    void Start()
    {
        // กำหนด Listener สำหรับปุ่ม
        closeButton.onClick.AddListener(OnCloseButtonClick); 
        startGameButton.onClick.AddListener(OnStartGameButtonClick); 
    }

    // --- Animation Logic (Content Card สลับขยับ) ---
    void Update()
    {
        // คำนวณค่า Scale พื้นฐาน (สำหรับ Content 1)
        float scaleOffset1 = Mathf.Sin(Time.time * scaleSpeed) * (maxScale - 1f);
        float currentScale1 = 1f + scaleOffset1;
        
        // คำนวณค่า Scale แบบชดเชยเฟส (สำหรับ Content 2)
        // การบวก 'phaseOffset' เข้าไปใน Time.time จะทำให้การเคลื่อนไหวสลับกัน
        float scaleOffset2 = Mathf.Sin(Time.time * scaleSpeed + phaseOffset) * (maxScale - 1f);
        float currentScale2 = 1f + scaleOffset2;

        // นำไปใช้กับ Content Card 1
        if (content1Card != null)
        {
            content1Card.transform.localScale = new Vector3(currentScale1, currentScale1, 1f);
        }
        
        // นำไปใช้กับ Content Card 2
        if (content2Card != null)
        {
            content2Card.transform.localScale = new Vector3(currentScale2, currentScale2, 1f);
        }
    }

    // --- Scene Transition Logic ---

    public void OnCloseButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void OnStartGameButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    void OnDestroy()
    {
        if (closeButton != null) closeButton.onClick.RemoveListener(OnCloseButtonClick);
        if (startGameButton != null) startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
    }
}