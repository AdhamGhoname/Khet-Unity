using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boarde : MonoBehaviour
{
   // private Vector3 mouseOOfset = new Vector3(-5.0f, 0, -5.0f);

    private Board ins = new Board();

    private Vector2 mouse;
 

    float distance = 15;

    private void OnMouseDrag()
    {
        int xbefor = (int)Input.mousePosition.x;
        int ybefor = (int)Input.mousePosition.y;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
       
        transform.position = objectPos;
    }

  

}

