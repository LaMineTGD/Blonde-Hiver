using UnityEngine;
using extOSC;
using UnityEngine.SceneManagement;

public class MacroManager : MonoBehaviour
{
    public static MacroManager Instance { get; private set; }

    [HideInInspector] public float AbletonFactor = 0.00390625f;

    public bool m_DebugMode;
    public GameObject m_DebugUI;

    void Start()
    {
        if(m_DebugMode)
        {
            m_DebugUI.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //SceneManager.LoadScene(0);
        }
    }
}