using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotValue : MonoBehaviour
{
    public int Attack;
    public int Block;
    public int Buff;
    //todo undepreciate these
    public bool Rank1;
    public bool Rank2;
    public bool Rank3;

    public GameObject SelectedOutline;
    public void SlotHighlight()
    {
        GetComponentInParent<SlotMachineManager>().DisableSelectedHighlight();
        SelectedOutline.SetActive(true);
    }
    public void SlotHighlightCheck()
    {

    }
}
