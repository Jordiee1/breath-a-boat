using UnityEngine;

public class MusicPlayerController : MonoBehaviour
{
    // ตัวแปรสาธารณะให้เข้าถึง Instance ได้ง่าย
    public static MusicPlayerController Instance { get; private set; } 
    
    private AudioSource audioSource;
    private bool isMusicPlaying = true; // สถานะเริ่มต้นคือเปิดเพลง

    void Awake()
    {
        // ... (โค้ดเดิมสำหรับ Singleton และ DontDestroyOnLoad)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>(); // เก็บ AudioSource ไว้
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันใหม่สำหรับปิด/เปิดเสียงเพลง (ใช้สำหรับปุ่ม)
    public void ToggleMusic()
    {
        if (audioSource == null) return;
        
        isMusicPlaying = !isMusicPlaying;

        if (isMusicPlaying)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Pause();
        }
    }
    
    // ฟังก์ชันนี้จะส่งสถานะปัจจุบันกลับไปให้ HomeSceneController ใช้ในการสลับกราฟิก
    public bool IsMusicPlaying()
    {
        return isMusicPlaying;
    }
}