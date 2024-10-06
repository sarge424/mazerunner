using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    [Header("Generator Settings")]
    public int width;
    public int height;
    public float deleteFrac;

    [Header("Tile Settings")]
    public GameObject wallTile;
    public float tilesize;

    [Header("Level Settings")]
    public GameObject floor;
    public GameObject ceiling;


    enum dir{n,e,w,s,o};
    GameObject[,] tiles;


    // Start is called before the first frame update
    void Start()
    {

        //create the floor
        Transform f = Instantiate(floor, new Vector3(width * tilesize, -.5f, height * tilesize), Quaternion.identity).transform;
        f.localScale = new Vector3(2*width + 1, 1/tilesize, 2*height + 1) * tilesize;
        
        //create the ceiling
        Transform c = Instantiate(ceiling, new Vector3(width * tilesize, 3.5f, height * tilesize), Quaternion.identity).transform;
        c.localScale = new Vector3(2*width + 1, 1/tilesize, 2*height + 1) * tilesize;

        // create the paths
        dir[,] maze = new dir[width, height];
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height-1; j++){
                maze[i,j] = dir.e;
            }

            maze[i, height-1] = dir.s;
        }

        maze[width-1, height-1] = dir.o;
        Vector2Int origin = new Vector2Int(width-1, height-1);

        for(int i = 0; i < width * height * 25; i++){
            dir originMoveDir = (dir)Random.Range(0,4);
            if(origin.x == 0 && originMoveDir == dir.n)
                originMoveDir = dir.s;
            if(origin.x == width-1 && originMoveDir == dir.s)
                originMoveDir = dir.n;
            if(origin.y == 0 && originMoveDir == dir.w)
                originMoveDir = dir.e;
            if(origin.y == height-1 && originMoveDir == dir.e)
                originMoveDir = dir.w;

            maze[origin.x, origin.y] = originMoveDir;
            switch(originMoveDir){
                case dir.n: origin.x -= 1; break;
                case dir.s: origin.x += 1; break;
                case dir.e: origin.y += 1; break;
                case dir.w: origin.y -= 1; break;
            }
        }


        // create the tiles for the scene
        tiles = new GameObject[2*width+1, 2*height+1];

        for(int i = 0; i < 2*width + 1; i++){
            for(int j = 0; j < 2*height + 1; j++){
                tiles[i, j] = Instantiate(wallTile, new Vector3(i, 0, j) * tilesize, Quaternion.identity);
                tiles[i, j].name = "wall"+i+","+j;
            }
        }

        // cut out the paths within the walls
        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                tiles[2*i + 1, 2*j + 1].SetActive(false);

                switch(maze[i,j]){
                    case dir.n: tiles[2*i, 2*j + 1].SetActive(false); break;
                    case dir.e: tiles[2*i + 1, 2*j + 2].SetActive(false); break;
                    case dir.w: tiles[2*i + 1, 2*j].SetActive(false); break;
                    case dir.s: tiles[2*i + 2, 2*j + 1].SetActive(false); break;
                }
            }
        }

        // randomly remove 20% of walls to form loops
        for(int i = 1; i < 2*width; i++){
            for(int j = 1; j < 2*height; j++){
                if(tiles[i,j].activeSelf && isRemovable(i, j)){
                    if(Random.Range(0f, 1f) <= deleteFrac)
                        tiles[i,j].SetActive(false);
                }
            }
        }

        //cut out entrance and exit
        //tiles[0,1].SetActive(false);
        tiles[2*width, 2*height-1].SetActive(false);
    }

    private bool isRemovable(int i,int j){
        int code = 0;
        if(tiles[i-1,j].activeSelf)
            code += 1000;
        if(tiles[i+1,j].activeSelf)
            code += 100;
        if(tiles[i,j-1].activeSelf)
            code += 10;
        if(tiles[i,j+1].activeSelf)
            code += 1;

        return code == 1100 || code == 0011;
    }
}
