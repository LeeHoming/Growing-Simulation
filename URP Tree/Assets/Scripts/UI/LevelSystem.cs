using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public SkillSettingPanel SkillPanel;

    public GameOfPlants GOP;

    private int
        //Max seed period

            T0Max = 5,
            //Max young tree period
            T1Max = 6;

    public int
        //publish seed/young time

            T0,
            T1,
            T2;

    public float

            NearbyRadiusMin = 2.5f,
            CrowdFactor,
            NearbyRadius,
            WaterResistFactor;

    private void Awake()
    {
        //SkillPanel = GetComponent<SkillSettingPanel>();
    }

    public void ParameterCulculator()
    {
        //Grow speed adjustment
        //Get GS from panel
        int GS = SkillPanel.GetNumByType(SkillType.GrowSpeed);
        if (GS < 3)
        {
            T0 = T0Max - GS;
            T1 = T1Max;
        }
        else if (GS >= 3)
        {
            T0 = T0Max - 3;
            T1 = T1Max - GS + 1;
        }

        //Drought adjust
        int DR = SkillPanel.GetNumByType(SkillType.Drought);
        WaterResistFactor = 1f - 0.1f * (float) DR;
        if (DR >= 3)
        {
            CrowdFactor = 3f;
        }
        else
        {
            CrowdFactor = 4f;
        }

        //Spread distance
        int DS = SkillPanel.GetNumByType(SkillType.Distance);
        NearbyRadius = NearbyRadiusMin + 0.8f * (float) DS;

        //Duration - Producing round
        T2 = SkillPanel.GetNumByType(SkillType.Duration) + 2;
    }
}
