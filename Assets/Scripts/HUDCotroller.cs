using TMPro;
using UnityEngine;

public class HUDCotroller : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI timeText;
    
    public TextMeshProUGUI energyLossText;
    public TextMeshProUGUI timeDilationText;
    
    public TextMeshProUGUI posXText;
    public TextMeshProUGUI posYText;
    public TextMeshProUGUI posZText;

    public void setFps(string fps)
    {
        fpsText.text = "FPS: " + fps;
    }

    public void setTime(string time)
    {
        timeText.text = "Time: " + time;
    }

    public void setEnergyLoss(string energyLoss)
    {
        energyLossText.text = "Energy Loss: " + energyLoss;
    }
    
    public void setTimeDilation(string timeDilation)
    {
        timeDilationText.text = "Time Dilation: " + timeDilation;
    }

    public void setPosX(string posX)
    {
        posXText.text = "X: " + posX;
    }
    
    public void setPosY(string posY)
    {
        posYText.text = "Y: " + posY;
    }
    
    public void setPosZ(string posZ)
    {
        posZText.text = "Z: " + posZ;
    }
}
