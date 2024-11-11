using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bleh:MonoBehaviour
{
    public Button Button1;
    public TextMeshProUGUI Text;

    public void OnButtonPress()
    {
        Text.text = "Clicked";
    }
}

