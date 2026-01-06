using UnityEngine;

public class StudyParameter : MonoBehaviour
{
    public int number = 10;

    void Start()
    {
        NormalParamerter(number);
        RefParameter(ref number);
        OutParameter(out number);
        int returnValue = ReturnValue(number);
        Debug.Log("Return Value : " + returnValue); // 400
    }

    private void NormalParamerter(int n)
    {
        Debug.Log("Parameter Value : " + n); // 10

        n = 100;

        Debug.Log("Parameter Value Changed : " + number); // 10

    }

    private void RefParameter(ref int n)
    {
        Debug.Log("ref Parameter Value : " + n); // 10

        n = 200;

        Debug.Log("ref Parameter Value Changed : " + n); // 200
    }

    private void OutParameter(out int n)
    {
        n = 300;
        Debug.Log("Out Parameter Value : " + n); // 300
    }

    private int ReturnValue(int n)
    {
        n = 400;
        return n; // 400
    }
}
