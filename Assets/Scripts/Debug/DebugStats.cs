using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

[ExecuteInEditMode]

public class DebugStats : SingletonBehaviour <DebugStats> {

    public bool isShowInEditor = true;
    public bool isShowMenu = false;
    public int fontSize = 12;
    public Color fontColor = Color.white;

    private bool isMenuOpen = false;
    private bool se = true;
    private bool bgm = true;
    private int usedMemSize;
    private int peakMemSize;
    private float lastCollNum;
    private float lastAllocSet = -9999.0f;
    private GUIStyle style;
    private int debugGUIDepth = -1001;

    protected override void Awake() {
        base.Awake();
        //DontDestroyOnLoad(this);
	}

    void Start () {
        CreateGUIStyle();
	}

	void OnGUI () {
        if (!Application.isPlaying && !isShowInEditor) {
            return;
        } else {
            ProfileStats();
            DrawStats();
            if (isShowMenu) {
                DrawMenu();
            }
        }
    }

    void CreateGUIStyle(){
        style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = fontColor;
    }

    void ProfileStats () {
        lastCollNum = System.GC.CollectionCount(0);
        usedMemSize = (int)System.GC.GetTotalMemory(false);

        if (usedMemSize > peakMemSize) {
            peakMemSize = usedMemSize;
        }

        if ((Time.realtimeSinceStartup - lastAllocSet) > 0.3f) {
            lastAllocSet = Time.realtimeSinceStartup;
        }
    }

    void DrawStats () {
        var text = new StringBuilder();

        // Memory
        text.Append("Memory\t\t")
            .Append(((float)usedMemSize / 1048576).ToString("0.00")
                     + " / " + SystemInfo.systemMemorySize + " MB")
            .AppendLine(" ( max " + ((float)peakMemSize / 1048576).ToString("0.00") + " MB )")
            .Append("VRAM\t\t")
            .Append(SystemInfo.graphicsMemorySize + " MB")
            .AppendLine();

        // FrameRate
        text.Append("FPS\t\t")
            .Append((1.0 / Time.unscaledDeltaTime).ToString("0.0")  + " fps / " + Application.targetFrameRate)
            .AppendLine();

        GUI.Label(new Rect(5, 0, 1000, 200), text.ToString(), style);
    }

    void DrawMenu () {
        if (isMenuOpen) {
            GUI.Box(new Rect(Screen.width-240, -5, 100, Screen.height+10), "");
            if (GUI.Button(new Rect(Screen.width - 230, -5, 80, 50), "Close")) {
                isMenuOpen = false;
            }

            if (GUI.Button(new Rect(Screen.width - 230, 90, 80, 50), "Reload")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            }

            if (GUI.Button(new Rect(Screen.width - 230, 160, 80, 50), "Next")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            if (GUI.Button(new Rect(Screen.width - 230, 230, 80, 50), "Previous")){
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }

            se = GUI.Toggle(new Rect(Screen.width - 230, 370, 80, 50), se, "SE");
            bgm = GUI.Toggle(new Rect(Screen.width - 230, 430, 80, 50), bgm, "BGM");

            if (GUI.Button(new Rect(Screen.width - 230, Screen.height - 45, 80, 50), "ReStart")) {
                SceneManager.LoadScene(0);
            }
        } else {
            if (GUI.Button(new Rect(Screen.width-230, -5, 80, 50), "Menu" ) ) {
                isMenuOpen = true;
            }
        }
    }
}
