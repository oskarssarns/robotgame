using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementType : MonoBehaviour
{
    public Status changeTo;

    protected PlayerController player;
    protected PlayerMovement movement;
    protected PlayerInput playerInput;
    protected Status playerStatus;

    public virtual void Start()
    {
        if(!GetComponent<PlayerController>().view.IsMine){this.enabled = false; return;}
        player = GetComponent<PlayerController>();

        player.AddMovementType(this);
        player.AddToStatusChange(PlayerStatusChange);
    }

    public virtual void SetPlayerComponents(PlayerMovement move, PlayerInput input)
    {
        movement = move; playerInput = input;
    }

    public virtual void PlayerStatusChange(Status status, Func<IKData> call)
    {
        playerStatus = status;
    }

    public virtual void Movement()
    {
        //Movement info
    }

    public virtual void Check(bool canInteract)
    {
        //Check info
    }

    public virtual IKData IK()
    {
        IKData data = new IKData();
        return data;
    }
}
