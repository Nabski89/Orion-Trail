using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeRepair : MonoBehaviour
{
    public Sprite[] PipeImage;
    public Sprite[] PipeImageFacingDown;
    public Sprite[] BrokenPipe;
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
    //the -1 is because we have the end component for our length, starting at 1 is so we don't move the first pipe
    public void RandomizeAllPipe()
    {
        //starting with the SECOND pipe, randomize the offset
        for (int i = 1; i < ShipPart.Length - 1; i++)
        {
            Vector2 newPosition = new Vector2(ShipPart[i].Pipe.anchoredPosition.x, 0);
            newPosition.y = ShipPart[i - 1].Pipe.anchoredPosition.y + Random.Range(-PipeImage.Length + 1, PipeImage.Length);
            ShipPart[i].Pipe.anchoredPosition = newPosition;
        }
        UpdatePipe();
    }


    void UpdatePipe()
    {
        for (int i = 0; i < ShipPart.Length - 1; i++)
        {
            //Distance between the two pipes, if the number is negative the scale is negative so the part is flipped
            int PipeDistance = Mathf.RoundToInt(ShipPart[i].Pipe.anchoredPosition.y - ShipPart[i + 1].Pipe.anchoredPosition.y);
            if (PipeDistance < 0)
                ShipPart[i].Pipe.GetComponent<Image>().sprite = PipeImage[Mathf.Abs(PipeDistance)];
            else
                ShipPart[i].Pipe.GetComponent<Image>().sprite = PipeImageFacingDown[Mathf.Abs(PipeDistance)];



            //turn on the other end of the pipe
            Transform PipeChild = ShipPart[i].Pipe.transform.GetChild(0);
            if (Mathf.Abs(PipeDistance) > PipeImage.Length - 2)
            {
                PipeChild.gameObject.SetActive(true);
                //         ShipPart[i].Pipe.transform.GetChild(0).RectTransform.anchoredPosition = new Vector2(0, PipeDistance);
            }
            else
            {
                PipeChild.gameObject.SetActive(false);
            }
            Debug.Log("We have completed pipe " + i);
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