using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParameterByID : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;
    private FMOD.Studio.PARAMETER_ID volumeParameterID;

    public FMODUnity.EventReference fmodEvent;
    [SerializeField] [Range(0f, 1f)]
    public float volume;
    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);

        FMOD.Studio.EventDescription volumeEventDescription;
        instance.getDescription(out volumeEventDescription);
        FMOD.Studio.PARAMETER_DESCRIPTION volumeParameterDescription;
        volumeEventDescription.getParameterDescriptionByName("Volume", out volumeParameterDescription);
        volumeParameterID = volumeParameterDescription.id;

        instance.start();

    }

    // Update is called once per frame
    void Update()
    {
        instance.setParameterByID(volumeParameterID, volume);
    }
}
