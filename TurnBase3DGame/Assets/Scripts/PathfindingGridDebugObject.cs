using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PathfindingGridDebugObject : GridDebugObject
{
    [SerializeField] private TextMeshPro _gcostText;
    [SerializeField] private TextMeshPro _hcostText;
    [SerializeField] private TextMeshPro _fcostText;

    private PathNode _pathNode;
    public override void SetGridObject(object gridObject)
    {
        _pathNode = (PathNode)gridObject;
        base.SetGridObject(gridObject);
    }

    protected override void Update()
    {
        base.Update();
        _gcostText.text = _pathNode.GetGCost().ToString();
        _fcostText.text = _pathNode.GetFCost().ToString();
        _hcostText.text = _pathNode.GetHCost().ToString();
    }
}
