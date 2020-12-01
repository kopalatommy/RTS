﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    private Renderer rend = null;

    public int teamCode = 0;
    [SerializeField] private AttackableObject toSpawn = null;

    bool canPlace = false;

    [SerializeField] private GameObject rangeMarker = null;
    [SerializeField] private float distBetween = 5;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rangeMarker.transform.localScale = new Vector3(distBetween, distBetween, 1);
        //rangeMarker.SetActive(false);
    }

    public void Populate(int TeamCode)
    {
        teamCode = TeamCode;
    }

    public bool CanPlace()
    {
        return canPlace;
    }

    private void Update()
    {
        canPlace = CombatHandler.instance.CanPlace(new Vector2(transform.position.x, transform.position.z), distBetween);

        rend.material.color = canPlace ? Color.green : Color.red;

        if (Input.GetKey(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    public bool TryPlace()
    {
        if (canPlace)
        {
            GameObject g = Instantiate(toSpawn.gameObject, transform.position, transform.rotation);
            g.GetComponent<AttackableObject>().Populate(teamCode);
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}