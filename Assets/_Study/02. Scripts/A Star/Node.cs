using System;
using UnityEngine;

public class Node : IComparable<Node>
{// 비교하는 인터페이스
    public Node parent;
    public Vector3 pos;

    public float nodeTotalCost;
    public float estimateCost;

    public bool isObstacle;

    public Node(Vector3 pos)
    {
        this.pos = pos;
        parent = null;
        nodeTotalCost = 0f;
        estimateCost = 0f;
        isObstacle = false;
    }

    public void SetObstacle()
    {
        isObstacle = true;
    }

    public float GetFCost()
    {
        return nodeTotalCost + estimateCost;
    }

    public int CompareTo(Node other)
    {
        float myF = GetFCost();
        float otherF = other.GetFCost();

        if (myF < otherF)
            return -1;
        else if (myF > otherF)
            return 1;

        if (estimateCost < other.estimateCost)
            return -1;
        else if (estimateCost > other.estimateCost)
            return 1;

        return 0;
    }
}
