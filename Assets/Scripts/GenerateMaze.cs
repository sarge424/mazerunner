using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    [Header("Maze Settings")]
    public int width;
    public int height;

    [Header("Tile Settings")]
    public GameObject wallTile;
    public float tilesize;

    enum dir{n,e,w,s,o};

    // Start is called before the first frame update
    void Start()
    {

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

        for(int i = 0; i < width * height * 10; i++){
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
        GameObject[,] tiles = new GameObject[2*width+1, 2*height+1];

        for(int i = 0; i < 2*width + 1; i++){
            for(int j = 0; j < 2*height + 1; j++){
                tiles[i, j] = Instantiate(wallTile, new Vector3(i, 0, j) * tilesize, Quaternion.identity);
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
                if(tiles[i,j].activeSelf){
                    if((!tiles[i-1,j].activeSelf && !tiles[i+1,j].activeSelf) || (!tiles[i,j-1].activeSelf && !tiles[i,j+1].activeSelf)){
                            if(Random.Range(0f, 1f) <= 0.1)
                                tiles[i,j].SetActive(false);
                        }
                }
            }
        } 
    }
}
