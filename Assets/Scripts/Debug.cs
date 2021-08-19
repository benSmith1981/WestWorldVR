using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
	public TextMeshPro m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<TextMeshPro>() ?? gameObject.AddComponent<TextMeshPro>();
        m_Text.autoSizeTextContainer = true;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
