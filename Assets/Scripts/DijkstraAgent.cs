using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DijkstraAgent : IAgent
{
    public struct Node
    {
        public bool walkable;
        public Vector2 wordlPosition;
        public int cost;
    }

    public struct Grid
    {
        public Node[,] grid;
        public float nodeRadius;
        public LayerMask unWalkableMask;
        public int gridSizeX;
        public int gridSizeZ;
    }

    public MovementIntent Act(PacManGameState gs, int playerNumber)
    {
        int[,] grid;
        Grid g = MakeMapDiscrete(gs);

        var movementActionValues = (MovementIntent[])Enum.GetValues(typeof(MovementIntent));

        PacManGameState gsCopy = new PacManGameState(gs);
        if (gsCopy.GetGumStatus())
        {
            //Heuristique recompense quand il est au plus pres de la gum ball


            return (MovementIntent)movementActionValues.GetValue(1);
        }
        else
        {
            //Si on est le joueur 1
            if (playerNumber == 1)
            {
                //Heuristique recompence quand il est pres du joeur
                if (gs.GetP1Status())
                {
                    return (MovementIntent)movementActionValues.GetValue(1);
                }
                //Heuristique recompence quand il est loin du joueur
                else
                {
                    return (MovementIntent)movementActionValues.GetValue(1);
                }

            }
            else
            {
                if (gs.GetP2Status())
                {
                    return (MovementIntent)movementActionValues.GetValue(1);
                }
                else
                {
                    return (MovementIntent)movementActionValues.GetValue(1);
                }
            }
        }
    }

    public void Obs(float reward, bool terminal)
    {
        return;
    }

    public void D1(Vector2 startPos, Vector2 endPos, Grid g)
    {
        Node start = ConvertLocationToNode(startPos);
        Node end = ConvertLocationToNode(endPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(start);
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].cost < currentNode.cost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode.wordlPosition == end.wordlPosition)
            {
                return;
            }

            foreach (Node n in GetNeighbours(currentNode, g))
            {
                if (!n.walkable || closeSet.Contains(n))
                {
                    continue;
                }

                //int
            }

        }
    }

    public Grid MakeMapDiscrete(PacManGameState gs)
    {
        Grid grid = new Grid();
        grid.nodeRadius = 0.5f;
        grid.gridSizeX = gs.GetXSize();
        grid.gridSizeZ = gs.GetZSise();

        Node n = new Node();

        for (int i = 0; i < grid.gridSizeX; i++)
        {
            for (int j = 0; j < grid.gridSizeZ; j++)
            {
                bool walkable;
                if (gs.GetEtatCase()[i, j] == 1)
                {
                    walkable = false;
                }
                else
                {
                    walkable = true;
                }
                n.walkable = walkable;
                n.wordlPosition = new Vector2(i, j);

                grid.grid[i, j] = n;

            }
        }
        return grid;
    }

    public Node ConvertLocationToNode(Vector2 location)
    {
        Node n = new Node();
        n.walkable = true;
        n.wordlPosition = new Vector2(Mathf.RoundToInt(location.x), Mathf.RoundToInt(location.y));
        return n;
    }

    public List<Node> GetNeighbours(Node node, Grid g)
    {
        List<Node> neigbours = new List<Node>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                //Noeud sur lequel on se trouve
                if (i == 0 && j == 0)
                {
                    continue;
                }

                int x = Mathf.RoundToInt(node.wordlPosition.x);
                int z = Mathf.RoundToInt(node.wordlPosition.y);

                if (x >= 0 && x < g.gridSizeX && z >= 0 && z < g.gridSizeZ)
                {
                    neigbours.Add(g.grid[x, z]);
                }
            }
        }
        return neigbours;
    }

    public int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.RoundToInt(Math.Abs(a.wordlPosition.x - b.wordlPosition.x));
        int dstY = Mathf.RoundToInt(Math.Abs(a.wordlPosition.y - b.wordlPosition.y));

        if (dstX > dstY)
        {
            return 2 * dstY + (dstX - dstY);
        }
        else
        {
            return 2 * dstX + (dstY - dstX);
        }
    }

}
