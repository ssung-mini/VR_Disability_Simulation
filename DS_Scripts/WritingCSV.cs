using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WritingCSV : MonoBehaviour
{
    // ���丮 ���� ���
    static string path;


    public int _participantsNumber;
    public static int participantsNumber;
    public int trialNum = 0;

    public Camera _camera;
    public Transform _wheelchair;

    public float endTime = 600.0f;

    static CsvFileWriter HeadMovement;
    static List<string> HeadColumns;

    static CsvFileWriter WheelchairMovement;
    static List<string> WheelchairColumns;

    static CsvFileWriter CollisionNum;
    static List<string> CollisionColumns;

    public static bool isEnded = true;

    public static float currentTime = 0f;

    // Start is called before the first frame update

    void Start()
    {
        participantsNumber = _participantsNumber;


        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
            "/DS_Study/" + participantsNumber + "�� ������/";

        // ���� ���� Ȯ��
        DirectoryInfo di = new DirectoryInfo(path);

        while (di.Exists)    // ex) trial 1 ������ �̹� ������ trial 2 ������ �����ϰԲ� ���� (1, 2 ���� -> 3 ����)
        {
            ++trialNum;
            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
                "/DS_Study/" + participantsNumber + "�� ������/" + trialNum + "/";
            di = new DirectoryInfo(path);
        }

        // ������ ������ ���� ����
        if (!di.Exists)
            di.Create();


        HeadMovement = new CsvFileWriter(path + "HeadMovement.csv");
        HeadColumns = new List<string>() { "CurrentTime", "HeadRot_x", "HeadRot_y", "HeadRot_z" };
        WheelchairMovement = new CsvFileWriter(path + "WheelchairMovement.csv");
        WheelchairColumns = new List<string>() { "CurrentTime", "WCPos_x", "WCPos_y", "WCPos_z", "WCRot_x", "WCRot_y", "WCRot_z" };
        CollisionNum = new CsvFileWriter(path + "CollisionNum.csv");
        CollisionColumns = new List<string>() { "participantsNum", "Obstacle", "HillWall" };

        HeadMovement.WriteRow(HeadColumns);
        HeadColumns.Clear();
        WheelchairMovement.WriteRow(WheelchairColumns);
        WheelchairColumns.Clear();
        CollisionNum.WriteRow(CollisionColumns);
        CollisionColumns.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnded)
        {
            Check_Timer();
            WritingHeadData();
            WritingWheelchairData();
            if (currentTime > endTime)
            {
                End_Timer();
            }
        }
    }

    // Timer ����
    private void Check_Timer()
    {
        currentTime += Time.deltaTime;
    }

    public static void End_Timer()
    {
        Debug.Log("End");
        isEnded = true;
        Debug.Log(currentTime);
        WritingCollisionNumber();
        OnOffCanvas.EndCanvas();
        StaticCoroutine.DoCoroutine(GameClearQuit());
    }

    public static void Start_Timer()
    {
        isEnded = false;
        Debug.Log("Start");
    }

    void WritingHeadData()
    {
        // Head�� Rotation ��� (Head rotation ���� inspector �� �״�� ������)
        var headRotation = _camera.transform.rotation.eulerAngles;

        HeadColumns.Add(currentTime.ToString());
        HeadColumns.Add(headRotation.x.ToString());
        HeadColumns.Add(headRotation.y.ToString());
        HeadColumns.Add(headRotation.z.ToString());

        HeadMovement.WriteRow(HeadColumns);
        HeadColumns.Clear();
    }

    void WritingWheelchairData()
    {
        // Head�� Rotation ��� (Head rotation ���� inspector �� �״�� ������)
        Vector3 wheelchairPosition = _wheelchair.position;
        Vector3 wheelchairRotation = _wheelchair.rotation.eulerAngles;

        WheelchairColumns.Add(currentTime.ToString());
        WheelchairColumns.Add(wheelchairPosition.x.ToString());
        WheelchairColumns.Add(wheelchairPosition.y.ToString());
        WheelchairColumns.Add(wheelchairPosition.z.ToString());
        WheelchairColumns.Add(wheelchairRotation.x.ToString());
        WheelchairColumns.Add(wheelchairRotation.y.ToString());
        WheelchairColumns.Add(wheelchairRotation.z.ToString());

        WheelchairMovement.WriteRow(WheelchairColumns);
        WheelchairColumns.Clear();
    }

    public static void WritingCollisionNumber()
    {
        CollisionColumns.Add(participantsNumber.ToString());
        CollisionColumns.Add(CollisionObstacle.collisionNum.ToString());
        CollisionColumns.Add(CollisionObstacle.hillCollisionNum.ToString());

        CollisionNum.WriteRow(CollisionColumns);
        CollisionColumns.Clear();
    }

    private void OnApplicationQuit()
    {
        HeadMovement.Dispose();
        WheelchairMovement.Dispose();
        CollisionNum.Dispose();
    }

    // 5�� �� ���� �ڷ�ƾ
    public static IEnumerator GameClearQuit()
    {
        yield return new WaitForSecondsRealtime(5f);
        GameQuit();
    }

    // ���� ����
    public static void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
