using UnityEngine;

public class MusicPlayerController : MonoBehaviour
{
    private static MusicPlayerController instance = null;

    void Awake()
    {
        // ตรวจสอบว่ามี Object นี้อยู่แล้วหรือไม่ (เพื่อป้องกันเพลงซ้อนกัน)
        if (instance == null)
        {
            // ถ้ายังไม่มี ให้กำหนดให้ตัวเองเป็น instance แรก
            instance = this;
            // และสั่งให้ Object นี้ไม่ถูกทำลายเมื่อโหลดซีนใหม่
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // ถ้ามี Object นี้อยู่แล้ว (เช่น มาจาก HomeScene เก่า) ให้ทำลายตัวเองทันที
            Destroy(gameObject);
        }
    }
}