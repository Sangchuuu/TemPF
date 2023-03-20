using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour
{    
    public void Button1()
    {
        GameManager.instance.clickedBt1 = true;
    }


    public void Button2()
    {
        GameManager.instance.clickedBt2 = true;
    }
}
