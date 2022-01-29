using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySubtitleTag : MonoBehaviour{
    private TextMeshProUGUI text;

    public void Start(){
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetSubTitle(string subtitle){
        text.text = subtitle;
    }

    public void ForceStart(){
        Start();
    }
}
