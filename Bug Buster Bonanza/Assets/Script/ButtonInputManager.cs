using UnityEngine;
using UnityEngine.Events;

public class ButtonInputManager : MonoBehaviour
{
    public UnityEvent onButtonPressed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string line = SerialManager.Instance.LatestLine;

        if (!string.IsNullOrEmpty(line) && line.Contains("BTN"))
        {
            Debug.Log("ButtonInputManager: Button pressed detected.");
            onButtonPressed?.Invoke();
        }
    }

}
