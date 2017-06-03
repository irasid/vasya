using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour {
    public Sprite layer_blue, layer_red;
//public string action;
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = layer_red;
    }
    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = layer_blue;
    }

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
            
        {
            case "play":
                Application.LoadLevel("igra");
                break;

            case "menu":
                Application.LoadLevel("menu");
                break;
                
                     case "faktRK":
                Application.LoadLevel("RK");
                break;

                     case "faktTS":
                Application.LoadLevel("TS");
                break;

            case "faktTM":
                Application.LoadLevel("TM");
                break;

            case "faktIP":
                Application.LoadLevel("IP");
                break;
        }
    }
}
