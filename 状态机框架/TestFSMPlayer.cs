using UnityEngine;
using System.Collections;
using PlayerMotionState;
using System;

public enum PlayerStateType : uint
{
    LeftRight = 0,
    UpDown = 1,
    Stop = 2,
}
public enum PlayerFireBulletStateType : uint 
{
    OpenFire=0,
    StopFire=1,
}
public class TestFSMPlayer : MonoBehaviour
{
    [HideInInspector]
    public StateMachine PlayerStateMachine = new StateMachine();
    // Use this for initialization
    void Awake()
    {
        PlayerStateMachine.RegisterState(new LeftRightRunState(this));
        PlayerStateMachine.RegisterState(new UpDownRunState(this));
        PlayerStateMachine.RegisterState(new StopMoveState(this));
        //PlayerStateMachine.SwitchState((uint)StateType.LeftRight, null, null);
    }
    void Start()
    {
        PlayerStateMachine.SwitchState((uint)PlayerStateType.LeftRight, null, null);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStateMachine.OnUpdate();
    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(0.0f, 0.0f, 200, 50), "LeftRight"))
        {
            PlayerStateMachine.SwitchState((uint)PlayerStateType.LeftRight, null, null);
        }
        if (GUI.Button(new Rect(0.0f, 110.0f, 200, 50), "UpDown"))
        {
            PlayerStateMachine.SwitchState((uint)PlayerStateType.UpDown, null, null);
        }
        if (GUI.Button(new Rect(0f, 220f, 200, 50), "Stop"))
        {
            PlayerStateMachine.SwitchState((uint)PlayerStateType.Stop, null, null);
        }
    }

    void FixedUpdate()
    {
        PlayerStateMachine.OnFixedUpdate();
    }
    void LeteUpdate()
    {
        PlayerStateMachine.OnLeteUpdate();
    }
}
namespace PlayerMotionState
{
    
    public class StopMoveState : IState
    {
        TestFSMPlayer mPlayer = null;
        /// <summary>
        /// 停止移动状态
        /// </summary>
        public StopMoveState(TestFSMPlayer player)
        {
            mPlayer = player;
        }
        public uint GetStateID()
        {
            return (uint)PlayerStateType.Stop;
        }

        public void OnEnter(StateMachine machine, IState prevState, object param1, object param2)
        {
            Debug.Log("StopMove OnEnter");
        }

        public void OnFixedUpdate()
        {

        }

        public void OnLeave(IState nextState, object param1, object param2)
        {
            Debug.Log("离开停止移动状态");
        }

        public void OnleteUpdate()
        {
        }

        public void OnUpdate()
        {
            Debug.Log("停止移动");
        }
    }
    
    public class UpDownRunState : IState
    {

        TestFSMPlayer mPlayer = null;
        /// <summary>
        /// 上下移动状态
        /// </summary>
        public UpDownRunState(TestFSMPlayer player)
        {
            mPlayer = player;
        }

        public uint GetStateID()
        {
            return (uint)PlayerStateType.UpDown;
        }

        public void OnEnter(StateMachine machine, IState prevState, object param1, object param2)
        {
            Debug.Log("UpDownRunState OnEnter");
        }

        public void OnLeave(IState nextState, object param1, object param2)
        {
            Debug.Log("UpDownRunState OnLeave");
        }
        bool isUp = false;
        public void OnUpdate()
        {
            if (isUp)
            {
                mPlayer.transform.position += new UnityEngine.Vector3(0f, 0.1f);
                if (mPlayer.transform.position.y > 1.5f)
                {
                    isUp = false;
                }
            }
            else
            {
                mPlayer.transform.position -= new UnityEngine.Vector3(0f, 0.1f);
                if (mPlayer.transform.position.y < -1.5f)
                {
                    isUp = true;
                }
            }
        }

        public void OnFixedUpdate()
        {

        }

        public void OnleteUpdate()
        {

        }
    }
    
    public class LeftRightRunState : IState
    {
        TestFSMPlayer mPlayer = null;
        /// <summary>
        /// 左右移动状态
        /// </summary>
        public LeftRightRunState(TestFSMPlayer player)
        {
            mPlayer = player;
        }

        public uint GetStateID()
        {
            return (uint)PlayerStateType.LeftRight;
        }

        public void OnEnter(StateMachine machine, IState prevState, object param1, object param2)
        {
            Debug.Log("LeftRightRunState OnEnter");
        }

        public void OnLeave(IState nextState, object param1, object param2)
        {
            Debug.Log("LeftRightRunState OnLeave");
        }
        private bool isLeft = true;
        public void OnUpdate()
        {
            if (isLeft)
            {
                mPlayer.transform.position += new UnityEngine.Vector3(0.1f, 0.0f);
                if (mPlayer.transform.position.x > 1.5f)
                {
                    isLeft = false;
                }
            }
            else
            {
                mPlayer.transform.position -= new UnityEngine.Vector3(0.1f, 0.0f);
                if (mPlayer.transform.position.x < -1.5f)
                {
                    isLeft = true;
                }
            }
        }

        void IState.OnFixedUpdate()
        {

        }

        void IState.OnleteUpdate()
        {

        }
    }
}