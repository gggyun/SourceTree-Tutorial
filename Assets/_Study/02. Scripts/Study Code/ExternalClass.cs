using UnityEngine;

public class ExternalClass : MonoBehaviour
{

    void Start()
    {
        StudyUnityAction.buttonAction += MethodB;
    }

    public void MethodB()
    {
        Debug.Log("MethodB");
    }
}