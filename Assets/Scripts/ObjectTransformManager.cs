using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using RetroAesthetics;
using extOSC;

public class ObjectTransformManager : MonoBehaviour
{
    public bool m_CameraRotation = false;
    public bool m_CameraRotateAround = false;
    public float m_RotationFactor = -6.4f;
    public UVScroller m_UVScroller;
    public OSCReceiver m_OSCReceiver;
    [Header("Game Objects")]
    public Transform m_CenterPlanetRotator;
    public Transform m_CenterScreenRotator;
    public Transform m_Halo;
    public Transform m_Sun;
    public Transform m_Roof;
    public Transform m_Billboard;
    [Header("Prefabs")]
    public GameObject m_CityBlock;
    public GameObject m_CityLight;
    public GameObject m_Car;
    public GameObject m_ForestBlock;

    float m_Speed;
    Dictionary<GameObject, float> m_PassingObjects = new Dictionary<GameObject, float>();
    PostProcessVolume _PostProcessVolume;
    ColorGrading _ColorGrading;

    void Start()
    {
        _PostProcessVolume = Camera.main.GetComponent<PostProcessVolume>();
        _ColorGrading = _PostProcessVolume.sharedProfile.GetSetting<ColorGrading>();

        m_OSCReceiver.Bind("/paysage", OSCPaysage);
        m_OSCReceiver.Bind("/camera", OSCCamera);

        //StartCoroutine(m_PostProcessColor(3, new Color(0,0,0)));
        //StartCoroutine(LerpPosition(Camera.main.transform, new Vector3(17,302,20), 3));
        //StartCoroutine(LerpRotation(Camera.main.transform, Quaternion.Euler(new Vector3(47, 39, 0)), 3));
        //StartCoroutine(LerpScale(m_Halo, 10, 3));
        //ToggleVisibility(m_Sun.gameObject);

        StartCoroutine("m_CityBlockCoroutine");
        StartCoroutine("m_ForestBlockCoroutine");
        StartCoroutine("m_CityLightCoroutine");
        StartCoroutine("m_CarCoroutine");
    }

    void Update()
    {
        m_Speed = m_UVScroller.scrollSpeed.x / m_RotationFactor;

        List<GameObject> _ElementsToDelete = new List<GameObject>();
        foreach (KeyValuePair<GameObject,float> _PassingObject in m_PassingObjects)
        {
            _PassingObject.Key.transform.RotateAround(m_CenterPlanetRotator.position, Vector3.right, _PassingObject.Value * Time.deltaTime);
            if (_PassingObject.Key.transform.position.y <= 0f)
            {
                _ElementsToDelete.Add(_PassingObject.Key);
            }
        }

        foreach(GameObject _ObjectToDelete in _ElementsToDelete)
        {
            m_PassingObjects.Remove(_ObjectToDelete);
            Destroy(_ObjectToDelete);
        }

        if (Input.GetKeyDown("space"))
        {
            Debug.Log("hey");
        }

        if (m_CameraRotation)
            Camera.main.transform.Rotate(Vector3.up * m_Speed * Time.deltaTime);
        if(m_CameraRotateAround)
            Camera.main.transform.RotateAround(m_CenterScreenRotator.position, Vector3.up, m_Speed * Time.deltaTime);
    }

    void ToggleVisibility(GameObject g)
    {
        g.SetActive(!g.activeSelf);
    }

    IEnumerator LerpPosition(Transform _Target, Vector3 _TargetValue, float _Duration)
    {
        float _Time = 0;
        Vector3 startPosition = _Target.position;
        while (_Time < _Duration)
        {
            _Target.position = Vector3.Lerp(startPosition, _TargetValue, _Time / _Duration);
            _Time += Time.deltaTime;
            yield return null;
        }
        _Target.position = _TargetValue;
    }

    IEnumerator LerpRotation(Transform _Target, Quaternion _TargetValue, float _Duration)
    {
        float _Time = 0;
        Quaternion _StartValue = _Target.rotation;
        while (_Time < _Duration)
        {
            _Target.rotation = Quaternion.Lerp(_StartValue, _TargetValue, _Time / _Duration);
            _Time += Time.deltaTime;
            yield return null;
        }
        _Target.rotation = _TargetValue;
    }

    IEnumerator LerpScale(Transform _Target, float _TargetValue, float _Duration)
    {
        float _Time = 0;
        float _ScaleModifier = 1;
        float _StartValue = _ScaleModifier;
        Vector3 _StartScale = _Target.localScale;
        while (_Time < _Duration)
        {
            _ScaleModifier = Mathf.Lerp(_StartValue, _TargetValue, _Time / _Duration);
            _Target.localScale = _StartScale * _ScaleModifier;
            _Time += Time.deltaTime;
            yield return null;
        }

        _Target.localScale = _StartScale * _TargetValue;
        _ScaleModifier = _TargetValue;
    }

    IEnumerator m_CityBlockCoroutine()
    {
        float _Time = 0;
        bool _DumbFix = true;
        m_PassingObjects.Add(Instantiate(m_CityBlock), -m_Speed);
        while (_Time < 9)
        {
            if (_Time > 0 && _DumbFix)
            {
                m_PassingObjects.Add(Instantiate(m_CityBlock), -m_Speed);
                _DumbFix = false;
            }
            _Time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("m_CityBlockCoroutine");
    }

    IEnumerator m_CityLightCoroutine()
    {
        float _Time = 0;
        m_PassingObjects.Add(Instantiate(m_CityLight), -m_Speed);
        while (_Time < 4)
        {
            _Time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("m_CityLightCoroutine");
    }

    IEnumerator m_CarCoroutine()
    {
        float _Time = 0;
        m_PassingObjects.Add(Instantiate(m_Car), -m_Speed*2);
        while (_Time < 5)
        {
            _Time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("m_CarCoroutine");
    }

    IEnumerator m_ForestBlockCoroutine()
    {
        float _Time = 0;
        m_PassingObjects.Add(Instantiate(m_ForestBlock), -m_Speed);
        while (_Time < 1)
        {
            _Time += Time.deltaTime;
            yield return null;
        }
        StartCoroutine("m_ForestBlockCoroutine");
    }

    IEnumerator m_PostProcessColor(float _Duration, Color _TargetColor)
    {
        float _Time = 0;
        Color _StartValue = _ColorGrading.colorFilter.value;
        while (_Time < _Duration)
        {
            _ColorGrading.colorFilter.value = Color.Lerp(_StartValue, _TargetColor, _Time / _Duration);
            _Time += Time.deltaTime;
            yield return null;
        }
        _ColorGrading.colorFilter.value = _TargetColor;
    }

    public void ChangeSpeed(float _NewSpeed)
    {
        m_Speed = _NewSpeed / m_RotationFactor;
    }
    void OSCPaysage(OSCMessage message)
    {
        switch (message.Values[0].FloatValue * 256)
        {
            case 0:
                StartCoroutine("m_CityBlockCoroutine");
                break;
            case 1:
                StopCoroutine("m_CityBlockCoroutine");
                break;
            case 2:
                StartCoroutine("m_ForestBlockCoroutine");
                break;
            case 3:
                StopCoroutine("m_ForestBlockCoroutine");
                break;
            case 4:
                StartCoroutine("m_CityLightCoroutine");
                break;
            case 5:
                StopCoroutine("m_CityLightCoroutine");
                break;
            case 6:
                StartCoroutine("m_CarCoroutine");
                break;
            case 7:
                StopCoroutine("m_CarCoroutine");
                break;
            case 8:
                m_Billboard.gameObject.SetActive(true);
                break;
            case 9:
                m_Billboard.gameObject.SetActive(false);
                break;
            case 10:
                m_Roof.gameObject.SetActive(true);
                break;
            case 11:
                m_Roof.gameObject.SetActive(false);
                break;
            default:
                print("Paysage : Pas la bonne valeur");
                break;
        }
    }

    void OSCCamera(OSCMessage message)
    {
        switch (message.Values[0].FloatValue * 256)
        {
            case 0:
                m_CameraRotation = false;
                m_CameraRotateAround = false;
                StartCoroutine(LerpPosition(Camera.main.transform, new Vector3(0,256,-35), 3));
                StartCoroutine(LerpRotation(Camera.main.transform, Quaternion.Euler(new Vector3(0, 0, 0)), 3));
                break;
            case 1:
                m_CameraRotation = false;
                m_CameraRotateAround = true;
                StartCoroutine(LerpPosition(Camera.main.transform, new Vector3(0, 256, -35), 3));
                StartCoroutine(LerpRotation(Camera.main.transform, Quaternion.Euler(new Vector3(0, 0, 0)), 3));
                break;
            case 2:
                m_CameraRotation = true;
                m_CameraRotateAround = false;
                StartCoroutine(LerpPosition(Camera.main.transform, new Vector3(0, 256, -35), 3));
                StartCoroutine(LerpRotation(Camera.main.transform, Quaternion.Euler(new Vector3(0, 0, 0)), 3));
                break;
            case 3:
                m_CameraRotation = false;
                m_CameraRotateAround = false;
                StartCoroutine(LerpPosition(Camera.main.transform, new Vector3(17, 302, 20), 3));
                StartCoroutine(LerpRotation(Camera.main.transform, Quaternion.Euler(new Vector3(47, 39, 0)), 3));
                break;
            default:
                print("Caméra : Pas la bonne valeur");
                break;
        }
    }
}