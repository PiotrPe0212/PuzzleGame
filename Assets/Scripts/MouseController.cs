using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject selectedObjectChildrenTarget;
    public GameObject selectedObjectChildrenSelect;
    Vector2 mousePosition;
    public Collider2D targetObject;
    private bool _puzzelTaken = false;
    private Vector3 _rotationStep;
    private Quaternion rotation;


    void Start()
    {
        Cursor.visible = true;
        _rotationStep = new Vector3(0, 0, 90);
        rotation = Quaternion.Euler(_rotationStep);
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!_puzzelTaken) targetObject = Physics2D.OverlapPoint(mousePosition);
        if (targetObject) PuzzleTargeting();
        else if (!targetObject && !Input.GetMouseButton(0)) PuzzleUntargeting();

        if (!selectedObject) return;
        if (selectedObject.transform.tag == "PuzzleDrop") return;

        if (Input.GetMouseButtonDown(0)) MouseInit();
        if (Input.GetMouseButton(0)) MousePressed();
        if (Input.GetMouseButtonUp(0)) MouseRelese();







    }


    private void PuzzleTargeting()
    {
        selectedObject = targetObject.transform.gameObject;

        if (!Input.GetMouseButton(0))
        {
            if (selectedObject.transform.tag == "PuzzleDrop") return;
            selectedObjectChildrenTarget = selectedObject.transform.GetChild(0).gameObject;
            selectedObjectChildrenTarget.SetActive(true);

        }

    }

    private void PuzzleUntargeting()
    {
        if (selectedObjectChildrenTarget) selectedObjectChildrenTarget.SetActive(false);

        selectedObjectChildrenTarget = null;
        selectedObjectChildrenSelect = null;
        selectedObject = null;

    }
    private void rotatingPuzzle(string direction)
    {
        if (direction == "right") _rotationStep += new Vector3(0, 0, 90);
        else _rotationStep -= new Vector3(0, 0, 90);
        rotation = Quaternion.Euler(_rotationStep);
        selectedObject.transform.rotation = rotation;
    }

    private void MouseInit()
    {
        selectedObjectChildrenSelect = selectedObject.transform.GetChild(1).gameObject;
        if (selectedObjectChildrenTarget) selectedObjectChildrenTarget.SetActive(false);
        selectedObjectChildrenSelect.SetActive(true);
        _puzzelTaken = true;
    }

    private void MousePressed()
    {
        selectedObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, 2);
        selectedObject.GetComponent<SpriteRenderer>().sortingOrder = 10;
        if (selectedObject.transform.tag == "Puzzle")
            selectedObject.transform.tag = "Untagged";
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotatingPuzzle("right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotatingPuzzle("left");
        }
    }
    private void MouseRelese()
    {
        if (selectedObject.transform.tag == "Untagged") selectedObject.transform.tag = "PuzzleDrop";
        selectedObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        selectedObject.transform.position += new Vector3(0, 0, -2);

        if (selectedObjectChildrenSelect)
            selectedObjectChildrenSelect.SetActive(false);
        if (selectedObjectChildrenTarget)
            selectedObjectChildrenTarget.SetActive(false);

        selectedObject = null;
        targetObject = null;
        _puzzelTaken = false;
    }

}
