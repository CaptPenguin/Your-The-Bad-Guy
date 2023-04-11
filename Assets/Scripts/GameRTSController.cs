using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRTSController : MonoBehaviour
{
    [SerializeField] private Transform selectionAreaTransform;
    private Vector3 startPosition;
    private List<UnitRTS> selectedUnitRTSList;

    private void Awake()
    {
        selectedUnitRTSList = new List<UnitRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))         //left mouse pressed
        {
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = getMWP();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = getMWP();                      //draws selection box
            Vector3 lowerLeft = new Vector3(
                Mathf.Min(startPosition.x, currentMousePosition.x),
                Mathf.Min(startPosition.y, currentMousePosition.y));
            Vector3 upperRight = new Vector3(
                Mathf.Max(startPosition.x, currentMousePosition.x),
                Mathf.Max(startPosition.y, currentMousePosition.y));
            selectionAreaTransform.position = lowerLeft;
            selectionAreaTransform.localScale = upperRight - lowerLeft;
        }

        if (Input.GetMouseButtonUp(0))          //left mouse released
        {
            selectionAreaTransform.gameObject.SetActive(false);
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, getMWP());

            foreach(UnitRTS unitRTS in selectedUnitRTSList)     //deselect all units
            {
                unitRTS.SetSelectedVisible(false);
            }
            selectedUnitRTSList.Clear();

            foreach (Collider2D collider2D in collider2DArray)      //select units within area
            {
                UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();
                if(unitRTS != null)
                {
                    unitRTS.SetSelectedVisible(true);
                    selectedUnitRTSList.Add(unitRTS);             //adds units within box into the selectedUnits array
                }
            }
            Debug.Log(selectedUnitRTSList.Count);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 moveToPosition = getMWP();

            List<Vector3> targetPositionList = GetPositionListAround(moveToPosition, new float[] { 5f, 10f, 15f }, new int[] { 5, 10, 20 });

            int targetPositionListIndex = 0;

            foreach (UnitRTS unitRTS in selectedUnitRTSList)
            {
                unitRTS.MoveTo(targetPositionList[targetPositionListIndex]);
                targetPositionListIndex = (targetPositionListIndex + 1) % targetPositionList.Count;
            }
        }
    }

    public List<Vector3> GetPositionListAround(Vector3 startPosition, float[] ringDistanceArray, int[] ringPositionCountArray)
    {
        List<Vector3> positionList = new List<Vector3>();
        positionList.Add(startPosition);
        for(int i = 0; i < ringDistanceArray.Length; i++)
        {
           positionList.AddRange(GetPositionListAround(startPosition, ringDistanceArray[i], ringPositionCountArray[i]));
        }
        return positionList;
    }

    public List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
    {
        List<Vector3> positionList = new List<Vector3>();
        for(int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPosition + dir * distance;
            positionList.Add(position);
        }
        return positionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vec, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vec;
    }

    public Vector3 getMWP()       //gets mouse position in reference to world
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
}