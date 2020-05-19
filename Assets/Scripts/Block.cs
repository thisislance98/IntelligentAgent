using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Block : MonoBehaviour
{

    public GameObject ConnectionPrefab;
    public float Activation;
    public float ActivationDecreaseRate = 0.8f;
    public float FieldMin;
    public float FieldMax;

    public float Threshold = .5f;

    public Color BlockColor = Color.blue;

    public List<GameObject> Observers = new List<GameObject>();

    public List<Connection> Connections = new List<Connection>();

    public float FireTime;

    public bool FiredFromSensor;

    public AudioClip SoundClip;

    // float _activationFromConnections = 0;
    protected float _fieldCenter;
    protected float _fieldRadius;

    Vector3 _startScale;

    bool _coolingDown;
    public int BlockIndex;
    static int _currentIndex;

    AudioSource _audio;


    protected virtual void Awake() {
        GetComponent<Renderer>().material.color = BlockColor;
        BlockIndex = _currentIndex;
        _currentIndex++;
        _audio = GetComponent<AudioSource>();
    }

    protected virtual void Start() {
        _startScale = transform.localScale;

        _fieldCenter = (FieldMax + FieldMin) / 2.0f;
        _fieldRadius = Mathf.Abs(FieldMax - _fieldCenter);
        // Debug.Log("Min " + FieldMin + " max: " + FieldMax + " rad: " + _fieldRadius + " center: " + _fieldCenter);
    }

    public void Fire(float activation=1.0f, bool fromSensor=false) {

        if (_coolingDown && fromSensor == false) return;
        
        FireTime = Time.time;
        FiredFromSensor = fromSensor;

        if (fromSensor) {
            foreach(GameObject obj in Observers) {
                obj.SendMessage("OnObservedBlockFire",this);
            }
            
            if (Activation > 0) {
                
                // if we are active from connections and from input then set the threshold to 
                // slighly below the current activation so next time we receive this amount of 
                // activation we fire
                // Threshold = Mathf.Min(Activation * .99f, Threshold);
            }
        }
        else {
            RevertColor();
        }


        Activation = activation;
        _coolingDown = true;
        if (Connections.Count > 0) {
            foreach (Connection c in Connections) {
                c.OnFromBlockFire();
            }
        }

        PlaySound();
        // _activationFromConnections = 0;
    }

    void PlaySound() {
        float transpose = 1;
        _audio.pitch =  Mathf.Pow(2, ((float)BlockIndex+transpose)/12.0f);
        _audio.Play();
    }
    public void OnReceivedConnectionActivation(float increase) {

        if (_coolingDown) return;

        Activation += increase;

        if (Activation >= Threshold) {
            Fire(1,false);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Activation > 0) {
            transform.localScale = _startScale * (1 + Activation);
            Activation -= ActivationDecreaseRate * Time.deltaTime;
            Activation = Mathf.Max(0,Activation);
            if (_coolingDown && Activation == 0) {
                _coolingDown = false;
            }
            
        }        

        // if (_activationFromConnections > 0) {
        //     _activationFromConnections -= ActivationDecreaseRate * Time.deltaTime;
        //     _activationFromConnections = Mathf.Max(0,_activationFromConnections);
        // }
    }

    public void SetFieldMin(float min) {
        FieldMin = min;
    }

    public void SetFieldMax(float max) {
        FieldMax = max;
    }

    public void AddObserver(GameObject observer) {
        Observers.Add(observer);
    }

    // public bool IsConnectedTo(Block block, int timeDiff) {
    //     IEnumerable<Connection> matches = Connections.Where(c => c.ToBlock == block && c.TimeDiff == timeDiff);

    //     return matches.Count() > 0;
    // }

      public Connection GetConnectionTo(Block block, float travelTime) {
        IEnumerable<Connection> matches = Connections.Where(
            c => c.ToBlock == block && 
            Mathf.Abs(c.TravelTime - travelTime) < Connection.TimeWindow);
        
        return matches.Count() > 0 ? (Connection)matches.ElementAt(0) : null;
    }

    public void ConnectTo(Block toBlock, float travelTime) {
        
        GameObject connectionObj = GameObject.Instantiate(ConnectionPrefab);
        connectionObj.transform.parent = transform;

        Connection c = connectionObj.GetComponent<Connection>();
        c.Init(this,toBlock,travelTime);
        Connections.Add(c);
    }
    public void SetColor(Color c, bool setBlockColor=true) {
        if (setBlockColor)
            BlockColor = c;
        GetComponent<Renderer>().material.color = c;
    }

    public void RevertColor() {
        GetComponent<Renderer>().material.color = BlockColor;
    }

    public bool IsCoolingDown() {
        return _coolingDown;
    }
}
