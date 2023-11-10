using UnityEngine;
public class CameraController : MonoBehaviour
{
    void Start()
    {
        Camera.main.transform.position = new Vector3(DifficultySettings.gridWidth / 2, DifficultySettings.gridHeight / 2, -10);
        if (DifficultySettings.gridHeight >=10 && DifficultySettings.gridHeight <=12)
            Camera.main.orthographicSize = 8;
        else if (DifficultySettings.gridHeight > 12 && DifficultySettings.gridHeight <= 14)
            Camera.main.orthographicSize = 10;
        else if(DifficultySettings.gridHeight > 14 && DifficultySettings.gridHeight <= 16)
            Camera.main.orthographicSize = 11;
        else
            Camera.main.orthographicSize =12;
    }
}
