using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollRainbow : MonoBehaviour
{
    float offset = 0;
    float speed = 1;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.fontMaterial.SetTextureOffset("_FaceTex", new Vector2(offset, 1));
        offset = (offset + speed * Time.deltaTime) % 1.0f;
    }
}
