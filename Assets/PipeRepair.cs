using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeRepair : MonoBehaviour
{
    public Sprite[] PipeImage;
    public Pipes[] ShipPart;
    [System.Serializable]
    public class Pipes
    {
        public RectTransform Pipe;
        public int PipeValue;
    }
    public void Start()
    {
        RandomizeAllPipe();

    }
    public void RandomizeAllPipe()
    {
        //starting with the SECOND pipe, randomize the offset
        for (int i = 1; i < ShipPart.Length; i++)
        {
            Vector2 newPosition = new Vector2(ShipPart[i].Pipe.anchoredPosition.x, 0);
            newPosition.y = ShipPart[i - 1].Pipe.anchoredPosition.y + Random.Range(-PipeImage.Length + 1, PipeImage.Length);
            ShipPart[i].Pipe.anchoredPosition = newPosition;
        }
        UpdatePipe();
    }
    void UpdatePipe()
    {
        for (int i = 0; i < ShipPart.Length; i++)
        {
            //Distance between the two pipes, if the number is negative the scale is negative so the part is flipped
            int PipeDistance = Mathf.RoundToInt(ShipPart[i].Pipe.anchoredPosition.y - ShipPart[i + 1].Pipe.anchoredPosition.y);
            if (PipeDistance > 0)
                ShipPart[i].Pipe.localScale = new Vector3(1, -1, 1);
            else
                ShipPart[i].Pipe.localScale = Vector3.one;

            ShipPart[i].Pipe.GetComponent<Image>().sprite = PipeImage[Mathf.Abs(PipeDistance)];
        }
    }


    public void HitPipe(int PipeHit)
    {
        MovePipe(PipeHit, 3);
        MovePipe(PipeHit + 1, 1);
        MovePipe(PipeHit - 1, 1);


        ShipPart[PipeHit + 1].PipeValue = Mathf.Clamp(ShipPart[PipeHit + 1].PipeValue, PipeImage.Length, -PipeImage.Length);
        UpdatePipe();
    }

    void MovePipe(int PipeMove, int Strength)
    {

        int DirectionalMult = 1;
        if (ShipPart[PipeMove].Pipe.anchoredPosition.y > 0)
            DirectionalMult = -1;

        Vector2 newPosition = ShipPart[PipeMove].Pipe.anchoredPosition;

        newPosition.y += Strength * DirectionalMult;
        ShipPart[PipeMove].Pipe.anchoredPosition = newPosition;
    }

    public void EndSkill()
    {
        GetComponentInParent<RepairMinigame>().EndSkill();
    }
}