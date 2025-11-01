using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultScriptSample : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button targetButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGameScene(){
        SceneManager.LoadScene("TitleScene");
    }

    public void OnPointerEnter(PointerEventData eventData){
        Debug.Log("乗ってる");
        /*
        ColorBlock colorBlock = targetButton.colors;
        colorBlock.normalColor = new Color(255,0,0,0.5f);
        targetButton.colors = colorBlock;
        */
    }
    public void OnPointerExit(PointerEventData eventData){
        Debug.Log("乗ってない");
        /*
        ColorBlock colorBlock = targetButton.colors;
        colorBlock.normalColor = new Color(0,0,0,0);
        targetButton.colors = colorBlock;
        */
    }
}