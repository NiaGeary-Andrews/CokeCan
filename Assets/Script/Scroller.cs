using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private RawImage m_Image;
    [SerializeField] private float x, y; 
    void Update()
    {
        m_Image.uvRect = new Rect(m_Image.uvRect.position + new Vector2(x,y) * Time.deltaTime, m_Image.uvRect.size);
    }
}
