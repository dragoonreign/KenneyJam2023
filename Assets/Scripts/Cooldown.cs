using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    public float CDTimer = 0;
    public float MaxCDTimer = 5f;
    public bool CDStarted;
    public bool CDReset; // if true reset settings

    public void CDStart(float NewMaxCDTimer)
    {
        CDStarted = true;
        CDReset = false;
        MaxCDTimer = NewMaxCDTimer;
    }

    public void CDEnd()
    {
        CDTimer = 0;
        CDStarted = false;
        CDReset = true;
    }

    public bool CDBool()
    {
        return CDReset;
    }

    public void UpdateCooldown(Cooldown m_CooldownManager)
    {
        //if cd hasnt started return.
        if (!m_CooldownManager.CDStarted) return;
        
        //if cd started increment timer
        m_CooldownManager.CDTimer += Time.deltaTime;

        if (m_CooldownManager.CDTimer >= m_CooldownManager.MaxCDTimer)
        {
            m_CooldownManager.CDEnd();
        }
    }
}
