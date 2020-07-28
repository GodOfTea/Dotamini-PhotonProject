using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractController : MonoBehaviour
{
    public UILabel nameLabel;

    public void FillContract(string name)
    {
        nameLabel.text = name;
    }

    public void SetContractAsComplete()
    {
        nameLabel.text = "COMPLETE";
    }
}
