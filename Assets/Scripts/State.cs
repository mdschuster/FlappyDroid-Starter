using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public enum STATE
    {
        PLAY, GAMEOVER
    }

    public enum EVENT
    {
        EXIT, UPDATE, ENTER
    }

    public STATE name;
    protected EVENT stage;
    protected State nextState;

    public State()
    {

    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

}

public class Play : State
{
    public Play() : base()
    {
        name = STATE.PLAY;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        GameManager.instance().reset();
        base.Enter();
    }

    public override void Update()
    {
        // if (GameManager.instance().dead == true)
        // {
        //     stage = EVENT.EXIT;
        //     nextState = new GameOver();
        //     return;
        // }


        GameManager.instance().spawnPipe();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class GameOver : State
{
    public GameOver() : base()
    {
        name = STATE.GAMEOVER;
        stage = EVENT.ENTER;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stage = EVENT.EXIT;
            nextState = new Play();
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
