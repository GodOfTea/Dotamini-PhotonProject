using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargestList : MonoBehaviour
{
    [SerializeField] private HeroesPool heroesPool;
    [SerializeField] GridController gridController; /* ссылка на скрипт */
    [SerializeField] UIGrid contractsGrid; /* ссылка на UI объект */
    [SerializeField] GameObject contract; /* ссылка на объект */

    public List<string> killsTargets;
    public List<ContractController> contracts;

    [SerializeField] private List<string> stack;

    public string[] SetTargets(int[] gamePool)
    {
        string[] targets = new string[3];

        /* Чистка списка */
        if (killsTargets.Count > 0)
        {
            killsTargets.RemoveRange(0, killsTargets.Count);
            contracts.RemoveRange(0, contracts.Count);
        }

        /* Добавление имен */
        for (int i = 0; i < 3; i++)
        {
            int heroIndex = -1;
            string newTarget = "";
            bool isNewTarget = false;

            if (stack.Count == 0)
            {
                heroIndex = Random.Range(0, gamePool.Length);
                newTarget = heroesPool.pool[gamePool[heroIndex]].name;
                isNewTarget = true;
            }
            
            while (isNewTarget == false)
            {
                heroIndex = Random.Range(0, gamePool.Length);
                newTarget = heroesPool.pool[gamePool[heroIndex]].name;

                foreach(var target in stack)
                {
                    if (target.Contains(newTarget) == true)
                    {
                        isNewTarget = false;
/* проверка */          Debug.Log("Find dubliate");
                        break;
                    }
                    else isNewTarget = true;
                }
            }

            stack.Add(newTarget);
            targets[i] = newTarget;
        }

        return targets;
    }

    public void SpawnContractsInGrid(string[] targets)
    {
        foreach (var target in targets) killsTargets.Add(target);

        for (int i = 0; i < 3; i++)
        {
            contractsGrid.gameObject.AddChild(contract.gameObject);
            
            var lastIndex = contractsGrid.transform.childCount - 1;
            var data = contractsGrid.transform.GetChild(lastIndex).GetComponent<ContractController>();

            data.FillContract(targets[i]);
            contracts.Add(data);
            contractsGrid.Reposition();
        }
    }
}
