using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AManager : MonoBehaviour
{
    protected int currStep = 0;

    protected virtual void Start()
    {
        EventManager.Instance.StartListening("NextStep", NextStep);
    }

    protected abstract void Update();

    protected abstract void NextStep(EventParam e);

    protected virtual void OnDestroy()
    {
        EventManager.Instance.StopListening("NextStep", NextStep);
    }
}
