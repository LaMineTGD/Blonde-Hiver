using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using extOSC;
using UnityEngine.Rendering.PostProcessing;

public class ColorChanger : MonoBehaviour
{
    private ColorGrading m_ColorGrading;

    public Light m_PrimaryLight;
    public Light m_SecondaryLight;
    public MeshRenderer m_GridSphere;
    public MeshRenderer m_Roof;
    public Terrain m_Terrain;
    public SpriteRenderer m_Halo;
    public SpriteRenderer m_Sun;
    public Text m_BlondeText;
    public Text m_HiverText;

    [Header("Color Collection")]
    public Color m_Blue01;
    public Color m_Blue02;
    public Color m_Blue03;
    public Color m_BlueGlow;
    public Color m_Red01;
    public Color m_Red02;
    public Color m_Red03;
    public Color m_RedGlow;
    public Color m_Green01;
    public Color m_Green02;
    public Color m_Green03;
    public Color m_GreenGlow;

    public OSCReceiver m_OSCReceiver;

    void Start()
    {
        m_Terrain.materialTemplate.SetColor("_MainColor", Color.black);
        m_Terrain.materialTemplate.SetColor("_LineColor", m_Blue02);
        m_Terrain.materialTemplate.SetColor("_EmissionColor", m_Blue03);

        m_OSCReceiver.Bind("/logoR", LogoR);
        m_OSCReceiver.Bind("/logoG", LogoG);
        m_OSCReceiver.Bind("/logoB", LogoB);
        m_OSCReceiver.Bind("/logoA", LogoA);

        m_OSCReceiver.Bind("/sunR", SunR);
        m_OSCReceiver.Bind("/sunG", SunG);
        m_OSCReceiver.Bind("/sunB", SunB);
        m_OSCReceiver.Bind("/sunA", SunA);

        m_OSCReceiver.Bind("/haloR", HaloR);
        m_OSCReceiver.Bind("/haloG", HaloG);
        m_OSCReceiver.Bind("/haloB", HaloB);
        m_OSCReceiver.Bind("/haloA", HaloA);

        m_OSCReceiver.Bind("/color1R", Color1R);
        m_OSCReceiver.Bind("/color1G", Color1G);
        m_OSCReceiver.Bind("/color1B", Color1B);
        m_OSCReceiver.Bind("/color1Gain", Color1Gain);

        m_OSCReceiver.Bind("/color2R", Color2R);
        m_OSCReceiver.Bind("/color2G", Color2G);
        m_OSCReceiver.Bind("/color2B", Color2B);

        m_OSCReceiver.Bind("/colorGskyR", ColorGskyR);
        m_OSCReceiver.Bind("/colorGskyG", ColorGskyG);
        m_OSCReceiver.Bind("/colorGskyB", ColorGskyB);
        m_OSCReceiver.Bind("/colorGskyA", ColorGskyA);

        m_ColorGrading = Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ColorGrading>();
        m_ColorGrading.colorFilter.value = Color.black;
    }

    void Update()
    {

    }

    public void StartingFade()
    {
        StartCoroutine(LerpColor("PostProcess", Color.white, 5f));
    }

    IEnumerator LerpColor(string _ObjectToChange, Color _TargetColor, float _LerpDuration)
    {
        float _TimeElapsed = 0;
        Color _StartColor = new Color();

        switch (_ObjectToChange)
        {
            case "PrimaryLight":
                _StartColor = m_PrimaryLight.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_PrimaryLight.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_PrimaryLight.color;
                }
                m_PrimaryLight.color = _TargetColor;
                yield return m_PrimaryLight.color;
                break;
            case "SecondaryLight":
                _StartColor = m_SecondaryLight.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_SecondaryLight.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_SecondaryLight.color;
                }
                m_SecondaryLight.color = _TargetColor;
                yield return m_SecondaryLight.color;
                break;
            case "GridMain":
                _StartColor = m_GridSphere.material.GetColor("_MainColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_GridSphere.material.SetColor("_MainColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_GridSphere.material.GetColor("_MainColor");
                }
                m_GridSphere.material.SetColor("_MainColor", _TargetColor);
                yield return m_GridSphere.material.GetColor("_MainColor");
                break;
            case "GridLine":
                _StartColor = m_GridSphere.material.GetColor("_LineColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_GridSphere.material.SetColor("_LineColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_GridSphere.material.GetColor("_LineColor");
                }
                m_GridSphere.material.SetColor("_LineColor", _TargetColor);
                yield return m_GridSphere.material.GetColor("_LineColor");
                break;
            case "GridEmission":
                _StartColor = m_GridSphere.material.GetColor("_EmissionColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_GridSphere.material.SetColor("_EmissionColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_GridSphere.material.GetColor("_EmissionColor");
                }
                m_GridSphere.material.SetColor("_EmissionColor", _TargetColor);
                yield return m_GridSphere.material.GetColor("_EmissionColor");
                break;
            case "TerrainMain":
                _StartColor = m_Terrain.materialTemplate.GetColor("_MainColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_Terrain.materialTemplate.SetColor("_MainColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_Terrain.materialTemplate.GetColor("_MainColor");
                }
                m_Terrain.materialTemplate.SetColor("_EmissionColor", _TargetColor);
                yield return m_Terrain.materialTemplate.GetColor("_EmissionColor");
                break;
            case "TerrainLine":
                _StartColor = m_Terrain.materialTemplate.GetColor("_LineColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_Terrain.materialTemplate.SetColor("_LineColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_Terrain.materialTemplate.GetColor("_LineColor");
                }
                m_Terrain.materialTemplate.SetColor("_EmissionColor", _TargetColor);
                yield return m_Terrain.materialTemplate.GetColor("_EmissionColor");
                break;
            case "TerrainEmission":
                _StartColor = m_Terrain.materialTemplate.GetColor("_EmissionColor");
                while (_TimeElapsed < _LerpDuration)
                {
                    m_Terrain.materialTemplate.SetColor("_EmissionColor", Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration));
                    _TimeElapsed += Time.deltaTime;
                    yield return m_Terrain.materialTemplate.GetColor("_EmissionColor");
                }
                m_Terrain.materialTemplate.SetColor("_EmissionColor", _TargetColor);
                yield return m_Terrain.materialTemplate.GetColor("_EmissionColor");
                break;
            case "Halo":
                _StartColor = m_Halo.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_Halo.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_Halo.color;
                }
                m_Halo.color = _TargetColor;
                yield return m_Halo.color;
                break;
            case "Sun":
                _StartColor = m_Sun.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_Sun.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_Sun.color;
                }
                m_Sun.color = _TargetColor;
                yield return m_Sun.color;
                break;
            case "Blonde":
                _StartColor = m_BlondeText.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_BlondeText.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_BlondeText.color;
                }
                m_BlondeText.color = _TargetColor;
                yield return m_BlondeText.color;
                break;
            case "Hiver":
                _StartColor = m_HiverText.color;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_HiverText.color = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_HiverText.color;
                }
                m_HiverText.color = _TargetColor;
                yield return m_HiverText.color;
                break;
            case "PostProcess":
                _StartColor = Color.black;
                while (_TimeElapsed < _LerpDuration)
                {
                    m_ColorGrading.colorFilter.value = Color.Lerp(_StartColor, _TargetColor, _TimeElapsed / _LerpDuration);
                    _TimeElapsed += Time.deltaTime;
                    yield return m_ColorGrading.colorFilter.value;
                }
                m_ColorGrading.colorFilter.value = _TargetColor;
                yield return m_ColorGrading.colorFilter.value;
                break;
        }
    }

    void GoRed()
    {
        //StartCoroutine(LerpColor("Hiver", new Color(255f/255f,197f/255f,23f/255f), 2f));
        StartCoroutine(LerpColor("PrimaryLight", m_Red03, 5f));
        StartCoroutine(LerpColor("SecondaryLight", m_Red02, 5f));
        StartCoroutine(LerpColor("GridMain", m_Red01, 5f));
        StartCoroutine(LerpColor("GridLine", m_Red02, 5f));
        StartCoroutine(LerpColor("GridEmission", m_Red03, 5f));
        StartCoroutine(LerpColor("TerrainLine", m_Red02, 5f));
        StartCoroutine(LerpColor("TerrainEmission", m_Red03, 5f));
        StartCoroutine(LerpColor("Halo", m_RedGlow, 5f));
        StartCoroutine(LerpColor("Sun", m_Red03, 5f));
    }

    void GoGreen()
    {
        //StartCoroutine(LerpColor("Hiver", new Color(255f/255f,197f/255f,23f/255f), 2f));
        StartCoroutine(LerpColor("PrimaryLight", m_Green03, 5f));
        StartCoroutine(LerpColor("SecondaryLight", m_Green02, 5f));
        StartCoroutine(LerpColor("GridMain", m_Green01, 5f));
        StartCoroutine(LerpColor("GridLine", m_Green02, 5f));
        StartCoroutine(LerpColor("GridEmission", m_Green03, 5f));
        StartCoroutine(LerpColor("TerrainLine", m_Green02, 5f));
        StartCoroutine(LerpColor("TerrainEmission", m_Green03, 5f));
        StartCoroutine(LerpColor("Halo", m_GreenGlow, 5f));
        StartCoroutine(LerpColor("Sun", m_Green03, 5f));
    }

    void GoBlue()
    {
        //StartCoroutine(LerpColor("Hiver", new Color(255f/255f,197f/255f,23f/255f), 2f));
        StartCoroutine(LerpColor("PrimaryLight", m_Blue03, 5f));
        StartCoroutine(LerpColor("SecondaryLight", m_Blue02, 5f));
        StartCoroutine(LerpColor("GridMain", m_Blue01, 5f));
        StartCoroutine(LerpColor("GridLine", m_Blue02, 5f));
        StartCoroutine(LerpColor("GridEmission", m_Blue03, 5f));
        StartCoroutine(LerpColor("TerrainLine", m_Blue02, 5f));
        StartCoroutine(LerpColor("TerrainEmission", m_Blue03, 5f));
        StartCoroutine(LerpColor("Halo", m_BlueGlow, 5f));
        StartCoroutine(LerpColor("Sun", m_Blue03, 5f));
    }

    void LogoR(OSCMessage message)
    {
        m_BlondeText.color = new Color(message.Values[0].FloatValue, m_BlondeText.color.g, m_BlondeText.color.b, m_BlondeText.color.a);
        m_HiverText.color = new Color(message.Values[0].FloatValue, m_HiverText.color.g, m_HiverText.color.b, m_HiverText.color.a);
    }

    void LogoG(OSCMessage message)
    {
        m_BlondeText.color = new Color(m_BlondeText.color.r, message.Values[0].FloatValue, m_BlondeText.color.b, m_BlondeText.color.a);
        m_HiverText.color = new Color(m_HiverText.color.r, message.Values[0].FloatValue, m_HiverText.color.b, m_HiverText.color.a);
    }

    void LogoB(OSCMessage message)
    {
        m_BlondeText.color = new Color(m_BlondeText.color.r, m_BlondeText.color.g, message.Values[0].FloatValue, m_BlondeText.color.a);
        m_HiverText.color = new Color(m_HiverText.color.r, m_HiverText.color.g, message.Values[0].FloatValue, m_HiverText.color.a);
    }

    void LogoA(OSCMessage message)
    {
        m_BlondeText.color = new Color(m_BlondeText.color.r, m_BlondeText.color.g, m_BlondeText.color.b, message.Values[0].FloatValue);
        m_HiverText.color = new Color(m_HiverText.color.r, m_HiverText.color.g, m_HiverText.color.b, message.Values[0].FloatValue);
    }

    void SunR(OSCMessage message)
    {
        m_Sun.color = new Color(message.Values[0].FloatValue, m_Sun.color.g, m_Sun.color.b, m_Sun.color.a);
    }

    void SunG(OSCMessage message)
    {
        m_Sun.color = new Color(m_Sun.color.r, message.Values[0].FloatValue, m_Sun.color.b, m_Sun.color.a);
    }

    void SunB(OSCMessage message)
    {
        m_Sun.color = new Color(m_Sun.color.r, m_Sun.color.g, message.Values[0].FloatValue, m_Sun.color.a);
    }

    void SunA(OSCMessage message)
    {
        m_Sun.color = new Color(m_Sun.color.r, m_Sun.color.g, m_Sun.color.b, message.Values[0].FloatValue);
    }

    void HaloR(OSCMessage message)
    {
        m_Halo.color = new Color(message.Values[0].FloatValue, m_Halo.color.g, m_Halo.color.b, m_Halo.color.a);
    }

    void HaloG(OSCMessage message)
    {
        m_Halo.color = new Color(m_Halo.color.r, message.Values[0].FloatValue, m_Halo.color.b, m_Halo.color.a);
    }

    void HaloB(OSCMessage message)
    {
        m_Halo.color = new Color(m_Halo.color.r, m_Halo.color.g, message.Values[0].FloatValue, m_Halo.color.a);
    }

    void HaloA(OSCMessage message)
    {
        m_Halo.color = new Color(m_Halo.color.r, m_Halo.color.g, m_Halo.color.b, message.Values[0].FloatValue);
    }

    void Color1R(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(message.Values[0].FloatValue, m_GridSphere.material.GetColor("_EmissionColor").g, m_GridSphere.material.GetColor("_EmissionColor").b));
        m_Terrain.materialTemplate.SetColor("_EmissionColor", new Color(message.Values[0].FloatValue, m_Terrain.materialTemplate.GetColor("_EmissionColor").g, m_Terrain.materialTemplate.GetColor("_EmissionColor").b));
        m_PrimaryLight.color = new Color(message.Values[0].FloatValue, m_PrimaryLight.color.g, m_PrimaryLight.color.b);
    }

    void Color1G(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(m_GridSphere.material.GetColor("_EmissionColor").r, message.Values[0].FloatValue, m_GridSphere.material.GetColor("_EmissionColor").b));
        m_Terrain.materialTemplate.SetColor("_EmissionColor", new Color(m_GridSphere.material.GetColor("_EmissionColor").r, message.Values[0].FloatValue, m_Terrain.materialTemplate.GetColor("_EmissionColor").b));
        m_PrimaryLight.color = new Color(m_PrimaryLight.color.r, message.Values[0].FloatValue, m_PrimaryLight.color.b);
    }

    void Color1B(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(m_GridSphere.material.GetColor("_EmissionColor").r, m_GridSphere.material.GetColor("_EmissionColor").g, message.Values[0].FloatValue));
        m_Terrain.materialTemplate.SetColor("_EmissionColor", new Color(m_GridSphere.material.GetColor("_EmissionColor").r, m_Terrain.materialTemplate.GetColor("_EmissionColor").g, message.Values[0].FloatValue));
        m_PrimaryLight.color = new Color(m_PrimaryLight.color.r, m_PrimaryLight.color.g, message.Values[0].FloatValue);
    }

    void Color1Gain(OSCMessage message)
    {
        m_GridSphere.material.SetFloat("_EmissionGain", message.Values[0].FloatValue);
    }

    void Color2R(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(message.Values[0].FloatValue, m_Terrain.materialTemplate.GetColor("_EmissionColor").g, m_Terrain.materialTemplate.GetColor("_EmissionColor").b));
        m_SecondaryLight.color = new Color(message.Values[0].FloatValue, m_SecondaryLight.color.g, m_SecondaryLight.color.b);
    }

    void Color2G(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(m_Terrain.materialTemplate.GetColor("_EmissionColor").r, message.Values[0].FloatValue, m_Terrain.materialTemplate.GetColor("_EmissionColor").b));
        m_SecondaryLight.color = new Color(m_SecondaryLight.color.r, message.Values[0].FloatValue, m_SecondaryLight.color.b);
    }

    void Color2B(OSCMessage message)
    {
        m_GridSphere.material.SetColor("_EmissionColor", new Color(m_Terrain.materialTemplate.GetColor("_EmissionColor").r, m_Terrain.materialTemplate.GetColor("_EmissionColor").g, message.Values[0].FloatValue));
        m_SecondaryLight.color = new Color(m_SecondaryLight.color.r, m_SecondaryLight.color.g, message.Values[0].FloatValue);
    }

    void ColorGskyR(OSCMessage message)
    {
        m_Roof.material.SetColor("_EmissionColor", new Color(message.Values[0].FloatValue, m_Roof.material.GetColor("_EmissionColor").g, m_Roof.material.GetColor("_EmissionColor").b));
    }

    void ColorGskyG(OSCMessage message)
    {
        m_Roof.material.SetColor("_EmissionColor", new Color(m_Roof.material.GetColor("_EmissionColor").r, message.Values[0].FloatValue, m_Roof.material.GetColor("_EmissionColor").b));
    }

    void ColorGskyB(OSCMessage message)
    {
        m_Roof.material.SetColor("_EmissionColor", new Color(m_Roof.material.GetColor("_EmissionColor").r, m_Roof.material.GetColor("_EmissionColor").g, message.Values[0].FloatValue));
    }

    void ColorGskyA(OSCMessage message)
    {
        m_Roof.material.SetColor("_Color", new Color(m_Roof.material.GetColor("_Color").r, m_Roof.material.GetColor("_Color").g, m_Roof.material.GetColor("_Color").b, message.Values[0].FloatValue));
    }
}