using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Node[,] nodes;

    private Vector3 origin;

    public int rows = 10;
    public int cols = 10;
    public float cellSize = 1f;

    void Awake()
    {
        origin = transform.position;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        CalculateObstacles();
    }

    private void CalculateObstacles()
    {
        nodes = new Node[rows, cols];
        int index = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var cellPos = GetGridCellCenter(index);
                Node node = new Node(cellPos);
                nodes[i, j] = node;
                index++;
            }
        }

        if (obstacles != null && obstacles.Length > 0) // Grid 위에 장애물 오브젝트가 있을 경우 해당 노드를 장애물로 설정
        {
            foreach (var obstacle in obstacles)
            {
                int indexCell = GetGridIndex(obstacle.transform.position);
                if (indexCell == -1)
                    continue;

                int row = GetRow(indexCell);
                int col = GetColumn(indexCell);
                nodes[row, col].SetObstacle();
            }
        }
    }

    private Vector3 GetGridCellCenter(int index) // Grid의 중간 위치 확인용
    {
        var cellPos = GetGridCellPosition(index);
        cellPos.x += cellSize * 0.5f;
        cellPos.z += cellSize * 0.5f;

        return cellPos;
    }

    private Vector3 GetGridCellPosition(int index) // Grid의 위치 확인용
    {
        int row = GetRow(index);
        int col = GetColumn(index);

        float posX = col * cellSize;
        float posZ = row * cellSize;

        return origin + new Vector3(posX, 0, posZ);

    }
    public int GetGridIndex(Vector3 pos) // 특정 위치를 넣으면 몇 번째 노드인지 반환
    {
        if (isInBounds(pos) == false)
            return -1;

        Vector3 localPos = pos - origin;

        int col = (int)(localPos.x / cellSize);
        int row = (int)(localPos.z / cellSize);

        return row * cols + col;
    }

    public bool isInBounds(Vector3 pos) // 특정 위치가 Grid 내에 있는지 확인
    {
        float width = cols * cellSize;
        float height = rows * cellSize;

        return pos.x >= origin.x && pos.x < origin.x + width &&
                pos.z >= origin.z && pos.z < origin.z + height;
    }

    public int GetRow(int index)
    {
        return index / cols;
    }

    public int GetColumn(int index)
    {
        return index % cols;
    }

    public void GetNeighbors(Node node, List<Node> neighbors) // 현재 위치에서 주변 노드 검색
    {
        int nodeIndex = GetGridIndex(node.pos);
        if (nodeIndex == -1)
            return;
        int row = GetRow(nodeIndex);
        int col = GetColumn(nodeIndex);

        AssignNeighbors(row - 1, col, neighbors); // 위
        AssignNeighbors(row + 1, col, neighbors); // 아래
        AssignNeighbors(row, col - 1, neighbors); // 왼쪽
        AssignNeighbors(row, col + 1, neighbors); // 오른쪽
    }

    public void AssignNeighbors(int row, int col, List<Node> neighbors) // 특정 노드의 주변 노드 검색
    {
        if (row >= 0 && row < rows && col >= 0 && col < cols)
        {
            Node nodeToAdd = nodes[row, col];

            if (!nodeToAdd.isObstacle)
                neighbors.Add(nodeToAdd);
        }
    }

    public void ResetNodes() // 노드 초기화
    {
        foreach (var node in nodes)
        {
            node.parent = null;
            node.nodeTotalCost = 0f;
            node.estimateCost = 0f;
        }
    }

    void OnDrawGizmos()
    {
        if (rows <= 0 || cols <= 0 || cellSize <= 0f)
            return;

        Gizmos.color = Color.white;

        float width = cols * cellSize;
        float height = rows * cellSize;

        for (int i = 0; i <= rows; i++)
        {
            var startPos = transform.position + i * cellSize * Vector3.forward;
            var endPos = startPos + width * cellSize * Vector3.right;
            Gizmos.DrawLine(startPos, endPos);
        }

        for (int j = 0; j <= cols; j++)
        {
            var startPos = transform.position + j * cellSize * Vector3.right;
            var endPos = startPos + height * cellSize * Vector3.forward;
            Gizmos.DrawLine(startPos, endPos);
        }

    }
}
