[System.Serializable]
public class SaveData
{
    public float PlayerPosX;
    public float PlayerPosY;
    public string PlayerRoom;

    public float MasterVolume = 1f;
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;

    public bool[] Findings = new bool[11];
        //tornedphoto;
        //doll;
        //painting_1;
        //tailorbox;
        //grouphoto;
        //statue;
        //painting_2;
        //brokenstatue;
        //confession;
        //shoes;
        //pocketwatch;
}