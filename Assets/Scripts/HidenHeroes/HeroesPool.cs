using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroesPool : MonoBehaviour
{
    public List<Hero> pool;
    [SerializeField] private List<int> gamePool; /* индексы героев */


    public int[] CreateGamePool(int heroesCount)
    {
        int[] result = new int[heroesCount];
        gamePool.RemoveRange(0, gamePool.Count); /* чиститься пул старого раунда */
        List<Hero> poolCopy = new List<Hero>(pool); /* создается копия общего пула */

        for (int i = 0; i < heroesCount; i++) /* из общего пула выбирается 60 героев */
        {
            int heroId = Random.Range(0, poolCopy.Count); /* рандом случайного героя */
            gamePool.Add(poolCopy[heroId].index); /* этот герой добавляется в пул раунда */
            result[i] = poolCopy[heroId].index;
            poolCopy.RemoveAt(heroId); /* из копии пула удалеяется этот герой */
        }


        return result;
    }
}
