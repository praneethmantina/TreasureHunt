using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2GameManager : MonoBehaviour
{
    public static Room2GameManager Instance;

    public int dangerTouches = 0;

    public int maxDangerTouches = 3;

    public Renderer statueRenderer;

    public Material originalMaterial;

    public Color winColor;

    private PlayerRespawn respawn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        respawn = FindObjectOfType<PlayerRespawn>();
    }

    public void PlayerHitDanger()
    {
        dangerTouches++;

        if (dangerTouches >= maxDangerTouches)
        {
            dangerTouches = 0;
            respawn.RespawnRoom1();
        }

        else
        {
            respawn.RespawnRoom2();
        }
    }

    public void PlayerReachedFinal()
    {

        VRLocomotion locomotion = FindObjectOfType<VRLocomotion>();
        locomotion.ResetAllWalls();


        statueRenderer.material.color = winColor;
        CharacterController cc = respawn.player.GetComponent<CharacterController>();
        cc.enabled = false;

        Invoke(nameof(RespawnAfterWin), 2f);


    }

    private void RespawnAfterWin()
    {

        statueRenderer.material = originalMaterial;
        dangerTouches = 0;
        respawn.RespawnRoom1();

        CharacterController cc = respawn.player.GetComponent<CharacterController>();
        cc.enabled = true;


    }
}
