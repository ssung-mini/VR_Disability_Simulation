using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Tobii.XR.DevTools;

public class WritingEmotionCSV : MonoBehaviour
{
    // 디렉토리 저장 경로
    static string path;


    public int _participantsNumber;
    public static int participantsNumber;
    private int trialNum = 0;

    public Camera _camera;
    public Transform _wheelchair;
    public GameObject gazePosition;
    public EyeDataManager eyeDataManager;
    public static ViveSR.anipal.Eye.TestFocus gazeScript;

    private static float timeRealGaze = 0;     // 응시하고 있는 시간이 0.2초 이상인지 아닌지 확인, Saccadic movement 잡기 위한 시간 체크.
    private static Vector3 previousRealGaze;

    public float endTime = 600.0f;

    static CsvFileWriter HeadMovement;
    static List<string> HeadColumns;

    static CsvFileWriter WheelchairMovement;
    static List<string> WheelchairColumns;

    static CsvFileWriter CollisionNum;
    static List<string> CollisionColumns;

    static CsvFileWriter gazeTime;
    static List<string> gazeColumns;

    static CsvFileWriter FilteredRealGazePosition;
    static List<string> FilteredRealGazecolumns;

    static CsvFileWriter FilteredEyeMovement;
    static List<string> FilteredEyeColumns;

    public static bool isEnded = true;

    public static float currentTime = 0f;

    public static float _headTime;
    public static float _bodyTime;

    //public static bool nowGazeHead = false;
    //public static bool nowGazeBody = false;
    //public static bool nowGazeEnv = false;

    //public static float headGazeTime = 0f;
    //public static float bodyGazeTime = 0f;
    //public static float envGazeTime = 0f;

    // Start is called before the first frame update

    void Start()
    {
        participantsNumber = _participantsNumber;

        gazeScript = transform.GetChild(1).GetComponent<ViveSR.anipal.Eye.TestFocus>();

        path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
            "/DS_Study_Emotion/" + participantsNumber + "번 피험자/";

        // 폴더 유무 확인
        DirectoryInfo di = new DirectoryInfo(path);

        while (di.Exists)    // ex) trial 1 폴더가 이미 있으면 trial 2 폴더를 생성하게끔 설정 (1, 2 존재 -> 3 생성)
        {
            ++trialNum;
            path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) +
                "/DS_Study_Emotion/" + participantsNumber + "번 피험자/" + trialNum + "/";
            di = new DirectoryInfo(path);
        }

        // 폴더가 없으면 폴더 생성
        if (!di.Exists)
            di.Create();


        HeadMovement = new CsvFileWriter(path + "HeadMovement.csv");
        HeadColumns = new List<string>() { "CurrentTime", "HeadRot_x", "HeadRot_y", "HeadRot_z" };
        WheelchairMovement = new CsvFileWriter(path + "WheelchairMovement.csv");
        WheelchairColumns = new List<string>() { "CurrentTime", "WCPos_x", "WCPos_y", "WCPos_z", "WCRot_x", "WCRot_y", "WCRot_z" };
        CollisionNum = new CsvFileWriter(path + "CollisionNum.csv");
        CollisionColumns = new List<string>() { "participantsNum", "Obstacle", "HillWall" };
        gazeTime = new CsvFileWriter(path + "GazeTime.csv");
        gazeColumns = new List<string>() { "HeadGaze", "BodyGaze", "EnvironmentGaze" };
        FilteredRealGazePosition = new CsvFileWriter(path + "RealGazePosition(Filtered).csv");
        FilteredRealGazecolumns = new List<string>() { "CurrentTime", "RealGazePos_x", "RealGazePos_y", "RealGazePos_z" };
        FilteredEyeMovement = new CsvFileWriter(path + "EyeMovement(Filtered).csv");
        FilteredEyeColumns = new List<string>() { "CurrentTime", "GazePos_x", "GazePos_y" };

        HeadMovement.WriteRow(HeadColumns);
        HeadColumns.Clear();
        WheelchairMovement.WriteRow(WheelchairColumns);
        WheelchairColumns.Clear();
        CollisionNum.WriteRow(CollisionColumns);
        CollisionColumns.Clear();
        gazeTime.WriteRow(gazeColumns);
        gazeColumns.Clear();
        FilteredRealGazePosition.WriteRow(FilteredRealGazecolumns);
        FilteredRealGazecolumns.Clear();
        FilteredEyeMovement.WriteRow(FilteredEyeColumns);
        FilteredEyeColumns.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnded)
        {
            Check_Timer();
            WritingHeadData();
            WritingWheelchairData();
            WritingEyeData();

            if(currentTime > endTime)
            {
                End_Timer();
            }
        }

    }

    public static void StaticGazeTime()
    {
        _headTime = gazeScript.GetHeadGaze();
        _bodyTime = gazeScript.GetBodyGaze();
        //Debug.Log("값 전달했쥬" + _headTime + ", " + gazeScript.GetHeadGaze());
    }

    // Timer 설정
    private void Check_Timer()
    {
        currentTime += Time.deltaTime;
    }

    public static void End_Timer()
    {
        //Debug.Log("End");
        isEnded = true;
        Debug.Log(currentTime);
        WritingCollisionNumber();
        StaticGazeTime();
        OnOffCanvas.EndCanvas();
        WritingGazeTime();
        StaticCoroutine.DoCoroutine(GameClearQuit());
    }

    public static void Start_Timer()
    {
        isEnded = false;
        Debug.Log("Start");
    }

    void WritingHeadData()
    {
        // Head의 Rotation 기록 (Head rotation 값은 inspector 값 그대로 가져옴)
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
        // Head의 Rotation 기록 (Head rotation 값은 inspector 값 그대로 가져옴)
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

    public static void WritingGazeTime()
    {
        gazeColumns.Add(_headTime.ToString());
        gazeColumns.Add(_bodyTime.ToString());
        gazeColumns.Add((currentTime - (_headTime + _bodyTime)).ToString());

        gazeTime.WriteRow(gazeColumns);
        gazeColumns.Clear();

    }

    void WritingEyeData()
    {
        // FilteredData Writing
        gazePosition.GetComponent<Transform>().position = eyeDataManager.realGazePosition;                  // Head rotation + Eye movement 결합된 현재 실제로 보고 있는 곳의 위치.
        Vector2 xyNow = new Vector2(gazePosition.transform.position.x, gazePosition.transform.position.y);  // 화면상 현재 보고 있는 점의 위치.
        Vector2 xyPrevious = new Vector2(previousRealGaze.x, previousRealGaze.y);                           // 화면상 바로 이전 frame에서 봤던 점의 위치.
        var distance = Vector2.Distance(xyNow, xyPrevious);                                                 // 두 점간의 거리. 
        //Debug.Log("Distance: " + distance);
        if (distance < 1.0f)
        {
            timeRealGaze += Time.deltaTime;
            if (timeRealGaze > 0.2f)
            {
                timeRealGaze = 0.0f;

                // Filtered RealGazePosition (Saccadiv movement 제외된 현제 보고있는 위치의 world position)
                FilteredRealGazecolumns.Add(currentTime.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.x.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.y.ToString());
                FilteredRealGazecolumns.Add(eyeDataManager.realGazePosition.z.ToString());

                FilteredRealGazePosition.WriteRow(FilteredRealGazecolumns);
                FilteredRealGazecolumns.Clear();

                // Filtered EyeMovement (Saccadic movement 제외된 현재 보고있는 HMD 렌즈상의 위치)
                FilteredEyeColumns.Add(currentTime.ToString());
                FilteredEyeColumns.Add(eyeDataManager.gazePosition.x.ToString());
                FilteredEyeColumns.Add(eyeDataManager.gazePosition.y.ToString());

                FilteredEyeMovement.WriteRow(FilteredEyeColumns);
                FilteredEyeColumns.Clear();
            }
        }
    }



    private void OnApplicationQuit()
    {
        HeadMovement.Dispose();
        WheelchairMovement.Dispose();
        CollisionNum.Dispose();
        gazeTime.Dispose();
        FilteredRealGazePosition.Dispose();
        FilteredEyeMovement.Dispose();
    }

    // 5초 후 종료 코루틴
    public static IEnumerator GameClearQuit()
    {
        yield return new WaitForSecondsRealtime(5f);
        GameQuit();
    }

    // 게임 종료
    public static void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void LateUpdate()
    {
        previousRealGaze = gazePosition.transform.position;
    }
}
