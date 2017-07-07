using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Grids2;

//some basic grids knowledge
//tilecells are the gameobjects that have a spritecell component attached
//spritecell extends from tilecell

public class GridBehaviorTest : GridBehaviour<GridPoint2, TileCell>
{

    public void TestMethod()
    {

        //TileCell cell = Grid [new GridPoint2(2, 3)]; This gives the individual tile gameobject at 2,3
        //TileCell cell = (SpriteCell) Grid [new GridPoint2(2, 3)]; //this gives access to the spritecell at that gameobject. Specifically, sprite and color 
        //MyCell cell = Grid[new GridPoint(2, 3)].GetComponent<MyCell>(); This is an example of how you would access your own data of a specific cell. 
        //MyCell is in this case my own class/component that I would make elsewhere  
    }

    //The example below changes the color of the cells to black. 
    override public void InitGrid() //when the grid is initialized -> do things 
    {
        foreach (var point in Grid.Points)
        {
            var cell = (SpriteCell)Grid[point];
            cell.Color = new Color(0, 0, 0);


			GridPoint2 testPoint = new GridPoint2(2, 2);
            List <GridPoint2> directions = new List<GridPoint2> {
         		new GridPoint2(1, 0),
         		new GridPoint2(-1, 0),
    		};
            
			List <GridPoint2> neighbors = new List <GridPoint2>(testPoint.GetVectorNeighbors(directions));
			print (neighbors.Count);
			
			List <GridPoint2> neighbors2 = new List <GridPoint2>(RectPoint.GetOrthogonalNeighbors(testPoint));
			//print (neighbors2.Count);


        }
    }


}
