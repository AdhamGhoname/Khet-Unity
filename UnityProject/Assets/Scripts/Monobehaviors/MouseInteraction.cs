using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    GameObject previousHit;

    

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool found = Physics.Raycast(ray, out hit);
        if (found && hit.collider.gameObject.GetComponent<Interactive>())
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit.collider.gameObject.GetComponent<Interactive>().OnPress.Invoke();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                hit.collider.gameObject.GetComponent<Interactive>().OnRelease.Invoke();
            }
            else
            {
                hit.collider.gameObject.GetComponent<Interactive>().OnEnter.Invoke();
            }
            previousHit = hit.collider.gameObject;
        }
        else
        {
            if (previousHit)
            {
                previousHit.GetComponent<Interactive>().OnExit.Invoke();
                previousHit = null;
            }
        }
    }
}
