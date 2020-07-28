using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class GridController : MonoBehaviour
{
    /* Ссылки на другие объекты */
    [SerializeField] private HeroesPool heroesPool;
    [SerializeField] private GameObject grid;
    [SerializeField] private HeroIcon heroIcon;

    private Vector2 rightTopCorner = new Vector2(-0.75f, 0.5f);

    /* Количество строк и столбцов */
    [SerializeField] private int rows = 6;
    [SerializeField] private int columns = 10;

    /* Отступ */
    [SerializeField] private float step = 0.25f;

    /* Для синхронизации */
    private double lastTickTime;

    public void FillGrid(int[] gamePool)
    {
        foreach (Transform child in grid.transform) /* чистим грид */
            Destroy(child.gameObject);

        Vector3 pos = rightTopCorner; /* начальные переменные */
        pos.z = 10;
        int index = 0;

        for (int i = 0; i < rows; i++)  /* заполнение по строкам */
        {
            for (int j = 0; j < columns; j++)  /* заполнение по столбцам */
            {
                heroIcon.Spawn(heroesPool.pool[gamePool[index]],  /* вставка иконки */
                                grid.transform, pos);

                pos.x += 0.25f;  /* смена координат */
                index++;
            }
            pos.y -= 0.25f;  /* смена координат */
            pos.x = rightTopCorner.x;
        }
    }
}
