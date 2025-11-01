using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI akari;
    AkariManager AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = GameObject.Find("AkariObject").GetComponent<AkariManager>();
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextUpdate(){
        akari.text = "" + AM.akari;
    }
}
