using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    [SerializeField] UnitController unitController;

    [SerializeField] Camera mainCamera;
    [SerializeField] LayerMask unitLayer;

    [SerializeField] LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    RaycastHit hit;
    Ray ray;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, unitLayer))
            {
                if (hit.collider == null) return;

                if(Input.GetKey(KeyCode.LeftShift)) 
                    unitController.ShiftClickSelectUnit(hit.transform.GetComponent<Unit>());
                else 
                    unitController.ClickSelectUnit(hit.transform.GetComponent<Unit>());

            }
            else
            {
                if (!Input.GetKeyDown(KeyCode.LeftShift))
                    unitController.DeSelectUnitAll();
            }

            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                if (hit.collider == null) return;

                unitController.PathFinding(hit.point);
                unitController.UnitMoveTo(hit.point);
            }
        }
    }
}
