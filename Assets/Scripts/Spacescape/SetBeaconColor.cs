using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBeaconColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateBeaconPath BeaconSetting = GetComponentInParent<CreateBeaconPath>();
        if (BeaconSetting != null)
            GetComponent<SpriteRenderer>().color = BeaconSetting.BeaconColor;
        Destroy(this);
    }

}
