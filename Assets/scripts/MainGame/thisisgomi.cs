using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class thisisgomi : MonoBehaviour
{

    //------------------変数-------------------------//
    public int[,] map = new int[50, 50];//x座標、y座標
    bool[,] kakutei = new bool[50, 50];
    public int downx, downy;
    public int ippen;//一辺の長さ 
    public int magarukaisuu;
    public int previous = 0;//0は下向き、1は右向き、２は下向き、３は左向き
                            //-------------------------------------------------//
                            // Use this for initialization

    bool check(int x, int y, int range, int movex, int movey)
    {
        for (int i = 0; i < range; i++)//ゴールにたどり着くまで
        {
            if (map[y + movey, x + movex] == 1 && kakutei[y + movey, x + movex] == true)
            {
                return false;
            }
            x += movex; y += movey;
        }

        //    map[y + movey, x + movex] = 1; kakutei[y + movey, x + movex] = true;

        for (int i = 0; i < range; i++)
        {
            map[y, x] = 0; kakutei[y, x] = true;
            x -= movex; y -= movey;
        }
        return true;
    }

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            map[downy - 1, downx - 1 + i] = 1;
            kakutei[downy - 1, downx - 1 + i] = true;
        }
        for (int i = 0; i <= ippen; i++)
        {
            map[0, i] = 1; map[i, 0] = 1; map[ippen, i] = 1; map[i, ippen] = 1;
            kakutei[0, i] = true; kakutei[i, 0] = true; kakutei[ippen, i] = true; kakutei[i, ippen] = true;
        }

        for (int nannkaime = 0; nannkaime < magarukaisuu; nannkaime++)
        {
            int muki, downrange = 0, downhantairange = 0;
            //重力通りに動く玉から
            while (true)
            {
                //0は下向き、1は右向き、２は上向き、３は左向き
                muki = Random.Range(0, 4);
                if (muki == previous) { continue; }
                if (muki == (previous + 2) % 4) { continue; }
                previous = muki;

                if (muki == 0)//した 
                {
                    downrange = Random.Range(0, 15 - downy);//壁を作る一歩手前で止まる
                    if (downrange % 2 == 1) { continue; }//もし距離が奇数なら
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[downy + downrange + 1, downx - 1 + i] == 0 && kakutei[downy + downrange + 1, downx - 1 + i] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(downx, downy, downrange, 0, 1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[downy + downrange + 1, downx - 1 + i] = 1;//三マス塗る！
                        kakutei[downy + downrange + 1, downx - 1 + i] = true;//
                    }
                    //----------------------------------------------//
                    if (kakutei[downy - 1, downx] == false)
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (downy == 1) { continue; }
                            downhantairange = Random.Range(2, downy);//ここ分からん！
                            if (downhantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo && kakutei[downy - downhantairange, downx] == false)//
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[downy - downhantairange, downx - 1 + i]) { continue; }
                                map[downy - downhantairange, downx - 1 + i] = 1;//
                                kakutei[downy - downhantairange, downx - 1 + i] = true;//
                            }
                        }
                    }
                    downy += downrange;//
                }
                if (muki == 1)
                {
                    downrange = Random.Range(0, 15 - downx);//直す
                    if (downrange % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[downy - 1 + i, downx + downrange + 1] == 0 && kakutei[downy - 1 + i, downx + downrange + 1] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(downy, downx, downrange, 1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[downy - 1 + i, downx + downrange + 1] = 1;//直す
                        kakutei[downy - 1 + i, downx + downrange + 1] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[downy, downx - 1] == false)//直す
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (downx == 1) { break; }//直す
                            downhantairange = Random.Range(2, downx);//直す
                            if (downhantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo && kakutei[downy, downx - downhantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[downy - 1 + i, downx - downhantairange]) { continue; }//直す
                                map[downy - 1 + i, downx - downhantairange] = 1;//直す
                                kakutei[downy - 1 + i, downx - downhantairange] = true;
                            }
                        }
                    }
                    downx += downrange;//直す
                }
                if (muki == 2)//上
                {
                    downrange = Random.Range(0, downy);
                    if (downrange % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[downy - downrange - 1, downx - 1 + i] == 0 && kakutei[downy - downrange - 1, downx - 1 + i] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(downy, downx, downrange, 0, -1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[downy - downrange - 1, downx - 1 + i] = 1;//直す
                        kakutei[downy - downrange - 1, downx - 1 + i] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[downy + 1, downx] == false)//直す
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (downy == 15) { break; }//直す
                            downhantairange = Random.Range(2, 15 - downy);//直す
                            if (downhantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo && kakutei[downy + downhantairange, downx] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[downy + downhantairange, downx - 1 + i]) { continue; }//直す
                                map[downy + downhantairange, downx - 1 + i] = 1;//直す
                                kakutei[downy + downhantairange, downx - 1 + i] = true;
                            }
                        }
                    }
                    downy -= downrange;
                }
                if (muki == 3)
                {
                    downrange = Random.Range(0, downx);//直す
                    if (downrange % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[downy - 1 + i, downx - downrange - 1] == 0 && kakutei[downy - 1 + i, downx - downrange - 1] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(downy, downx, downrange, -1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[downy - 1 + i, downx - downrange - 1] = 1;//直す
                        kakutei[downy - 1 + i, downx - downrange - 1] = true;//直す
                    }
                    //----------------------------------------------//
                    if (kakutei[downy, downx + 1] == false)//直す
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (downx == 15) { break; }//直す
                            downhantairange = Random.Range(2, 15 - downx);//直す
                            if (downhantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo && kakutei[downy, downx + downhantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[downy - 1 + i, downx + downhantairange]) { continue; }//直す
                                map[downy - 1 + i, downx + downhantairange] = 1;//直す
                                kakutei[downy - 1 + i, downx + downhantairange] = true;
                            }
                        }
                    }
                    downx -= downrange;//直す
                }
                Debug.Log(muki); Debug.Log(downrange);
                break;
            }
        }
        Debug.Log("  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7");
        for (int i = 0; i < 17; i++)
        {
            string a = ""; a += i.ToString(); a += " ";
            for (int j = 0; j < 17; j++)
            {
                a += map[i, j].ToString();
                a += " ";
            }
            Debug.Log(a);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
