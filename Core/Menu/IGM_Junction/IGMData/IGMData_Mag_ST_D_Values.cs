﻿
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace OpenVIII
{
    public partial class IGM_Junction
    {
        #region Classes

        private class IGMData_Mag_ST_D_Values : IGMData_Values
        {
            #region Methods

            public static IGMData_Mag_ST_D_Values Create() => Create<IGMData_Mag_ST_D_Values>(14, 5, new IGMDataItem.Box { Title = Icons.ID.Status_Defense, Pos = new Rectangle(280, 342, 545, 288) }, 2, 7);

            public Dictionary<Kernel.J_Statuses, byte> getTotal(Saves.CharacterData source, out Enum[] availableFlagsarray)
                    => getTotal<Kernel.J_Statuses>(out availableFlagsarray, 100, Kernel.Stat.ST_Def_1,
                            source.StatJ[Kernel.Stat.ST_Def_1],
                            source.StatJ[Kernel.Stat.ST_Def_2],
                            source.StatJ[Kernel.Stat.ST_Def_3],
                            source.StatJ[Kernel.Stat.ST_Def_4]);

            public override bool Update()
            {
                if (Memory.State?.Characters != null && Damageable != null && Damageable.GetCharacterData(out Saves.CharacterData c))
                {
                    Dictionary<Kernel.J_Statuses, byte> oldtotal = (prevSetting != null) ? getTotal(prevSetting, out Enum[] availableFlagsarray) : null;
                    Dictionary<Kernel.J_Statuses, byte> total = getTotal(c, out availableFlagsarray);
                    FillData(oldtotal, total, availableFlagsarray, Icons.ID.Status_Death, 1, palette: 10);
                }
                return base.Update();
            }

            protected override void InitShift(int i, int col, int row)
            {
                base.InitShift(i, col, row);
                SIZE[i].Inflate(-25, -10);
                SIZE[i].Y -= 3 * row;
            }

            #endregion Methods
        }

        #endregion Classes
    }
}