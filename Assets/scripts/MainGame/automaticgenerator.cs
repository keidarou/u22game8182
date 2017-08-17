using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class automaticgenerator : MonoBehaviour
{

    //------------------変数-------------------------//
    public int[,] map = new int[50, 50];//x座標、y座標
    bool[,] kakutei = new bool[50, 50];
    public int nowx, nowy;
    public string scenename;
    public int downx, downy;
    public int saiteigenkabe;
    public int ippen;//一辺の長
    public int magarukaisuu;
    public int startupx, startdownx, startupy, startdowny;
    int cnt = 0;
    public int previous = 0;//0は下向き、1は右向き、２は下向き、３は左向き
                            //-------------------------------------------------//
                            // Use this for initialization

    bool check(int y, int x, int range, int movex, int movey)
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
        kakutei[downx, downy] = true;
        kakutei[nowx, nowy] = true;
        map[downx, downy] = 0;
        map[nowx, nowy] = 0;
        startdownx = downx;
        startdowny = downy;
        startupx = nowx;
        startupy = nowy;
        for (int i = 0; i <= ippen; i++)
        {
            for (int j = 0; j <= ippen; j++)
            {
                kakutei[i, j] = false; map[i, j] = 0;
            }
        }
        for (int i = 0; i < 3; i++)//down
        {
            map[nowy - 1, nowx - 1 + i] = 1;
            kakutei[nowy - 1, nowx - 1 + i] = true;
        }
        for (int i = 0; i < 3; i++)//up
        {
            map[downy + 1, downx - 1 + i] = 1;
            kakutei[downy + 1, downx - 1 + i] = true;
        }
        for (int i = 0; i <= ippen; i++)
        {
            map[0, i] = 1; map[i, 0] = 1; map[ippen, i] = 1; map[i, ippen] = 1;
            kakutei[0, i] = true; kakutei[i, 0] = true; kakutei[ippen, i] = true; kakutei[i, ippen] = true;
        }

        for (int nannkaime = 0; nannkaime < magarukaisuu; nannkaime++)
        {
            int muki, range = 0, hantairange = 0, downrange = 0, downhantairange = 0;
            //重力通りに動く玉から
            while (true)
            {
                cnt++; if (cnt > 99999) { break; }
                //0は下向き、1は右向き、２は上向き、３は左向き
                muki = Random.Range(0, 4);
                if (muki == previous) { continue; }
                if (muki == (previous + 2) % 4) { continue; }

                if (muki == 0)//した 
                {
                    range = Random.Range(0, ippen - 1 - nowy);//壁を作る一歩手前で止まる
                    if (range % 2 == 1) { continue; }//もし距離が奇数なら
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[nowy + range + 1, nowx - 1 + i] == 0 && kakutei[nowy + range + 1, nowx - 1 + i] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(nowy, nowx, range, 0, 1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy + range + 1, nowx - 1 + i] = 1;//三マス塗る！
                        kakutei[nowy + range + 1, nowx - 1 + i] = true;//
                    }
                    //----------------------------------------------//
                    /*if (kakutei[nowy - 1, nowx] == false)
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (nowy == 1) { continue; }
                            hantairange = Random.Range(2, nowy);//ここ分からん！
                            if (hantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo&&kakutei[nowy - hantairange, nowx] == false)//
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - hantairange, nowx - 1 + i]) { continue; }
                                map[nowy - hantairange, nowx - 1 + i] = 1;//
                                kakutei[nowy - hantairange, nowx - 1 + i] = true;//
                            }
                        }
                    }*/
                    nowy += range;//
                    int count2 = 0;
                    while (true)//
                    {
                        count2++; if (count2 > 9999) { break; }
                        downrange = Random.Range(0, downy);
                        if (downrange % 2 == 1) { continue; }
                        flag = false;
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
                        break;
                    }
                    //----------------------------------------------//
                    /* if (kakutei[downy + 1, downx] == false)//直す
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
                     }*/
                    downy -= downrange;
                }
                if (muki == 1)
                {
                    range = Random.Range(0, ippen - 1 - nowx);//直す
                    if (range % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[nowy - 1 + i, nowx + range + 1] == 0 && kakutei[nowy - 1 + i, nowx + range + 1] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(nowy, nowx, range, 1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - 1 + i, nowx + range + 1] = 1;//直す
                        kakutei[nowy - 1 + i, nowx + range + 1] = true;//直す
                    }
                    //----------------------------------------------//
                    /*if (kakutei[nowy, nowx - 1] == false)//直す
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (nowx == 1) { break; }//直す
                            hantairange = Random.Range(2, nowx);//直す
                            if (hantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo&&kakutei[nowy, nowx - hantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - 1 + i, nowx - hantairange]) { continue; }//直す
                                map[nowy - 1 + i, nowx - hantairange] = 1;//直す
                                kakutei[nowy - 1 + i, nowx - hantairange] = true;
                            }
                        }
                    }*/
                    nowx += range;//直す
                    int count2 = 0;
                    while (true)
                    {
                        count2++; if (count2 > 9999) { break; }
                        downrange = Random.Range(0, downx);//直す
                        if (downrange % 2 == 1) { continue; }
                        flag = false;
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
                        break;
                    }
                    //----------------------------------------------//
                    /* if (kakutei[downy, downx + 1] == false)//直す
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
                     }*/
                    downx -= downrange;//直す
                }
                if (muki == 2)//上
                {
                    range = Random.Range(0, nowy);
                    if (range % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[nowy - range - 1, nowx - 1 + i] == 0 && kakutei[nowy - range - 1, nowx - 1 + i] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(nowy, nowx, range, 0, -1)) { continue; }
                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - range - 1, nowx - 1 + i] = 1;//直す
                        kakutei[nowy - range - 1, nowx - 1 + i] = true;//直す
                    }
                    //----------------------------------------------//
                    /*if (kakutei[nowy + 1, nowx] == false)//直す
                    {
                        bool keidaroo = false ;
                        while (true)
                        {
                            if (nowy == 15) { break; }//直す
                            hantairange = Random.Range(2, 15 - nowy);//直す
                            if (hantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo&&kakutei[nowy + hantairange, nowx] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy + hantairange, nowx - 1 + i]) { continue; }//直す
                                map[nowy + hantairange, nowx - 1 + i] = 1;//直す
                                kakutei[nowy + hantairange, nowx - 1 + i] = true;
                            }
                        }
                    }*/
                    nowy -= range;
                    int count2 = 0;
                    while (true)
                    {
                        count2++; if (count2 > 9999) { break; }
                        downrange = Random.Range(0, ippen - 1 - downy);//壁を作る一歩手前で止まる
                        if (downrange % 2 == 1) { continue; }//もし距離が奇数なら
                        flag = false;
                        for (int i = 0; i < 3; i++)
                        {
                            if (map[downy + downrange + 1, downx - 1 + i] == 0 && kakutei[downy + downrange + 1, downx - 1 + i] == true) { flag = true; break; }//三マス確認
                        }
                        if (flag) { continue; }
                        if (!check(downy, downx, downrange, 0, 1)) { continue; }
                        for (int i = 0; i < 3; i++)
                        {
                            map[downy + downrange + 1, downx - 1 + i] = 1;//三マス塗る！
                            kakutei[downy + downrange + 1, downx - 1 + i] = true;//
                        }
                        break;
                    }
                    //----------------------------------------------//
                    /*if (kakutei[downy - 1, downx] == false)
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
                    }*/
                    downy += downrange;//
                }
                if (muki == 3)
                {
                    range = Random.Range(0, nowx);//直す
                    if (range % 2 == 1) { continue; }
                    bool flag = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (map[nowy - 1 + i, nowx - range - 1] == 0 && kakutei[nowy - 1 + i, nowx - range - 1] == true) { flag = true; break; }//三マス確認
                    }
                    if (flag) { continue; }
                    if (!check(nowy, nowx, range, -1, 0)) { continue; }//直す

                    for (int i = 0; i < 3; i++)
                    {
                        map[nowy - 1 + i, nowx - range - 1] = 1;//直す
                        kakutei[nowy - 1 + i, nowx - range - 1] = true;//直す
                    }
                    //----------------------------------------------//
                    /*if (kakutei[nowy, nowx + 1] == false)//直す
                    {
                        bool keidaroo = false;
                        while (true)
                        {
                            if (nowx == 15) { break; }//直す
                            hantairange = Random.Range(2, 15-nowx);//直す
                            if (hantairange % 2 == 0) { keidaroo = true; break; }
                        }
                        if (!keidaroo&&kakutei[nowy, nowx + hantairange] == false)//直す
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (kakutei[nowy - 1 + i, nowx + hantairange]) { continue; }//直す
                                map[nowy - 1 + i, nowx + hantairange] = 1;//直す
                                kakutei[nowy - 1 + i, nowx + hantairange] = true;
                            }
                        }
                    }*/
                    nowx -= range;//直す
                    int count2 = 0;
                    while (true)
                    {
                        count2++; if (count2 > 9999) { break; }
                        downrange = Random.Range(0, ippen - 1 - downx);//直す
                        if (downrange % 2 == 1) { continue; }
                        flag = false;
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
                        break;
                    }
                    //----------------------------------------------//
                    /*if (kakutei[downy, downx - 1] == false)//直す
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
                    }*/
                    downx += downrange;//直す
                }
                previous = muki;
                break;
            }
        }
        ////////////////////////////////
        if (nowx == startupx && nowy == startupy) { SceneManager.LoadScene(scenename); }
        if (nowx == startdownx && nowy == startdowny) { SceneManager.LoadScene(scenename); }
        if (downx == startupx && downy == startupy) { SceneManager.LoadScene(scenename); }
        if (downx == startdownx && downy == startdowny) { SceneManager.LoadScene(scenename); }
        int pikuto = 0;
        for (int i = 1; i < ippen; i++)
        {
            for (int j = 1; j < ippen; j++)
            {
                if (map[i, j] == 1) { pikuto++; }
            }
        }
        if (pikuto < saiteigenkabe) { SceneManager.LoadScene(scenename); }
        Debug.Log("pikuto");
        Debug.Log(pikuto);
        int pikutocount = 0;
        if (ippen == 24)
        {
            while (true)
            {
                if (pikutocount == 2) { break; }
                int shougaibutux = Random.Range(0, ippen), shougaibutuy = Random.Range(0, ippen);
                if (shougaibutux % 2 == 0 || shougaibutuy % 2 == 0) { continue; }
                if (kakutei[shougaibutux, shougaibutuy] == false)
                {
                    pikutocount++;
                    kakutei[shougaibutux, shougaibutuy] = true;
                    map[shougaibutux, shougaibutuy] = 3;
                }
            }
        }
        map[nowy, nowx] = 2;
        map[downy, downx] = 2;
     /*   Debug.Log("  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7");
        for (int i = 0; i < 17; i++)
        {
            string a = ""; a += i.ToString(); a += " ";
            for (int j = 0; j < 17; j++)
            {
                /*if (kakutei[i, j]) { a += "1"; }
                else { a += "0"; }
                a += map[i, j];
                a += " ";
            }
            Debug.Log(a);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
