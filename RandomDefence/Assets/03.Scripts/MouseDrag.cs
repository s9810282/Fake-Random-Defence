using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseDrag : MonoBehaviour
{
    [SerializeField] RectTransform dragRectangle;

    [SerializeField] Camera mainCamera;
    [SerializeField] UnitController unitController;

    Rect rect;
    Vector2 start;
    Vector2 end;

    bool isDown = false;

    // Start is called before the first frame update
    void Start()
    {
        start = Vector2.zero;
        end = Vector2.zero;

        isDown = false;

        DrawDragRectangle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isDown = true;

            start = Input.mousePosition;
            rect = new Rect();
        }

        if (!isDown)
            return;

        if(Input.GetMouseButton(0))
        {
            end = Input.mousePosition;
            DrawDragRectangle();
        }

        if(Input.GetMouseButtonUp(0))
        {
            isDown = false;

            CalcurateDragRect();
            SelectUnit();

            start = end = Vector2.zero;
            DrawDragRectangle();
        }

    }

    public void DrawDragRectangle()
    {
        dragRectangle.position = (start + end) * 0.5f;
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }

    public void CalcurateDragRect()
    {
        if(Input.mousePosition.x < start.x)
        {
            rect.xMin = Input.mousePosition.x;
            rect.xMax = start.x;
        }
        else
        {
            rect.xMin = start.x;
            rect.xMax = Input.mousePosition.x;
        }
        if (Input.mousePosition.y < start.y)
        {
            rect.yMin = Input.mousePosition.y;
            rect.yMax = start.y;
        }
        else
        {
            rect.yMin = start.y;
            rect.yMax = Input.mousePosition.y;
        }
    }

    public void SelectUnit()
    {
        List<Unit> unitList = unitController.ReturnUnitList();

        for (int i = 0; i < unitList.Count; i++)
        {
            Unit unit = unitList[i];

            if (unit == null) continue;

            if (rect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
            {
                unitController.DragSelect(unit);
            }
        }
    }
}
