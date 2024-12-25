using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridKeeper : MonoBehaviour
{
 
    public int[,] grid;

    private void Start()
    {
        
        grid = LevelGenerator.grid;
        
    }

}
