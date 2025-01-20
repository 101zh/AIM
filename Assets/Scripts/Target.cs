using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    PracticeSessionManager practiceSessionManagerInstance;

    private void Start()
    {
        practiceSessionManagerInstance = PracticeSessionManager.instance;
    }

    private void OnBecameVisible()
    {
        practiceSessionManagerInstance.onTargetInView?.Invoke();
    }

}
