using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    bool MyTurn;
    public Material materialSliver;
    public Material materialRed;
    GameObject selected;
    Board board = new Board();
    public Transform topLeft, bottomLeft, topRight;
    float width, height;
    int currX, currY;
    private void Start()
    {

        selected = null;
        width = Mathf.Abs(topLeft.position.x - topRight.position.x) / 10.0f;
        height = Mathf.Abs(topLeft.position.z - bottomLeft.position.z) / 8.0f;
        MyTurn = false;
    }

    private void Update()
    {

        if (selected)
        {
           
            if (Input.GetMouseButtonDown(0))
            {
               
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    point = hit.point;
                }
                else
                {
                    return;
                }
                Debug.Log(Input.mousePosition);
                int newX = (int)(Mathf.Abs(point.x - topLeft.position.x) / width);
                int newY = (int)(Mathf.Abs(point.z - topLeft.position.z) / height);

                Debug.Log(newX + " " + newY);

                if (board.MakeMove(currX, currY, newX, newY))
                {
                    float newPosX = topLeft.position.x + newX * width + 0.5f * width;
                    float newPosZ = topLeft.position.z - newY * height - 0.5f * width;
                    selected.transform.position = new Vector3(newPosX, selected.transform.position.y, newPosZ);
                    selected = null;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                board.RotateLeft(currX, currY);
                Debug.Log(board[currX, currY].rotation.ToString());
                selected.transform.Rotate(new Vector3(0, -90, 0));
                selected = null;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                board.RotateRight(currX, currY);
                Debug.Log(board[currX, currY].rotation.ToString());
                selected.transform.Rotate(new Vector3(0, 90, 0));
                selected = null;
            }
        }
    }


    public void SetSelected(GameObject s)
    {
       
        currX = (int)(Mathf.Abs(s.transform.position.x - topLeft.position.x) / width);
        currY = (int)(Mathf.Abs(s.transform.position.z - topLeft.position.z) / height);

        if (MyTurn)
        {
            if (board[currX, currY].color.ToString() == "silver")
            {
                selected = s;
                s.GetComponent<Renderer>().material = materialSliver;
                MyTurn = !MyTurn;
            }
        }

        if (!MyTurn)
        {
            if (board[currX, currY].color == color.red)
            {
                Debug.Log("Selected " + s.name);
                selected = s;
                Renderer[] renderers = s.GetComponentsInChildren<Renderer>();
                foreach (var i in renderers)
                {
                    i.material = materialRed;
                }
                //MyTurn = !MyTurn;
            }
        }
       
       
       

    }

}

