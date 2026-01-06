using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private float HeuristicEstimateCost(Node currNode, Node endNode)
    {
        return (currNode.pos - endNode.pos).magnitude;
    }

    public List<Node> FindPath(Node startNode, Node endNode, GridManager gridManager)
    {
        gridManager.ResetNodes();

        var openList = new PriorityQueue(); // 탐색할 노드 리스트
        var closedList = new List<Node>(); // 탐색이 끝난 노드 리스트

        startNode.nodeTotalCost = 0f;
        startNode.estimateCost = HeuristicEstimateCost(startNode, endNode);
        startNode.parent = null;

        openList.Push(startNode);

        Node node = null;

        while (openList.Length != 0)
        {
            node = openList.First();
            openList.Remove(node);
            closedList.Add(node);

            if (node == endNode)
            {
                return CalculatePath(node);
            }

            List<Node> neighbors = new List<Node>();
            gridManager.GetNeighbors(node, neighbors);

            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighborNode = neighbors[i];

                if (closedList.Contains(neighborNode))
                    continue;

                float costToMove = (neighborNode.pos - node.pos).magnitude; // 목적지까지의 최단 거리
                float tentativeG = node.nodeTotalCost + costToMove; // 아직 결정하지 않은 추정 거리

                bool isInOpenList = openList.Contains(neighborNode);
                if (isInOpenList == false || tentativeG < neighborNode.nodeTotalCost)
                {
                    neighborNode.parent = node;
                    neighborNode.nodeTotalCost = tentativeG;
                    neighborNode.estimateCost = HeuristicEstimateCost(neighborNode, endNode);

                    if (isInOpenList == false)
                        openList.Push(neighborNode);
                }




            }
        }
        Debug.Log("Path not found");
        return null;

    }
    private List<Node> CalculatePath(Node node)
    {
        List<Node> list = new List<Node>();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}
