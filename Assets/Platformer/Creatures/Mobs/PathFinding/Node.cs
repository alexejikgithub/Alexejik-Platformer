using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Creatures.Mobs.PathFinding

{
    public class Node 
    {
        public bool Walkable;
        public Vector3 WorldPosition;

        public int GridX;
        public int GridY;

        public Node Parent;

        public int GCost;
        public int HCost;
        public int FCost
        {
            get { return GCost + HCost; }
        }


        public Node (bool walkable, Vector3 worldPos, int gridX, int gridY)
		{
            Walkable = walkable;
            WorldPosition = worldPos;
            GridX = gridX;
            GridY = gridY;

        }

        
    }
}
