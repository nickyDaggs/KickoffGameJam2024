using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureScroll : MonoBehaviour
{
    public float scrollX;
    public float scrollY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * scrollX;
        float offsetY = Time.time * scrollY;
        GetComponent<RawImage>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
