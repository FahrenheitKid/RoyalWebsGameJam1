using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [SerializeField]
    public FitType fitType;
    [SerializeField]
    public int rows;
    [SerializeField]
    public int columns;
    [SerializeField]
    public Vector2 cellSize;
    [SerializeField]
    public Vector2 spacing;
    [SerializeField]
    public bool fitX;
    [SerializeField]
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * (columns - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

        }

    }

    public GameObject GetElement(int row, int column)
    {
        int columnCount = 0;
        int rowCount = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            if (row == rowCount && columnCount == column)
            {
                return rectChildren[i].gameObject;
            }
        }

        return null;
    }

    public bool IsAtEdge(Transform element, UtilityTools.Directions dir)
    {
        if (element.IsChildOf(transform) == false)return false;

        int columnCount = 0;
        int rowCount = 0;
        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;
            if (element.gameObject == rectChildren[i].gameObject)
            {
                if (dir == UtilityTools.Directions.left && columnCount == 0)
                {
                    return true;
                }
                else if (dir == UtilityTools.Directions.right && columnCount == columns)
                { 
                    return true;
                }
                else if (dir == UtilityTools.Directions.up && rowCount == 0)
                { 
                    return true;
                }
                else if (dir == UtilityTools.Directions.down && rowCount == rows)
                { 
                    return true;
                }

            }

        }

        return false;
    }
    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }

}