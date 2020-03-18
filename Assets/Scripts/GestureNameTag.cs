using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestureNameTag : MonoBehaviour
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "Null";
    }

    public void UpdateText(string name)
    {
        text.text = name;
    }
}
