using UnityEngine;
using work.ctrl3d;

public class Example : MonoBehaviour
{
    private async void Start()
    {
        var osCmd = new OSCmd();
        var output = await osCmd.RunAsync("echo", "Hello World");
        
        Debug.Log(output);
    }
}