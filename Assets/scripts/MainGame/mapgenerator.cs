using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgenerator : MonoBehaviour
{
    public bool automaticMode;
    public int width;
    //heightも同じなのでメモリ節約
     /*public int[,] map = new int[,] {
         { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,0,0,0,0,0},
         {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0 },
         {1,0,0,0,0,0,1,0,1,1,1,1,1,0,0,0,1,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,1,1,1,0,1,0,0,0,1,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0 },
         {1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,1,0,1,1,1,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,1,1,1,1,1,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         {1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1 ,0,0,0,0,0},
         { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
         {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
     };*/
   public int[,] map = new int[50, 50];
    public GameObject tate, yoko, goal, shougaimono;
    public float kiteix, kiteiy, haba, z;//規定はマップの左端のx座標またはy座標版、habaは、一ますごとの間隔
    // Use this for initialization
    void Start()
    {
        float pikutocount = 0;//微妙なずれを発生させてテクスチャをよくするため
        if (automaticMode)
            map = GetComponent<automaticgenerator>().map;//これ、こっちの方が速く実行されていたらしぬので、そこを注意
        else
        {
            map = GetComponent<codevisualizer>().Lockoff();
            width = GetComponent<codevisualizer>().width+1;
        }
        //縦に連なった壁
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int jj = j / 2, ii = i / 2;
                if (i % 2 == 1) { continue; }
                if (map[i, j] == 1 && map[i + 1, j] == 1 && map[i + 2, j] == 1)
                {
                    pikutocount += 0.001f;
                    GameObject instanttate = (GameObject)Instantiate(tate);
                    instanttate.transform.position = new Vector3(-kiteix - haba * (width / 2 - jj + 1), kiteiy + haba * (width / 2 - ii + 1) - 0.5f, z + pikutocount);
                }
            }
        }
        //横に連なった壁
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < width; j++)
            {

                int jj = j / 2, ii = i / 2;
                if (map[i, j] == 2)
                {
                    GameObject goalins = (GameObject)Instantiate(goal);
                    goalins.transform.position = new Vector3(-kiteix - haba * (width / 2 + 1 - jj) + 0.5f, kiteiy + haba * (width / 2 - ii + 1) - 0.5f, 0f); continue;
                }
                if (map[i, j] == 3)
                {
                    GameObject shougaibutu = (GameObject)Instantiate(shougaimono);
                    shougaibutu.transform.position = new Vector3(-kiteix - haba * (width / 2 + 1 - jj) + 0.5f, kiteiy + haba * (width / 2 - ii + 1) - 0.5f, 0f); continue;

                }
                if (j % 2 == 1) { continue; }
                if (map[i, j] == 1 && map[i, j + 1] == 1 && map[i, j + 2] == 1)
                {
                    pikutocount += 0.001f;
                    GameObject instanttate = (GameObject)Instantiate(yoko);
                    instanttate.transform.position = new Vector3(-kiteix - haba * (width / 2 + 1 - jj) + 0.5f, kiteiy + haba * (width / 2 - ii + 1), z + pikutocount);
                }
            }
        }
        /*    for (int i = 0; i < width; i++)
            {
                for (int j = 0; j <width; j++)
                {
                    if (map[i, j] == 2)
                    {
                        GameObject goalins = (GameObject)Instantiate(goal);
                        goalins.transform.position = new Vector3(+kiteix + (haba / 3) * j, kiteiy - (haba / 3) * i, z); continue;
                    }
                }
            }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
