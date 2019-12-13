using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    bool MyTurn;
    bool selecting;
    public Material selectedMat;
    GameObject selected, lastSwapped, toBeSelected;
    Board board = new Board();
    public Transform topLeft, bottomLeft, topRight;
    Material originalMaterial;
    float width, height;
    int currX, currY;
    public LineRenderer laser;
    private void Start()
    {

        selected = null;
        width = Mathf.Abs(topLeft.position.x - topRight.position.x) / 10.0f;
        height = Mathf.Abs(topLeft.position.z - bottomLeft.position.z) / 8.0f;
        MyTurn = true;
        selecting = false;
    }

    private void Update()
    {
        lastSwapped = null;

        if (selecting)
        {
            currX = (int)(Mathf.Abs(toBeSelected.transform.position.x - topLeft.position.x) / width);
            currY = (int)(Mathf.Abs(toBeSelected.transform.position.z - topLeft.position.z) / height);
            if (MyTurn)
            {
                if (board[currX, currY].color == color.silver)
                {
                    selected = toBeSelected;
                    originalMaterial = toBeSelected.transform.GetChild(0).GetComponent<Renderer>().material;
                    Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
                    foreach (Renderer i in renderers)
                    {
                        if (i.gameObject.tag != "Mirror")
                        {
                            i.material = selectedMat;
                        }

                    }
                    MyTurn = !MyTurn;
                }
            }

            if (!MyTurn)
            {
                if (board[currX, currY].color == color.red)
                {
                    Debug.Log("Selected " + toBeSelected.name);
                    selected = toBeSelected;
                    originalMaterial = toBeSelected.transform.GetChild(0).GetComponent<Renderer>().material;
                    Renderer[] renderers = toBeSelected.GetComponentsInChildren<Renderer>();
                    foreach (var i in renderers)
                    {
                        if (i.gameObject.tag != "Mirror")
                            i.material = selectedMat;
                    }
                    MyTurn = !MyTurn;
                }
            }
            selecting = false;
        }
        else if (selected)
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
                Piece moved = board[currX, currY];
                bool canMove = board.MakeMove(currX, currY, newX, newY);
                if (canMove)
                {
                    Debug.Log(currX + " " + currY + " " + newX + " " + newY);
                    Vector3 oldPos = selected.transform.position;
                    float newPosX = topLeft.position.x + newX * width + 0.5f * width;
                    float newPosZ = topLeft.position.z - newY * height - 0.5f * width;

                    if (board[newX, newY].type != Type.empty)
                    {
                        Collider[] arr = Physics.OverlapSphere(new Vector3(newPosX, selected.transform.position.y, newPosZ), 0.01f);
                        if (arr.Length > 0)
                        {
                            foreach(Collider c in arr)
                            {
                                if (c.gameObject.tag != "Board")
                                {
                                    c.transform.position = new Vector3(selected.transform.position.x, c.transform.position.y, selected.transform.position.z);
                                    lastSwapped = c.gameObject;
                                    break;
                                }
                            }
                                  
                        }
                    }

                    selected.transform.position = new Vector3(newPosX, selected.transform.position.y, newPosZ);
           
                    Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
                    foreach (var i in renderers)
                    {
                        if (i.gameObject.tag != "Mirror")
                            i.material = originalMaterial;
                    }

                    if (moved.color == color.silver)
                    {
                        List<Tuple<int,int>> laserPoints = Laser.shoot_laser_path(ref board, (int)board[9, 7].rotation, 9, 7);
                        List<Vector3> laserPositions = new List<Vector3>();
                        laser.positionCount = laserPoints.Count;
                        foreach (var p in laserPoints)
                        {
                            Debug.Log("Laser: " + p.Item1 + " " + p.Item2);
                            float _x = topLeft.position.x + p.Item1 * width + 0.5f * width;
                            float _z = topLeft.position.z - p.Item2 * height - 0.5f * width;
                            laserPositions.Add(new Vector3(_z, -1.53f, _x));
                        }
                        float _X = topLeft.position.x + laserPoints[laserPoints.Count-1].Item1 * width + 0.5f * width;
                        float _Z = topLeft.position.z - laserPoints[laserPoints.Count - 1].Item2 * height - 0.5f * width;
                        Collider[] arr = Physics.OverlapSphere(new Vector3(_X, selected.transform.position.y, _Z), 0.01f);
                        if (arr.Length > 0 && board[laserPoints[laserPoints.Count - 1].Item1, laserPoints[laserPoints.Count - 1].Item2].type == Type.empty)
                        {
                            foreach (Collider c in arr)
                            {
                                if (c.gameObject.tag != "Board")
                                {
                                    Destroy(c.gameObject);
                                    break;
                                }
                            }
                        }
                        laserPositions.Reverse();
                        laser.SetPositions(laserPositions.ToArray());
                        //Debug.Log(board[laserPoints[laserPoints.Count - 1].Item1, laserPoints[laserPoints.Count - 1].Item2].type);
                    }
                    else if (moved.color == color.red)
                    {
                        List<Tuple<int, int>> laserPoints = Laser.shoot_laser_path(ref board, (int)board[0, 0].rotation, 0, 0);
                        List<Vector3> laserPositions = new List<Vector3>();
                        laser.positionCount = laserPoints.Count;
                        foreach (var p in laserPoints)
                        {
                            Debug.Log("Laser: " + p.Item1 + " " + p.Item2);
                            float _x = topLeft.position.x + p.Item1 * width + 0.5f * width;
                            float _z = topLeft.position.z - p.Item2 * height - 0.5f * width;
                            laserPositions.Add(new Vector3(_z, -1.53f, _x));
                        }
                        float _X = topLeft.position.x + laserPoints[laserPoints.Count - 1].Item1 * width + 0.5f * width;
                        float _Z = topLeft.position.z - laserPoints[laserPoints.Count - 1].Item2 * height - 0.5f * width;
                        Collider[] arr = Physics.OverlapSphere(new Vector3(_X, selected.transform.position.y, _Z), 0.01f);
                        if (arr.Length > 0 && board[laserPoints[laserPoints.Count - 1].Item1, laserPoints[laserPoints.Count - 1].Item2].type == Type.empty)
                        {
                            foreach (Collider c in arr)
                            {
                                if (c.gameObject.tag != "Board")
                                {
                                    Destroy(c.gameObject);
                                    break;
                                }
                            }
                        }
                        laserPositions.Reverse();
                        laser.SetPositions(laserPositions.ToArray());

                        //Debug.Log(board[laserPoints[laserPoints.Count - 1].Item1, laserPoints[laserPoints.Count - 1].Item2].type);
                    }
                    selected = null;
                }
                else
                {
                    Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
                    foreach (var i in renderers)
                    {
                        if (i.gameObject.tag != "Mirror")
                            i.material = originalMaterial;
                    }

                    selected = null;
                    MyTurn = !MyTurn;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                if (board.RotateLeft(currX, currY))
                {
                    selected.transform.Rotate(new Vector3(0, -90, 0));
                }
                else
                {
                    MyTurn = !MyTurn;
                }
                Debug.Log(board[currX, currY].rotation.ToString());

                Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
                foreach (var i in renderers)
                {
                    if (i.gameObject.tag != "Mirror")
                        i.material = originalMaterial;
                }

                selected = null;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (board.RotateRight(currX, currY))
                {
                    selected.transform.Rotate(new Vector3(0, 90, 0));
                }
                else
                {
                    MyTurn = !MyTurn;
                }
               
                Renderer[] renderers = selected.GetComponentsInChildren<Renderer>();
                foreach (var i in renderers)
                {
                    if (i.gameObject.tag != "Mirror")
                        i.material = originalMaterial;
                }
                selected = null;
            }
        }
    }


    public void SetSelected(GameObject s)
    {
        if (selected)
            return;
        if (s == lastSwapped)
            return;
        selecting = true;
        toBeSelected = s;
    }

}

