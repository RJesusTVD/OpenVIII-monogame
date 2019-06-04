﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF8
{
    public partial class Module_main_menu_debug
    {
        private class IGM_Junction : Menu
        {
            public enum Items
            {
                Junction,
                Off,
                Auto,
                Ability,
                HP,
                Str,
                Vit,
                Mag,
                Spr,
                Spd,
                Luck,
                Hit,
                ST_A,
                ST_D,
                EL_A,
                EL_D,
                ST_A_D,
                EL_A_D,
                Stats,
                ST_A2,
                GF,
                Magic,
                AutoAtk,
                AutoMag,
                AutoDef,
                RemAll,
                RemMag,
                ChooseGFtojunction,
                Chooseslottojunction,
                Choosemagictojunction,
                RemovealljunctionedGFandmagic,
                Removealljunctionedmagic,
                CurrentEXP,
                NextLEVEL,
                _,
                LV,
                ForwardSlash,
                Percent
            }

            public enum SectionName
            {
                /// <summary>
                /// Junction OFF Auto Ability
                /// </summary>
                TopMenu,

                /// <summary>
                /// Top Right
                /// </summary>
                Title,

                /// <summary>
                /// Description Help
                /// </summary>
                Help,

                /// <summary>
                /// Character Stats
                /// </summary>
                Mag_Group,

                /// <summary>
                /// 4 Commands you can use in battle
                /// </summary>
                Commands,

                /// <summary>
                /// Portrait Name HP EXP Rank?
                /// </summary>
                CharacterInfo,

                TopMenu_Junction,
                TopMenu_Off,
                TopMenu_Auto,
                TopMenu_Abilities,
                RemMag,
                RemAll,
                TopMenu_GF_Group,
            }

            public static Dictionary<Items, FF8String> Titles { get; private set; }
            public static Dictionary<Items, FF8String> Misc { get; private set; }
            public static Dictionary<Items, FF8String> Descriptions { get; private set; }

            /// <summary>
            /// Character who has the junctions and inventory. Same as VisableCharacter unless TeamLaguna.
            /// </summary>
            public static Characters Character { get; private set; }

            /// <summary>
            /// Required to support Laguna's Party. They have unique stats but share junctions and inventory.
            /// </summary>
            public static Characters VisableCharacter { get; private set; }

            public override bool Update()
            {
                base.Update();
                return Inputs();
            }

            protected override void Init()
            {
                Size = new Vector2 { X = 840, Y = 630 };
                TextScale = new Vector2(2.545455f, 3.0375f);

                Titles = new Dictionary<Items, FF8String> {
                    {Items.Junction, Memory.Strings.Read(Strings.FileID.MNGRP,2,217) },
                    {Items.Off, Memory.Strings.Read(Strings.FileID.MNGRP,2,219) },
                    {Items.Auto, Memory.Strings.Read(Strings.FileID.MNGRP,2,221) },
                    {Items.Ability, Memory.Strings.Read(Strings.FileID.MNGRP,2,223) },
                    {Items.HP, Memory.Strings.Read(Strings.FileID.MNGRP,2,225) },
                    {Items.Str, Memory.Strings.Read(Strings.FileID.MNGRP,2,227) },
                    {Items.Vit, Memory.Strings.Read(Strings.FileID.MNGRP,2,229) },
                    {Items.Mag, Memory.Strings.Read(Strings.FileID.MNGRP,2,231) },
                    {Items.Spr, Memory.Strings.Read(Strings.FileID.MNGRP,2,233) },
                    {Items.Spd, Memory.Strings.Read(Strings.FileID.MNGRP,2,235) },
                    {Items.Luck, Memory.Strings.Read(Strings.FileID.MNGRP,2,237) },
                    {Items.Hit, Memory.Strings.Read(Strings.FileID.MNGRP,2,239) },
                    {Items.ST_A,Memory.Strings.Read(Strings.FileID.MNGRP,2,243)},
                    {Items.ST_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,245)},
                    {Items.EL_A,Memory.Strings.Read(Strings.FileID.MNGRP,2,247)},
                    {Items.EL_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,249)},
                    {Items.ST_A_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,251)},
                    {Items.EL_A_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,253)},
                    {Items.Stats,Memory.Strings.Read(Strings.FileID.MNGRP,2,255)},
                    { Items.ST_A2,Memory.Strings.Read(Strings.FileID.MNGRP, 2, 257)},
                    {Items.GF,Memory.Strings.Read(Strings.FileID.MNGRP,2,262)},
                    { Items.Magic,Memory.Strings.Read(Strings.FileID.MNGRP, 2, 264)},
                    {Items.AutoAtk,Memory.Strings.Read(Strings.FileID.MNGRP,2,269)},
                    {Items.AutoMag,Memory.Strings.Read(Strings.FileID.MNGRP,2,271)},
                    {Items.AutoDef,Memory.Strings.Read(Strings.FileID.MNGRP,2,273)},
                    {Items.RemAll,Memory.Strings.Read(Strings.FileID.MNGRP,2,275)},
                    {Items.RemMag,Memory.Strings.Read(Strings.FileID.MNGRP,2,277)},
                };

                Misc = new Dictionary<Items, FF8String> {
                { Items.CurrentEXP, Memory.Strings.Read(Strings.FileID.MNGRP, 0, 23)  },
                { Items.NextLEVEL, Memory.Strings.Read(Strings.FileID.MNGRP, 0, 24)  },
                { Items._,Memory.Strings.Read(Strings.FileID.MNGRP,2,266)},
                { Items.HP,Memory.Strings.Read(Strings.FileID.MNGRP,0,26)},
                { Items.LV,Memory.Strings.Read(Strings.FileID.MNGRP,0,27)},
                { Items.ForwardSlash,Memory.Strings.Read(Strings.FileID.MNGRP,0,25)},
                { Items.Percent,Memory.Strings.Read(Strings.FileID.MNGRP,0,29)},
                };

                //{Items.ST_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,259)},
                //{Items.EL_A,Memory.Strings.Read(Strings.FileID.MNGRP,2,260)},
                //{Items.EL_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,261)},
                //{Items.Areyousure?,Memory.Strings.Read(Strings.FileID.MNGRP,2,267)},
                //{Items.Keepprevioussetting,Memory.Strings.Read(Strings.FileID.MNGRP,2,268)},
                //{Items.Junctionedto<Special:0xA27>,Memory.Strings.Read(Strings.FileID.MNGRP,2,284)},
                //{Items.Empty,Memory.Strings.Read(Strings.FileID.MNGRP,2,285)},
                //{Items.BasicOperation,Memory.Strings.Read(Strings.FileID.MNGRP,2,286)},
                //{Items.BasicControlExplanationinFFVIII,Memory.Strings.Read(Strings.FileID.MNGRP,2,287)},
                //{Items.BattleOperation,Memory.Strings.Read(Strings.FileID.MNGRP,2,288)},
                //{Items.BattleExplanation,Memory.Strings.Read(Strings.FileID.MNGRP,2,289)},
                //{Items.CardGameRules,Memory.Strings.Read(Strings.FileID.MNGRP,2,290)},
                //{Items.CardGameExplanation,Memory.Strings.Read(Strings.FileID.MNGRP,2,291)},
                //{Items.OnlineHelp,Memory.Strings.Read(Strings.FileID.MNGRP,2,292)},
                //{Items.ExplanationofVariousFeatures,Memory.Strings.Read(Strings.FileID.MNGRP,2,293)},
                //{Items.GFJunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,294)},
                //{Items.JunctioningaGFandsettingcommands,Memory.Strings.Read(Strings.FileID.MNGRP,2,295)},
                //{Items.MagicJunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,296)},
                //{Items.Explanationonjunctioningmagic,Memory.Strings.Read(Strings.FileID.MNGRP,2,297)},
                //{Items.JunctiontoElements,Memory.Strings.Read(Strings.FileID.MNGRP,2,298)},
                //{Items.Explanationofelementaljunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,299)},
                //{Items.JunctionofStatus,Memory.Strings.Read(Strings.FileID.MNGRP,2,300)},
                //{Items.Explanationofstatusjunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,301)},

                Descriptions = new Dictionary<Items, FF8String> {
                    {Items.Junction, Memory.Strings.Read(Strings.FileID.MNGRP,2,218) }
                };

                Data.Add(SectionName.CharacterInfo, new IGMData_CharacterInfo());
                Data.Add(SectionName.Commands, new IGMData_Commands());
                Data.Add(SectionName.Help, new IGMData_Help());
                Data.Add(SectionName.TopMenu, new IGMData_TopMenu());
                Data.Add(SectionName.Title, new IGMData_Container(
                    new IGMDataItem_Box(Titles[Items.Junction], pos: new Rectangle(615, 0, 225, 66))));
                Data.Add(SectionName.TopMenu_Junction, new IGMData_TopMenu_Junction());
                Data.Add(SectionName.TopMenu_Off, new IGMData_TopMenu_Off_Group(
                    new IGMData_Container(
                        new IGMDataItem_Box(Titles[Items.Off], pos: new Rectangle(0, 12, 169, 54), options: Box_Options.Center | Box_Options.Middle)),
                    new IGMData_TopMenu_Off()
                    ));
                Data.Add(SectionName.TopMenu_Auto, new IGMData_TopMenu_Auto_Group(
                    new IGMData_Container(
                        new IGMDataItem_Box(Titles[Items.Auto], pos: new Rectangle(0, 12, 169, 54), options: Box_Options.Center | Box_Options.Middle)),
                    new IGMData_TopMenu_Auto()));
                Data.Add(SectionName.TopMenu_Abilities, new IGMData_Abilities_Group(
                    new IGMData_Abilities_Command(),
                    new IGMData_Abilities_AbilitySlots(),
                    new IGMData_Abilities_CommandPool(),
                    new IGMData_Abilities_AbilityPool()
                    ));
                FF8String Yes = Memory.Strings.Read(Strings.FileID.MNGRP, 0, 57);
                FF8String No = Memory.Strings.Read(Strings.FileID.MNGRP, 0, 58);
                Data.Add(SectionName.RemMag, new IGMData_ConfirmRemMag(data: Memory.Strings.Read(Strings.FileID.MNGRP, 2, 280), title: Icons.ID.NOTICE, opt1: Yes, opt2: No, pos: new Rectangle(180, 174, 477, 216)));
                Data.Add(SectionName.RemAll, new IGMData_ConfirmRemAll(data: Memory.Strings.Read(Strings.FileID.MNGRP, 2, 279), title: Icons.ID.NOTICE, opt1: Yes, opt2: No, pos: new Rectangle(180, 174, 477, 216)));
                Data.Add(SectionName.TopMenu_GF_Group, new IGMData_GF_Group(
                    new IGMData_GF_Junctioned(),
                    new IGMData_GF_Pool(),
                    new IGMData_Container(new IGMDataItem_Box(pos: new Rectangle(440, 345, 385, 66)))
                    ));

                Data.Add(SectionName.Mag_Group, new IGMData_Mag_Group(
                    new IGMData_Mag_Stats(),
                    new IGMData_Container(new IGMDataItem_Box(pos: new Rectangle(0, 345, 435, 66))),
                    new IGMData_Mag_Pool()
                    ));
                base.Init();
            }

            public void ReInit(Characters c, Characters vc)
            {
                Character = c;
                VisableCharacter = vc;
                ReInit();
            }

            public new enum Mode
            {
                TopMenu,
                TopMenu_Junction,
                TopMenu_Off,
                TopMenu_Auto,
                Abilities,
                Abilities_Commands,
                Abilities_Abilities,
                RemMag,
                RemAll,
                TopMenu_GF_Group,
                Mag_Pool,
                Mag_Stat
            }

            public new Mode mode;

            protected override bool Inputs()
            {
                bool ret = false;
                switch (mode)
                {
                    case Mode.TopMenu:
                        ret = ((IGMData_TopMenu)Data[SectionName.TopMenu]).Inputs();
                        break;

                    case Mode.TopMenu_Junction:
                        ret = ((IGMData_TopMenu_Junction)Data[SectionName.TopMenu_Junction]).Inputs();
                        break;

                    case Mode.TopMenu_Off:
                        ret = ((IGMData_TopMenu_Off_Group)Data[SectionName.TopMenu_Off]).Inputs();
                        break;

                    case Mode.TopMenu_Auto:
                        ret = ((IGMData_TopMenu_Auto_Group)Data[SectionName.TopMenu_Auto]).Inputs();
                        break;

                    case Mode.Abilities:
                        ret = ((IGMData_Abilities_Group)Data[SectionName.TopMenu_Abilities]).Inputs();
                        break;

                    case Mode.Abilities_Commands:
                        ret = ((IGMData_Abilities_Group)Data[SectionName.TopMenu_Abilities]).ITEM[2, 0].Inputs();
                        break;

                    case Mode.Abilities_Abilities:
                        ret = ((IGMData_Abilities_Group)Data[SectionName.TopMenu_Abilities]).ITEM[3, 0].Inputs();
                        break;

                    case Mode.RemMag:
                        ret = ((IGMData_ConfirmDialog)Data[SectionName.RemMag]).Inputs();
                        break;

                    case Mode.RemAll:
                        ret = ((IGMData_ConfirmDialog)Data[SectionName.RemAll]).Inputs();
                        break;

                    case Mode.TopMenu_GF_Group:
                        ret = ((IGMData_GF_Group)Data[SectionName.TopMenu_GF_Group]).ITEM[1, 0].Inputs();
                        break;

                    case Mode.Mag_Pool:
                        ret = ((IGMData_Mag_Group)Data[SectionName.Mag_Group]).ITEM[2, 0].Inputs();
                        break;
                    case Mode.Mag_Stat:
                        ret = ((IGMData_Mag_Group)Data[SectionName.Mag_Group]).ITEM[0, 0].Inputs();
                        break;
                    default:
                        break;
                }
                return ret;
            }

            public override void Draw()
            {
                StartDraw();

                base.Draw();
                EndDraw();
            }

            private class IGMData_CharacterInfo : IGMData
            {
                public IGMData_CharacterInfo() : base(1, 15, new IGMDataItem_Empty(new Rectangle(20, 153, 395, 255)))
                {
                }

                /// <summary>
                /// Things that may of changed before screen loads or junction is changed.
                /// </summary>
                public override void ReInit()
                {
                    base.ReInit();
                    ITEM[0, 0] = new IGMDataItem_Face((Faces.ID)VisableCharacter, new Rectangle(X + 12, Y, 96, 144));
                    ITEM[0, 2] = new IGMDataItem_String(Memory.Strings.GetName(VisableCharacter), new Rectangle(X + 117, Y + 0, 0, 0));

                    if (Memory.State.Characters != null)
                    {
                        ITEM[0, 4] = new IGMDataItem_Int(Memory.State.Characters[Character].Level, new Rectangle(X + 117 + 35, Y + 54, 0, 0), 13, numtype: Icons.NumType.sysFntBig, padding: 1, spaces: 6);
                        ITEM[0, 5] = Memory.State.Party != null && Memory.State.Party.Contains(Character)
                            ? new IGMDataItem_Icon(Icons.ID.InParty, new Rectangle(X + 278, Y + 48, 0, 0), 6)
                            : null;
                        ITEM[0, 7] = new IGMDataItem_Int(Memory.State.Characters[Character].CurrentHP(VisableCharacter), new Rectangle(X + 152, Y + 108, 0, 0), 13, numtype: Icons.NumType.sysFntBig, padding: 1, spaces: 6);
                        ITEM[0, 9] = new IGMDataItem_Int(Memory.State.Characters[Character].MaxHP(VisableCharacter), new Rectangle(X + 292, Y + 108, 0, 0), 13, numtype: Icons.NumType.sysFntBig, padding: 1, spaces: 5);
                        ITEM[0, 11] = new IGMDataItem_Int((int)Memory.State.Characters[Character].Experience, new Rectangle(X + 192, Y + 198, 0, 0), 13, numtype: Icons.NumType.Num_8x8_2, padding: 1, spaces: 9);
                        ITEM[0, 13] = new IGMDataItem_Int(Memory.State.Characters[Character].ExperienceToNextLevel, new Rectangle(X + 192, Y + 231, 0, 0), 13, numtype: Icons.NumType.Num_8x8_2, padding: 1, spaces: 9);
                    }
                }

                /// <summary>
                /// Things fixed at startup.
                /// </summary>
                protected override void Init()
                {
                    ITEM[0, 1] = new IGMDataItem_Icon(Icons.ID.MenuBorder, new Rectangle(X + 10, Y - 2, 100, 148), scale: new Vector2(1f));
                    ITEM[0, 3] = new IGMDataItem_String(Misc[Items.LV], new Rectangle(X + 117, Y + 54, 0, 0));
                    ITEM[0, 6] = new IGMDataItem_String(Misc[Items.HP], new Rectangle(X + 117, Y + 108, 0, 0));
                    ITEM[0, 8] = new IGMDataItem_String(Misc[Items.ForwardSlash], new Rectangle(X + 272, Y + 108, 0, 0));
                    ITEM[0, 10] = new IGMDataItem_String(Misc[Items.CurrentEXP] + new FF8String("\n") + Misc[Items.NextLEVEL], new Rectangle(X, Y + 192, 0, 0));
                    ITEM[0, 12] = new IGMDataItem_Icon(Icons.ID.P, new Rectangle(X + 372, Y + 198, 0, 0), 2);
                    ITEM[0, 14] = new IGMDataItem_Icon(Icons.ID.P, new Rectangle(X + 372, Y + 231, 0, 0), 2);
                    base.Init();
                }
            }

            private class IGMData_Mag_Stats : IGMData
            {
                public IGMData_Mag_Stats() : base(10, 5, new IGMDataItem_Box(pos: new Rectangle(0, 414, 840, 216)), 2, 5)
                {
                }

                /// <summary>
                /// Convert stat to correct icon id.
                /// </summary>
                private static Dictionary<Kernel_bin.Stat, Icons.ID> Stat2Icon = new Dictionary<Kernel_bin.Stat, Icons.ID>
                {
                    { Kernel_bin.Stat.HP, Icons.ID.Stats_Hit_Points },
                    { Kernel_bin.Stat.STR, Icons.ID.Stats_Strength },
                    { Kernel_bin.Stat.VIT, Icons.ID.Stats_Vitality },
                    { Kernel_bin.Stat.MAG, Icons.ID.Stats_Magic },
                    { Kernel_bin.Stat.SPR, Icons.ID.Stats_Spirit },
                    { Kernel_bin.Stat.SPD, Icons.ID.Stats_Speed },
                    { Kernel_bin.Stat.EVA, Icons.ID.Stats_Evade },
                    { Kernel_bin.Stat.LUCK, Icons.ID.Stats_Luck },
                    { Kernel_bin.Stat.HIT, Icons.ID.Stats_Hit_Percent },
                };

                /// <summary>
                /// Things that may of changed before screen loads or junction is changed.
                /// </summary>
                public override void ReInit()
                {
                    base.ReInit();

                    if (Memory.State.Characters != null)
                    {
                        List<Kernel_bin.Abilities> unlocked = Memory.State.Characters[Character].UnlockedGFAbilities;
                        ITEM[5, 0] = new IGMDataItem_Icon(Icons.ID.Icon_Status_Attack, new Rectangle(SIZE[5].X + 200, SIZE[5].Y, 0, 0),
                            (byte)(unlocked.Contains(Kernel_bin.Abilities.ST_Atk_J) ? 2 : 7));
                        ITEM[5, 1] = new IGMDataItem_Icon(Icons.ID.Icon_Status_Defense, new Rectangle(SIZE[5].X + 240, SIZE[5].Y, 0, 0),
                            (byte)(unlocked.Contains(Kernel_bin.Abilities.ST_Def_Jx1) ||
                            unlocked.Contains(Kernel_bin.Abilities.ST_Def_Jx2) ||
                            unlocked.Contains(Kernel_bin.Abilities.ST_Def_Jx4) ? 2 : 7));
                        ITEM[5, 2] = new IGMDataItem_Icon(Icons.ID.Icon_Elemental_Attack, new Rectangle(SIZE[5].X + 280, SIZE[5].Y, 0, 0),
                            (byte)(unlocked.Contains(Kernel_bin.Abilities.Elem_Atk_J) ? 2 : 7));
                        ITEM[5, 3] = new IGMDataItem_Icon(Icons.ID.Icon_Elemental_Defense, new Rectangle(SIZE[5].X + 320, SIZE[5].Y, 0, 0),
                            (byte)(unlocked.Contains(Kernel_bin.Abilities.Elem_Def_Jx1) ||
                            unlocked.Contains(Kernel_bin.Abilities.Elem_Def_Jx2) ||
                            unlocked.Contains(Kernel_bin.Abilities.Elem_Def_Jx4) ? 2 : 7));
                        BLANKS[5] = true;
                        foreach (Kernel_bin.Stat stat in (Kernel_bin.Stat[])Enum.GetValues(typeof(Kernel_bin.Stat)))
                        {
                            if (Stat2Icon.ContainsKey(stat))
                            {
                                int pos = (int)stat;
                                if (pos >= 5) pos++;
                                FF8String name = Kernel_bin.MagicData[Memory.State.Characters[Character].Stat_J[stat]].Name;
                                if (name.Length == 0) name = Misc[Items._];

                                ITEM[pos, 0] = new IGMDataItem_Icon(Stat2Icon[stat], new Rectangle(SIZE[pos].X, SIZE[pos].Y, 0, 0), 2);
                                ITEM[pos, 1] = new IGMDataItem_String(name, new Rectangle(SIZE[pos].X + 80, SIZE[pos].Y, 0, 0));
                                if (!unlocked.Contains(Kernel_bin.Stat2Ability[stat]))
                                {
                                    ((IGMDataItem_Icon)ITEM[pos, 0]).Pallet = ((IGMDataItem_Icon)ITEM[pos, 0]).Faded_Pallet = 7;
                                    ((IGMDataItem_String)ITEM[pos, 1]).Colorid = Font.ColorID.Grey;
                                }
                                ITEM[pos, 2] = new IGMDataItem_Int(Memory.State.Characters[Character].TotalStat(stat, VisableCharacter), new Rectangle(SIZE[pos].X + 152, SIZE[pos].Y, 0, 0), 2, Icons.NumType.sysFntBig, spaces: 10);
                                ITEM[pos, 3] = stat == Kernel_bin.Stat.HIT || stat == Kernel_bin.Stat.EVA
                                    ? new IGMDataItem_String(Misc[Items.Percent], new Rectangle(SIZE[pos].X + 350, SIZE[pos].Y, 0, 0))
                                    : null;

                                //((IGMDataItem_String)ITEM[pos, 1]).Colorid = Font.ColorID.Red;
                                //ITEM[pos, 4] = new IGMDataItem_Icon(Icons.ID.Arrow_Down, new Rectangle(SIZE[pos].X + 265, SIZE[pos].Y, 0, 0), 16);
                                //((IGMDataItem_String)ITEM[pos, 1]).Colorid = Font.ColorID.Yellow;
                                //ITEM[pos, 4] = new IGMDataItem_Icon(Icons.ID.Arrow_Up, new Rectangle(SIZE[pos].X + 265, SIZE[pos].Y, 0, 0), 17);
                            }
                        }
                    }
                }
                public override bool Update()
                {
                    bool ret = base.Update();
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.Mag_Stat && Enabled)
                    {
                        Cursor_Status |= Cursor_Status.Enabled;
                        Cursor_Status |= Cursor_Status.Horizontal;
                        Cursor_Status |= Cursor_Status.Vertical;
                        Cursor_Status &= ~Cursor_Status.Blinking;
                    }
                    else if(InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.Mag_Pool && Enabled)
                    {
                        Cursor_Status |= Cursor_Status.Blinking;
                    }
                    else
                    {
                        Cursor_Status &= ~Cursor_Status.Enabled;
                    }
                    return ret;
                }
                public override void Inputs_OKAY()
                {
                    base.Inputs_OKAY();
                    InGameMenu_Junction.mode = Mode.Mag_Pool;
                }
                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.mode = Mode.TopMenu_Junction;
                    InGameMenu_Junction.Data[SectionName.Mag_Group].Hide();
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(0, 4 + (-2 * row));
                }

                /// <summary>
                /// Things fixed at startup.
                /// </summary>
                protected override void Init() => base.Init();
            }

            private class IGMData_Commands : IGMData
            {
                public IGMData_Commands() : base(4, 1, new IGMDataItem_Box(pos: new Rectangle(615, 150, 210, 192), title: Icons.ID.COMMAND), 1, 4)
                {
                }

                /// <summary>
                /// Things that may of changed before screen loads or junction is changed.
                /// </summary>
                public override void ReInit()
                {
                    base.ReInit();

                    if (Memory.State.Characters != null)
                    {
                        ITEM[0, 0] = new IGMDataItem_String(
                                Kernel_bin.BattleCommands[
                                    Memory.State.Characters[Character].Abilities.Contains(Kernel_bin.Abilities.Mug) ?
                                    13 :
                                    1].Name,
                                SIZE[0]);

                        for (int pos = 1; pos < SIZE.Length; pos++)
                        {
                            ITEM[pos, 0] = Memory.State.Characters[Character].Commands[pos - 1] != Kernel_bin.Abilities.None ? new IGMDataItem_String(
                                Kernel_bin.Commandabilities[Memory.State.Characters[Character].Commands[pos - 1]].Name,
                                SIZE[pos]) : null;
                        }
                    }
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(0, 12 + (-8 * row));
                }

                /// <summary>
                /// Things fixed at startup.
                /// </summary>
                protected override void Init() => base.Init();
            }

            private class IGMData_Help : IGMData
            {
                public FF8String Data { get => ((IGMDataItem_Box)CONTAINER).Data; set => ((IGMDataItem_Box)CONTAINER).Data = value; }

                public IGMData_Help() : base(0, 0, new IGMDataItem_Box(IGM_Junction.Descriptions[Items.Junction], pos: new Rectangle(15, 69, 810, 78), title: Icons.ID.HELP))
                {
                }
            }

            private class IGMData_TopMenu : IGMData
            {
                public new Dictionary<Items, FF8String> Descriptions { get; private set; }

                public IGMData_TopMenu() : base(4, 1, new IGMDataItem_Box(pos: new Rectangle(0, 12, 610, 54)), 4, 1)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-40, -12);
                    SIZE[i].Offset(20 + (-20 * (col > 1 ? col : 0)), 0);
                }

                protected override void Init()
                {
                    base.Init();
                    ITEM[0, 0] = new IGMDataItem_String(Titles[Items.Junction], SIZE[0]);
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Horizontal;
                    Cursor_Status |= Cursor_Status.Vertical;
                    Descriptions = new Dictionary<Items, FF8String> {
                        {Items.Junction, Memory.Strings.Read(Strings.FileID.MNGRP,2,218) },
                        {Items.Off, Memory.Strings.Read(Strings.FileID.MNGRP,2,220) },
                        {Items.Auto, Memory.Strings.Read(Strings.FileID.MNGRP,2,222) },
                        {Items.Ability, Memory.Strings.Read(Strings.FileID.MNGRP,2,224) },
                    };
                }

                public override void ReInit()
                {
                    if (Memory.State.Characters != null)
                    {
                        Font.ColorID color = (Memory.State.Characters[Character].JunctionnedGFs == Saves.GFflags.None) ? Font.ColorID.Grey : Font.ColorID.White;

                        ITEM[1, 0] = new IGMDataItem_String(Titles[Items.Off], SIZE[1], color);
                        ITEM[2, 0] = new IGMDataItem_String(Titles[Items.Auto], SIZE[2], color);
                        ITEM[3, 0] = new IGMDataItem_String(Titles[Items.Ability], SIZE[3], color);
                        for (int i = 1; i <= 3; i++)
                            BLANKS[i] = Memory.State.Characters[Character].JunctionnedGFs == Saves.GFflags.None;
                    }
                    base.ReInit();
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    if (State == MainMenuStates.IGM_Junction)
                    {
                        State = MainMenuStates.InGameMenu;
                        InGameMenu.ReInit();
                        Fade = 0.0f;
                    }
                }

                public override void Inputs_OKAY()
                {
                    switch (CURSOR_SELECT)
                    {
                        case 0:
                            InGameMenu_Junction.Data[SectionName.TopMenu_Junction].Show();
                            Cursor_Status |= Cursor_Status.Blinking;
                            InGameMenu_Junction.mode = Mode.TopMenu_Junction;
                            break;

                        case 1:
                            InGameMenu_Junction.Data[SectionName.TopMenu_Off].Show();
                            Cursor_Status |= Cursor_Status.Blinking;
                            InGameMenu_Junction.mode = Mode.TopMenu_Off;
                            break;

                        case 2:
                            InGameMenu_Junction.Data[SectionName.TopMenu_Auto].Show();
                            Cursor_Status |= Cursor_Status.Blinking;
                            InGameMenu_Junction.mode = Mode.TopMenu_Auto;
                            break;

                        case 3:
                            InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].Show();
                            Cursor_Status |= Cursor_Status.Blinking;
                            InGameMenu_Junction.mode = Mode.Abilities;
                            break;
                    }
                    base.Inputs_OKAY();
                }

                public override bool Update()
                {
                    bool ret = base.Update();
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.TopMenu && Enabled)
                    {
                        FF8String Changed = null;
                        switch (CURSOR_SELECT)
                        {
                            case 0:
                                Changed = Descriptions[Items.Junction];
                                break;

                            case 1:
                                Changed = Descriptions[Items.Off];
                                break;

                            case 2:
                                Changed = Descriptions[Items.Auto];
                                break;

                            case 3:
                                Changed = Descriptions[Items.Ability];
                                break;
                        }
                        if (Changed != null)
                            ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Changed;
                    }
                    return ret;
                }
            }

            private class IGMData_TopMenu_Junction : IGMData
            {
                public new Dictionary<Items, FF8String> Descriptions { get; private set; }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.mode = Mode.TopMenu;
                    Hide();
                }

                public override void Inputs_OKAY()
                {
                    base.Inputs_OKAY();
                    if (CURSOR_SELECT == 0)
                    {
                        InGameMenu_Junction.mode = Mode.TopMenu_GF_Group;
                        InGameMenu_Junction.Data[SectionName.TopMenu_GF_Group].Show();
                    }
                    else
                    {
                        InGameMenu_Junction.mode = Mode.Mag_Stat;
                        InGameMenu_Junction.Data[SectionName.Mag_Group].Show();
                    }
                }

                public IGMData_TopMenu_Junction() : base(2, 1, new IGMDataItem_Box(pos: new Rectangle(210, 12, 400, 54)), 2, 1)
                {
                }

                public override bool Update()
                {
                    Update_String();
                    if (InGameMenu_Junction != null)
                    {
                        if (InGameMenu_Junction.mode == Mode.TopMenu_Junction)
                            Cursor_Status &= ~Cursor_Status.Blinking;
                        else
                            Cursor_Status |= Cursor_Status.Blinking;
                    }
                    return base.Update();
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-40, -12);
                    SIZE[i].Offset(20 + (-20 * (col > 1 ? col : 0)), 0);
                }

                protected override void Init()
                {
                    base.Init();
                    ITEM[0, 0] = new IGMDataItem_String(Titles[Items.GF], SIZE[0]);
                    ITEM[1, 0] = new IGMDataItem_String(Titles[Items.Magic], SIZE[1]);
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Horizontal;
                    Cursor_Status |= Cursor_Status.Vertical;

                    Descriptions = new Dictionary<Items, FF8String> {
                        {Items.GF,Memory.Strings.Read(Strings.FileID.MNGRP,2,263)},
                        {Items.Magic,Memory.Strings.Read(Strings.FileID.MNGRP,2,265)},
                    };

                    Hide();
                }

                private void Update_String()
                {
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.TopMenu_Junction && Enabled)
                    {
                        FF8String Changed = null;
                        switch (CURSOR_SELECT)
                        {
                            case 0:
                                Changed = Descriptions[Items.GF];
                                break;

                            case 1:
                                Changed = Descriptions[Items.Magic];
                                break;
                        }
                        if (Changed != null && InGameMenu_Junction != null)
                            ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Changed;
                    }
                }
            }

            private class IGMData_TopMenu_Off : IGMData
            {
                public IGMData_TopMenu_Off() : base(2, 1, new IGMDataItem_Box(pos: new Rectangle(165, 12, 445, 54)), 2, 1)
                {
                }

                public new Dictionary<Items, FF8String> Descriptions { get; private set; }

                private void Update_String()
                {
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.TopMenu_Off && Enabled)
                    {
                        FF8String Changed = null;
                        switch (CURSOR_SELECT)
                        {
                            case 0:
                                Changed = Descriptions[Items.RemMag];
                                break;

                            case 1:
                                Changed = Descriptions[Items.RemAll];
                                break;
                        }
                        if (Changed != null && InGameMenu_Junction != null)
                            ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Changed;
                    }
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-40, -12);
                    SIZE[i].Offset(20 + (-20 * (col > 1 ? col : 0)), 0);
                }

                public override bool Update()
                {
                    bool ret = base.Update();
                    Update_String();

                    if (InGameMenu_Junction != null)
                    {
                        if (InGameMenu_Junction.mode == Mode.TopMenu_Off)
                            Cursor_Status &= ~Cursor_Status.Blinking;
                        else
                            Cursor_Status |= Cursor_Status.Blinking;
                    }
                    return ret;
                }

                protected override void Init()
                {
                    base.Init();
                    ITEM[0, 0] = new IGMDataItem_String(Titles[Items.RemMag], SIZE[0]);
                    ITEM[1, 0] = new IGMDataItem_String(Titles[Items.RemAll], SIZE[1]);
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Horizontal;
                    Cursor_Status |= Cursor_Status.Vertical;
                    Descriptions = new Dictionary<Items, FF8String> {
                        {Items.RemMag,Memory.Strings.Read(Strings.FileID.MNGRP,2,278)},
                        {Items.RemAll,Memory.Strings.Read(Strings.FileID.MNGRP,2,276)},
                    };
                }

                public override void Inputs_OKAY()
                {
                    base.Inputs_OKAY();
                    switch (CURSOR_SELECT)
                    {
                        case 0:
                            InGameMenu_Junction.Data[SectionName.RemMag].Show();
                            InGameMenu_Junction.mode = Mode.RemMag;
                            break;

                        case 1:
                            InGameMenu_Junction.Data[SectionName.RemAll].Show();
                            InGameMenu_Junction.mode = Mode.RemAll;
                            break;
                    }
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.Data[SectionName.TopMenu_Off].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu;
                }
            }

            private class IGMData_TopMenu_Auto : IGMData
            {
                public IGMData_TopMenu_Auto() : base(3, 1, new IGMDataItem_Box(pos: new Rectangle(165, 12, 445, 54)), 3, 1)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-40, -12);
                    SIZE[i].Offset(20 + (-20 * (col > 1 ? col : 0)), 0);
                }

                public new Dictionary<Items, FF8String> Descriptions { get; private set; }

                private void Update_String()
                {
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.TopMenu_Auto && Enabled)
                    {
                        FF8String Changed = null;
                        switch (CURSOR_SELECT)
                        {
                            case 0:
                                Changed = Descriptions[Items.AutoAtk];
                                break;

                            case 1:
                                Changed = Descriptions[Items.AutoDef];
                                break;

                            case 2:
                                Changed = Descriptions[Items.AutoMag];
                                break;
                        }
                        if (Changed != null && InGameMenu_Junction != null)
                            ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Changed;
                    }
                }

                public override bool Update()
                {
                    bool ret = base.Update();
                    Update_String();
                    return ret;
                }

                protected override void Init()
                {
                    base.Init();
                    ITEM[0, 0] = new IGMDataItem_String(Titles[Items.AutoAtk], SIZE[0]);
                    ITEM[1, 0] = new IGMDataItem_String(Titles[Items.AutoDef], SIZE[1]);
                    ITEM[2, 0] = new IGMDataItem_String(Titles[Items.AutoMag], SIZE[2]);
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Horizontal;
                    Cursor_Status |= Cursor_Status.Vertical;
                    Descriptions = new Dictionary<Items, FF8String> {
                        //{Items.HP, Memory.Strings.Read(Strings.FileID.MNGRP,2,226) },
                        //{Items.Str, Memory.Strings.Read(Strings.FileID.MNGRP,2,228) },
                        //{Items.Vit, Memory.Strings.Read(Strings.FileID.MNGRP,2,230) },
                        //{Items.Mag, Memory.Strings.Read(Strings.FileID.MNGRP,2,232) },
                        //{Items.Spr, Memory.Strings.Read(Strings.FileID.MNGRP,2,234) },
                        //{Items.Spd, Memory.Strings.Read(Strings.FileID.MNGRP,2,236) },
                        //{Items.Luck, Memory.Strings.Read(Strings.FileID.MNGRP,2,238) },
                        //{Items.Hit, Memory.Strings.Read(Strings.FileID.MNGRP,2,240) },
                        //{Items.ST_A,Memory.Strings.Read(Strings.FileID.MNGRP,2,244)},
                        //{Items.ST_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,246)},
                        //{Items.EL_A,Memory.Strings.Read(Strings.FileID.MNGRP,2,248)},
                        //{Items.EL_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,250)},
                        //{Items.ST_A_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,252)},
                        //{Items.EL_A_D,Memory.Strings.Read(Strings.FileID.MNGRP,2,254)},
                        //{ Items.Stats,Memory.Strings.Read(Strings.FileID.MNGRP,2,256)},
                        //{Items.ST_A2,Memory.Strings.Read(Strings.FileID.MNGRP,2,258)},
                        //{Items.GF,Memory.Strings.Read(Strings.FileID.MNGRP,2,263)},
                        //{Items.Magic,Memory.Strings.Read(Strings.FileID.MNGRP,2,265)},
                        {Items.AutoAtk,Memory.Strings.Read(Strings.FileID.MNGRP,2,270)},
                        {Items.AutoMag,Memory.Strings.Read(Strings.FileID.MNGRP,2,272)},
                        {Items.AutoDef,Memory.Strings.Read(Strings.FileID.MNGRP,2,274)},
                        //{Items.RemMag,Memory.Strings.Read(Strings.FileID.MNGRP,2,278)},
                        //{Items.RemAll,Memory.Strings.Read(Strings.FileID.MNGRP,2,276)},
                        //{Items.RemovealljunctionedGFandmagic,Memory.Strings.Read(Strings.FileID.MNGRP,2,279)},
                        //{Items.Removealljunctionedmagic,Memory.Strings.Read(Strings.FileID.MNGRP,2,280)},
                        //{Items.ChooseGFtojunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,281)},
                        //{Items.Chooseslottojunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,282)},
                        //{Items.Choosemagictojunction,Memory.Strings.Read(Strings.FileID.MNGRP,2,283)},
                    };
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.Data[SectionName.TopMenu_Auto].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu;
                }
            }

            private class IGMData_Abilities_Group : IGMData_Group
            {
                public IGMData_Abilities_Group(params IGMData[] d) : base(d)
                {
                }

                public override void Inputs_Square()
                {
                    skipdata = true;
                    base.Inputs_Square();
                    skipdata = false;

                    IGMDataItem_IGMData i = ((IGMDataItem_IGMData)ITEM[0, 0]);
                    IGMDataItem_IGMData i2 = ((IGMDataItem_IGMData)ITEM[3, 0]);
                    if (i != null && i.Data != null)
                    {
                        if (CURSOR_SELECT >= i.Data.Count)
                        {
                            Memory.State.Characters[Character].Commands[CURSOR_SELECT - 1] = Kernel_bin.Abilities.None;
                            InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].ReInit();
                            InGameMenu_Junction.Data[SectionName.Commands].ReInit();
                        }
                        else
                        {
                            Memory.State.Characters[Character].Abilities[CURSOR_SELECT - i.Data.Count] = Kernel_bin.Abilities.None;
                            InGameMenu_Junction.ReInit();
                        }
                    }
                }

                public override void Inputs_CANCEL()
                {
                    skipdata = true;
                    base.Inputs_CANCEL();
                    skipdata = false;
                    InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu;
                }

                protected override void Init()
                {
                    base.Init();
                    Cursor_Status |= Cursor_Status.Enabled;
                    Hide();
                }

                public override void ReInit()
                {
                    base.ReInit();
                    IGMDataItem_IGMData i = ((IGMDataItem_IGMData)ITEM[0, 0]);
                    IGMDataItem_IGMData i2 = ((IGMDataItem_IGMData)ITEM[1, 0]);
                    if (i != null && i.Data != null && i2 != null && i2.Data != null)
                    {
                        SIZE = new Rectangle[i.Data.Count + i2.Data.Count];
                        Array.Copy(i.Data.SIZE, SIZE, i.Data.Count);
                        Array.Copy(i2.Data.SIZE, 0, SIZE, i.Data.Count, i2.Data.Count);
                        CURSOR = new Point[i.Data.Count + i2.Data.Count];
                        Array.Copy(i.Data.CURSOR, CURSOR, i.Data.Count);
                        Array.Copy(i2.Data.CURSOR, 0, CURSOR, i.Data.Count, i2.Data.Count);
                        BLANKS = new bool[i.Data.Count + i2.Data.Count];
                        Array.Copy(i.Data.BLANKS, BLANKS, i.Data.Count);
                        Array.Copy(i2.Data.BLANKS, 0, BLANKS, i.Data.Count, i2.Data.Count);
                    }
                    if (CURSOR_SELECT == 0)
                        CURSOR_SELECT = 1;
                }

                public override bool Update()
                {
                    bool ret = base.Update();

                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.Abilities)
                    {
                        Cursor_Status &= ~Cursor_Status.Blinking;

                        IGMDataItem_IGMData i = ((IGMDataItem_IGMData)ITEM[0, 0]);
                        IGMDataItem_IGMData i2 = ((IGMDataItem_IGMData)ITEM[1, 0]);
                        if (i != null && i.Data != null && i2 != null && i2.Data != null)
                        {
                            if (CURSOR_SELECT >= i.Data.Count)
                            {
                                if (i2.Data.Descriptions != null && i2.Data.Descriptions.ContainsKey(CURSOR_SELECT - i.Data.Count))
                                {
                                    ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = i2.Data.Descriptions[CURSOR_SELECT - i.Data.Count];
                                }
                            }
                            else
                            {
                                if (i.Data.Descriptions != null && i.Data.Descriptions.ContainsKey(CURSOR_SELECT))
                                {
                                    ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = i.Data.Descriptions[CURSOR_SELECT];
                                }
                            }
                        }
                    }
                    else
                        Cursor_Status |= Cursor_Status.Blinking;

                    return ret;
                }

                public override bool Inputs()
                {
                    skipdata = true;
                    bool ret = base.Inputs();
                    skipdata = false;
                    IGMDataItem_IGMData i = ((IGMDataItem_IGMData)ITEM[0, 0]);
                    IGMDataItem_IGMData i2 = ((IGMDataItem_IGMData)ITEM[3, 0]);
                    if (ret && i != null && i.Data != null)
                    {
                        if (CURSOR_SELECT >= i.Data.Count)
                            i2.Data.Show();
                        else
                            i2.Data.Hide();
                    }
                    return ret;
                }

                public override void Inputs_OKAY()
                {
                    base.Inputs_OKAY();
                    IGMDataItem_IGMData i = ((IGMDataItem_IGMData)ITEM[0, 0]);
                    IGMDataItem_IGMData i2 = ((IGMDataItem_IGMData)ITEM[3, 0]);
                    if (i != null && i.Data != null)
                    {
                        if (CURSOR_SELECT >= i.Data.Count)
                            InGameMenu_Junction.mode = Mode.Abilities_Abilities;
                        else
                            InGameMenu_Junction.mode = Mode.Abilities_Commands;
                    }
                }
            }

            private class IGMData_TopMenu_Off_Group : IGMData_Group
            {
                public IGMData_TopMenu_Off_Group(params IGMData[] d) : base(d)
                {
                }

                public override void Draw()
                {
                    if (Enabled)
                    {
                        Cursor_Status |= (Cursor_Status.Draw | Cursor_Status.Blinking);
                        base.Draw();
                        Tuple<Rectangle, Point, Rectangle> i = ((IGMDataItem_Box)(((IGMData_Container)(((IGMDataItem_IGMData)ITEM[0, 0]).Data)).CONTAINER)).Dims;
                        if (i != null)
                            CURSOR[0] = i.Item2;
                    }
                }

                protected override void Init()
                {
                    base.Init();
                    Hide();
                }
            }

            private class IGMData_TopMenu_Auto_Group : IGMData_Group
            {
                public IGMData_TopMenu_Auto_Group(params IGMData[] d) : base(d)
                {
                }

                public override void Draw()
                {
                    if (Enabled)
                    {
                        Cursor_Status |= (Cursor_Status.Draw | Cursor_Status.Blinking);
                        base.Draw();
                        Tuple<Rectangle, Point, Rectangle> i = ((IGMDataItem_Box)(((IGMData_Container)(((IGMDataItem_IGMData)ITEM[0, 0]).Data)).CONTAINER)).Dims;
                        if (i != null)
                            CURSOR[0] = i.Item2;
                    }
                }

                protected override void Init()
                {
                    base.Init();
                    Hide();
                }
            }

            private class IGMData_Abilities_Command : IGMData
            {
                public IGMData_Abilities_Command() : base(4, 2, new IGMDataItem_Box(pos: new Rectangle(0, 198, 435, 216), title: Icons.ID.COMMAND), 1, 4)
                {
                }

                protected override void Init()
                {
                    base.Init();
                    CURSOR[0] = Point.Zero; //disable this cursor location
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(0, 12 + (-8 * row));
                    CURSOR[i].X += 40;
                }

                public override void ReInit()
                {
                    base.ReInit();

                    if (Memory.State.Characters != null)
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            if (i == 0)
                            {
                                ITEM[i, 1] = new IGMDataItem_String(
                                        Kernel_bin.BattleCommands[
                                            Memory.State.Characters[Character].Abilities.Contains(Kernel_bin.Abilities.Mug) ?
                                            13 :
                                            1].Name,
                                        new Rectangle(SIZE[i].X + 80, SIZE[i].Y, 0, 0));
                            }
                            else
                            {
                                ITEM[i, 0] = new IGMDataItem_Icon(Icons.ID.Arrow_Right2, SIZE[i], 9);
                                ITEM[i, 1] = Memory.State.Characters[Character].Commands[i - 1] != Kernel_bin.Abilities.None ? new IGMDataItem_String(
                                    Icons.ID.Ability_Command, 9,
                                Kernel_bin.Commandabilities[Memory.State.Characters[Character].Commands[i - 1]].Name,
                                new Rectangle(SIZE[i].X + 40, SIZE[i].Y, 0, 0)) : null;
                                Kernel_bin.Abilities k = Memory.State.Characters[Character].Commands[i - 1];
                                Descriptions[i] = k == Kernel_bin.Abilities.None ? null : Kernel_bin.Commandabilities[k].BattleCommand.Description;
                            }
                        }
                    }
                }
            }

            private class IGMData_Abilities_AbilitySlots : IGMData
            {
                public IGMData_Abilities_AbilitySlots() : base(4, 2, new IGMDataItem_Box(pos: new Rectangle(0, 414, 435, 216), title: Icons.ID.ABILITY), 1, 4)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(80, 12 + (-8 * row));
                    CURSOR[i].X += 40;
                }

                public override void ReInit()
                {
                    base.ReInit();

                    if (Memory.State.Characters != null)
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            int slots = 2;
                            if (Memory.State.Characters[Character].UnlockedGFAbilities.Contains(Kernel_bin.Abilities.Abilityx3))
                                slots = 3;
                            if (Memory.State.Characters[Character].UnlockedGFAbilities.Contains(Kernel_bin.Abilities.Abilityx4))
                                slots = 4;
                            if (i < slots)
                            {
                                ITEM[i, 0] = new IGMDataItem_Icon(Icons.ID.Arrow_Right2, SIZE[i], 9);
                                if (Memory.State.Characters[Character].Abilities[i] != Kernel_bin.Abilities.None)
                                {
                                    ITEM[i, 1] = new IGMDataItem_String(

                                    Kernel_bin.EquipableAbilities[Memory.State.Characters[Character].Abilities[i]].Icon, 9,
                                    Kernel_bin.EquipableAbilities[Memory.State.Characters[Character].Abilities[i]].Name,
                                    new Rectangle(SIZE[i].X + 40, SIZE[i].Y, 0, 0));
                                    Descriptions[i] = Kernel_bin.EquipableAbilities[Memory.State.Characters[Character].Abilities[i]].Description.ReplaceRegion();
                                }
                                else
                                {
                                    ITEM[i, 1] = null;
                                    //Descriptions[i] = "";
                                }
                                BLANKS[i] = false;
                            }
                            else
                            {
                                ITEM[i, 0] = null;
                                ITEM[i, 1] = null;
                                BLANKS[i] = true;
                                //Descriptions[i] = "";
                            }
                        }
                    }
                }
            }

            public abstract class IGMData_Pool<T, T2> : IGMData
            {
                public IGMData_Pool(int count, int depth, IGMDataItem container = null, int? rows = null, int? pages = null) : base(count + 2, depth, container, 1, rows) => DefaultPages = pages ?? 1;

                public int DefaultPages { get; private set; }
                public int Pages { get; protected set; }
                public int Page { get; protected set; }
                public T2[] Contents { get; private set; }
                protected T Source { get; set; }

                protected override void Init()
                {
                    base.Init();
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Vertical;
                    Page = 0;
                    Contents = new T2[rows];
                    SIZE[Count - 2].X = X + 6;
                    SIZE[Count - 2].Y = Y + Height - 28;
                    SIZE[Count - 1].X = X + Width - 24;
                    SIZE[Count - 1].Y = Y + Height - 28;
                }

                public override void ReInit()
                {
                    base.ReInit();
                    Pages = DefaultPages;
                    ITEM[Count - 2, 0] = new IGMDataItem_Icon(Icons.ID.Arrow_Left, SIZE[Count - 2], 2, 7);
                    ITEM[Count - 1, 0] = new IGMDataItem_Icon(Icons.ID.Arrow_Right2, SIZE[Count - 1], 2, 7);
                }

                public override bool Inputs()
                {
                    bool ret = false;
                    if (Pages > 1 && CONTAINER.Pos.Contains(Input.MouseLocation.Transform(Focus)))
                    {
                        if (Input.Button(Buttons.MouseWheelup))
                        {
                            PAGE_PREV();
                            ret = true;
                        }
                        else if (Input.Button(Buttons.MouseWheeldown))
                        {
                            PAGE_NEXT();
                            ret = true;
                        }
                        if (ret)
                        {
                            Input.ResetInputLimit();
                            if (!skipsnd)
                                init_debugger_Audio.PlaySound(0);
                            return ret;
                        }
                    }
                    ret = base.Inputs();
                    if (Pages > 1 && !ret)
                    {
                        if (Input.Button(Buttons.Left))
                        {
                            PAGE_PREV();
                            ret = true;
                        }
                        else if (Input.Button(Buttons.Right))
                        {
                            PAGE_NEXT();
                            ret = true;
                        }
                        if (ret)
                        {
                            Input.ResetInputLimit();
                            if (!skipsnd)
                                init_debugger_Audio.PlaySound(0);
                        }
                    }
                    return ret;
                }

                protected virtual void PAGE_NEXT()
                {
                    Page++;
                    if (Page >= Pages)
                        Page = 0;
                }

                protected virtual void PAGE_PREV()
                {
                    Page--;
                    if (Page < 0)
                        Page = Pages - 1;
                }

                public virtual void UpdateTitle()
                {
                }
            }

            private class IGMData_Abilities_CommandPool : IGMData_Pool<Dictionary<Kernel_bin.Abilities, Kernel_bin.Command_abilities>, Kernel_bin.Abilities>
            {
                public IGMData_Abilities_CommandPool() : base(11, 1, new IGMDataItem_Box(pos: new Rectangle(435, 150, 405, 480), title: Icons.ID.COMMAND), 11, Kernel_bin.Commandabilities.Count / 11 + (Kernel_bin.Commandabilities.Count % 11 > 0 ? 1 : 0)) => Source = Kernel_bin.Commandabilities;

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(60, 12 + (-4 * row));
                }

                public override void Inputs_OKAY()
                {
                    skipsnd = true;
                    init_debugger_Audio.PlaySound(31);
                    base.Inputs_OKAY();
                    if (Contents[CURSOR_SELECT] != Kernel_bin.Abilities.None && !BLANKS[CURSOR_SELECT])
                    {
                        int target = InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].CURSOR_SELECT - 1;
                        Memory.State.Characters[Character].Commands[target] = Contents[CURSOR_SELECT];
                        InGameMenu_Junction.mode = Mode.Abilities;
                        InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].ReInit();
                        InGameMenu_Junction.Data[SectionName.Commands].ReInit();
                    }
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.mode = Mode.Abilities;
                }

                public override void UpdateTitle()
                {
                    base.UpdateTitle();
                    if (Pages == 1)
                    {
                        ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.COMMAND;
                        ITEM[11, 0] = ITEM[12, 0] = null;
                    }
                    else
                        switch (Page)
                        {
                            case 0:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.COMMAND_PG1;
                                break;

                            case 1:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.COMMAND_PG2;
                                break;
                        }
                }

                public override bool Update()
                {
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode != Mode.Abilities_Commands)
                        Cursor_Status &= ~Cursor_Status.Enabled;
                    else
                    {
                        Cursor_Status |= Cursor_Status.Enabled;
                    }
                    int pos = 0;
                    int skip = Page * rows;
                    for (int i = 0;
                        Memory.State.Characters != null &&
                        i < Memory.State.Characters[Character].UnlockedGFAbilities.Count &&
                        pos < rows; i++)
                    {
                        if (Memory.State.Characters[Character].UnlockedGFAbilities[i] != Kernel_bin.Abilities.None)
                        {
                            Kernel_bin.Abilities j = (Memory.State.Characters[Character].UnlockedGFAbilities[i]);
                            if (Source.ContainsKey(j) && skip-- <= 0)
                            {
                                Font.ColorID cid = Memory.State.Characters[Character].Commands.Contains(j) ? Font.ColorID.Grey : Font.ColorID.White;
                                BLANKS[pos] = cid == Font.ColorID.Grey ? true : false;
                                ITEM[pos, 0] = new IGMDataItem_String(
                                    Icons.ID.Ability_Command, 9,
                                Source[j].Name,
                                new Rectangle(SIZE[pos].X, SIZE[pos].Y, 0, 0), cid);
                                Contents[pos] = j;
                                pos++;
                            }
                        }
                    }
                    for (; pos < rows; pos++)
                    {
                        ITEM[pos, 0] = null;
                        BLANKS[pos] = true;
                        Contents[pos] = Kernel_bin.Abilities.None;
                    }

                    if (Contents[CURSOR_SELECT] != Kernel_bin.Abilities.None && InGameMenu_Junction.mode == Mode.Abilities_Commands)
                        ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Source[Contents[CURSOR_SELECT]].Description.ReplaceRegion();
                    UpdateTitle();
                    if (Contents[CURSOR_SELECT] == Kernel_bin.Abilities.None)
                        CURSOR_NEXT();
                    if (Pages > 1)
                    {
                        if (Contents[0] == Kernel_bin.Abilities.None)
                        {
                            Pages = Page;
                            PAGE_NEXT();
                            return Update();
                        }
                        else if (Contents[rows - 1] == Kernel_bin.Abilities.None)
                            Pages = Page + 1;
                    }
                    return base.Update();
                }
            }

            private class IGMData_Abilities_AbilityPool : IGMData_Pool<Dictionary<Kernel_bin.Abilities, Kernel_bin.Equipable_Ability>, Kernel_bin.Abilities>
            {
                public IGMData_Abilities_AbilityPool() : base(11, 1, new IGMDataItem_Box(pos: new Rectangle(435, 150, 405, 480), title: Icons.ID.ABILITY), 11, Kernel_bin.EquipableAbilities.Count / 11 + (Kernel_bin.EquipableAbilities.Count % 11 > 0 ? 1 : 0)) => Source = Kernel_bin.EquipableAbilities;

                protected override void Init()
                {
                    base.Init();
                    Hide();
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(60, 12 + (-4 * row));
                }

                public override void Inputs_OKAY()
                {
                    skipsnd = true;
                    init_debugger_Audio.PlaySound(31);
                    base.Inputs_OKAY();
                    if (Contents[CURSOR_SELECT] != Kernel_bin.Abilities.None && !BLANKS[CURSOR_SELECT])
                    {
                        int target = InGameMenu_Junction.Data[SectionName.TopMenu_Abilities].CURSOR_SELECT - 4;
                        Memory.State.Characters[Character].Abilities[target] = Contents[CURSOR_SELECT];
                        InGameMenu_Junction.mode = Mode.Abilities;
                        InGameMenu_Junction.ReInit(); // can be more specific if you want to find what is being changed.
                    }
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.mode = Mode.Abilities;
                }

                public override void UpdateTitle()
                {
                    base.UpdateTitle();
                    if (Pages == 1)
                    {
                        ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.ABILITY;
                        ITEM[11, 0] = ITEM[12, 0] = null;
                    }
                    else
                        switch (Page)
                        {
                            case 0:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.ABILITY_PG1;
                                break;

                            case 1:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.ABILITY_PG2;
                                break;

                            case 2:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.ABILITY_PG3;
                                break;

                            case 3:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.ABILITY_PG4;
                                break;
                        }
                }

                public override bool Update()
                {
                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode != Mode.Abilities_Abilities)
                        Cursor_Status &= ~Cursor_Status.Enabled;
                    else
                        Cursor_Status |= Cursor_Status.Enabled;
                    int pos = 0;
                    int skip = Page * rows;
                    for (int i = 0;
                        Memory.State.Characters != null &&
                        i < Memory.State.Characters[Character].UnlockedGFAbilities.Count &&
                        pos < rows; i++)
                    {
                        if (Memory.State.Characters[Character].UnlockedGFAbilities[i] != Kernel_bin.Abilities.None)
                        {
                            Kernel_bin.Abilities j = Memory.State.Characters[Character].UnlockedGFAbilities[i];
                            if (Source.ContainsKey(j))
                            {
                                if (skip > 0)
                                {
                                    skip--;
                                    continue;
                                }
                                Font.ColorID cid = Memory.State.Characters[Character].Abilities.Contains(j) ? Font.ColorID.Grey : Font.ColorID.White;
                                BLANKS[pos] = cid == Font.ColorID.Grey ? true : false;

                                ITEM[pos, 0] = new IGMDataItem_String(
                                    Source[j].Icon, 9,
                                Source[j].Name,
                                new Rectangle(SIZE[pos].X, SIZE[pos].Y, 0, 0), cid);
                                Contents[pos] = j;
                                pos++;
                            }
                        }
                    }
                    for (; pos < rows; pos++)
                    {
                        ITEM[pos, 0] = null;
                        BLANKS[pos] = true;
                        Contents[pos] = Kernel_bin.Abilities.None;
                    }
                    if (Contents[CURSOR_SELECT] != Kernel_bin.Abilities.None && InGameMenu_Junction.mode == Mode.Abilities_Abilities)
                        ((IGMDataItem_Box)InGameMenu_Junction.Data[SectionName.Help].CONTAINER).Data = Source[Contents[CURSOR_SELECT]].Description.ReplaceRegion();
                    UpdateTitle();
                    if (Contents[CURSOR_SELECT] == Kernel_bin.Abilities.None)
                        CURSOR_NEXT();
                    if (Pages > 1)
                    {
                        if (Contents[0] == Kernel_bin.Abilities.None)
                        {
                            Pages = Page;
                            PAGE_NEXT();
                            return Update();
                        }
                        else if (Contents[rows - 1] == Kernel_bin.Abilities.None)
                            Pages = Page + 1;
                    }
                    return base.Update();
                }
            }

            public abstract class IGMData_ConfirmDialog : IGMData
            {
                protected int startcursor;
                protected FF8String[] opt;

                public IGMData_ConfirmDialog(FF8String data, Icons.ID title, FF8String opt1, FF8String opt2, Rectangle? pos, int startcursor = 0) : base(2, 1, new IGMDataItem_Box(data, pos, title), 1, 2)
                {
                    this.startcursor = startcursor;
                    opt = new FF8String[Count];
                    opt[0] = opt1;
                    opt[1] = opt2;
                    ITEM[0, 0] = new IGMDataItem_String(opt[0], SIZE[0]);
                    ITEM[1, 0] = new IGMDataItem_String(opt[1], SIZE[1]);
                }

                protected override void Init()
                {
                    SIZE[0] = new Rectangle(212 + X, 117 + Y, 52, 30);
                    SIZE[1] = new Rectangle(212 + X, 156 + Y, 52, 30);
                    base.Init();
                    Hide();
                }

                public override void ReInit()
                {
                    base.ReInit();
                    CURSOR_SELECT = startcursor;
                    Cursor_Status |= Cursor_Status.Enabled;
                    Cursor_Status |= Cursor_Status.Vertical;
                    Cursor_Status |= Cursor_Status.Horizontal;
                }
            }

            private sealed class IGMData_ConfirmRemMag : IGMData_ConfirmDialog
            {
                public IGMData_ConfirmRemMag(FF8String data, Icons.ID title, FF8String opt1, FF8String opt2, Rectangle pos) : base(data, title, opt1, opt2, pos) => startcursor = 1;

                public override void Inputs_OKAY()
                {
                    switch (CURSOR_SELECT)
                    {
                        case 0:
                            base.Inputs_OKAY();
                            Memory.State.Characters[Character].Stat_J = Memory.State.Characters[Character].Stat_J.ToDictionary(e => e.Key, e => (byte)0);
                            skipsnd = true;
                            Inputs_CANCEL();
                            skipsnd = false;
                            InGameMenu_Junction.ReInit();
                            break;

                        case 1:
                            Inputs_CANCEL();
                            break;
                    }
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.Data[SectionName.RemMag].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu_Off;
                }
            }

            private sealed class IGMData_ConfirmRemAll : IGMData_ConfirmDialog
            {
                public IGMData_ConfirmRemAll(FF8String data, Icons.ID title, FF8String opt1, FF8String opt2, Rectangle pos) : base(data, title, opt1, opt2, pos) => startcursor = 1;

                public override void Inputs_OKAY()
                {
                    switch (CURSOR_SELECT)
                    {
                        case 0:
                            base.Inputs_OKAY();
                            Memory.State.Characters[Character].Stat_J = Memory.State.Characters[Character].Stat_J.ToDictionary(e => e.Key, e => (byte)0);
                            Memory.State.Characters[Character].Commands = Memory.State.Characters[Character].Commands.ConvertAll(Item => Kernel_bin.Abilities.None);
                            Memory.State.Characters[Character].Abilities = Memory.State.Characters[Character].Abilities.ConvertAll(Item => Kernel_bin.Abilities.None);
                            Memory.State.Characters[Character].JunctionnedGFs = Saves.GFflags.None;

                            InGameMenu_Junction.Data[SectionName.RemAll].Hide();
                            InGameMenu_Junction.Data[SectionName.TopMenu_Off].Hide();
                            InGameMenu_Junction.mode = Mode.TopMenu;
                            InGameMenu_Junction.Data[SectionName.TopMenu].CURSOR_SELECT = 0;
                            InGameMenu_Junction.ReInit();
                            break;

                        case 1:
                            Inputs_CANCEL();
                            break;
                    }
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.Data[SectionName.RemAll].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu_Off;
                }
            }

            private class IGMData_GF_Group : IGMData_Group
            {
                public IGMData_GF_Group(params IGMData[] d) : base(d)
                {
                    Hide();
                }
            }
            private class IGMData_Mag_Group : IGMData_Group
            {
                public IGMData_Mag_Group(params IGMData[] d) : base(d)
                {
                    Hide();
                }
                public override void Show()
                {
                    for (int i = 0; i < Count && ITEM[i, 0] != null; i++)
                    {
                        ((IGMDataItem_IGMData)ITEM[i, 0]).Data.Show();
                    }
                }

                public override void Hide()
                {
                    for(int i =1; i<Count && ITEM[i, 0]!=null; i++)
                    {
                        ((IGMDataItem_IGMData)ITEM[i, 0]).Data.Hide();
                    }
                }
            }

            private class IGMData_GF_Junctioned : IGMData
            {
                public IGMData_GF_Junctioned() : base(16, 1, new IGMDataItem_Box(pos: new Rectangle(0, 141, 440, 282)), 2, 8)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-45, -8);
                    SIZE[i].Offset((-10 * col), 0);
                }

                protected override void Init()
                {
                    Table_Options |= Table_Options.FillRows;
                    base.Init();
                }

                public override void ReInit()
                {
                    base.ReInit();
                    if (Memory.State.Characters != null)
                    {
                        IEnumerable<Enum> availableFlags = Enum.GetValues(typeof(Saves.GFflags)).Cast<Enum>();
                        int pos = 0;
                        foreach (Enum flag in availableFlags.Where(Memory.State.Characters[Character].JunctionnedGFs.HasFlag))
                        {
                            if ((Saves.GFflags)flag == Saves.GFflags.None) continue;
                            ITEM[pos, 0] = new IGMDataItem_String(
                            Memory.State.GFs[Saves.ConvertGFEnum[(Saves.GFflags)flag]].Name, SIZE[pos]);
                            pos++;
                        }
                        for (; pos < Count; pos++)
                            ITEM[pos, 0] = null;
                    }
                }
            }

            private class IGMData_Mag_Pool : IGMData_Pool<Saves.CharacterData, byte>
            {
                public IGMData_Mag_Pool() : base(5, 3, new IGMDataItem_Box(pos: new Rectangle(135, 150, 300, 192), title: Icons.ID.MAGIC), 4, 13)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(0, 12 + (-8 * row));
                }


                private void addMagic(ref int pos, byte spell, Font.ColorID color = Font.ColorID.White)
                {
                    ITEM[pos, 0] = new IGMDataItem_String(Kernel_bin.MagicData[spell].Name, SIZE[pos], color);
                    ITEM[pos, 2] = new IGMDataItem_Int(Source.Magics[spell], new Rectangle(SIZE[pos].X + SIZE[pos].Width - 50, SIZE[pos].Y, 0, 0), spaces: 3);
                    BLANKS[pos] = false;
                    Contents[pos] = spell;
                    pos++;
                }

                protected override void Init()
                {
                    base.Init();
                    SIZE[rows] = SIZE[0];
                    SIZE[rows].Y = Y;
                    ITEM[rows, 2] = new IGMDataItem_Icon(Icons.ID.NUM_, new Rectangle(SIZE[rows].X + SIZE[rows].Width - 45, SIZE[rows].Y, 0, 0), scale: new Vector2(2.5f));
                    BLANKS[rows] = true;
                }

                public override void ReInit()
                {
                    if (Memory.State.Characters != null)
                    {
                        Source = Memory.State.Characters[Character];

                        int pos = 0;
                        int skip = Page * rows;
                        for (byte i = 1; i < Kernel_bin.MagicData.Length && pos < rows; i++)
                        {
                            if (Source.Magics.ContainsKey(i) && skip-- <= 0)
                            {
                                addMagic(ref pos, i, Font.ColorID.White);
                            }
                        }
                        for (; pos < rows; pos++)
                        {
                            ITEM[pos, 0] = null;
                            ITEM[pos, 1] = null;
                            ITEM[pos, 2] = null;
                            BLANKS[pos] = true;
                        }
                        base.ReInit();
                        UpdateTitle();
                        UpdateCharacter();
                    }
                }

                public override void UpdateTitle()
                {
                    base.UpdateTitle();
                    if (Pages == 1)
                    {
                        ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.MAGIC;
                        ITEM[Count - 1, 0] = ITEM[Count - 2, 0] = null;
                    }
                    else
                        if (Page < Pages)
                        ((IGMDataItem_Box)CONTAINER).Title = (Icons.ID)((int)Icons.ID.MAGIC_PG1 + Page);
                }
                public override bool Update()
                {

                    if (InGameMenu_Junction != null && InGameMenu_Junction.mode == Mode.Mag_Pool && Enabled)
                    {
                        Cursor_Status |= Cursor_Status.Enabled;
                        Cursor_Status &= ~Cursor_Status.Horizontal;
                        Cursor_Status |= Cursor_Status.Vertical;
                        Cursor_Status &= ~Cursor_Status.Blinking;
                    }
                    else
                    {
                        Cursor_Status &= ~Cursor_Status.Enabled;
                    }
                    return base.Update();
                }

                private void UpdateCharacter()
                {
                    if (InGameMenu_Junction != null)
                    {
                        var m = Contents[CURSOR_SELECT];
                        IGMDataItem_IGMData i = (IGMDataItem_IGMData)((IGMData_GF_Group)InGameMenu_Junction.Data[SectionName.TopMenu_GF_Group]).ITEM[2, 0];
                    }
                }

                protected override void PAGE_PREV()
                {
                    base.PAGE_PREV();
                    ReInit();
                }

                protected override void PAGE_NEXT()
                {
                    base.PAGE_NEXT();
                    ReInit();
                }

                public override int CURSOR_PREV()
                {
                    int ret = base.CURSOR_PREV();
                    UpdateCharacter();
                    return ret;
                }

                public override int CURSOR_NEXT()
                {
                    int ret = base.CURSOR_NEXT();
                    UpdateCharacter();
                    return ret;
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    //TODO have pool return to correct screen as there will be possible return modes.
                    InGameMenu_Junction.mode = Mode.Mag_Stat;
                }

                public override void Inputs_OKAY()
                {
                    skipsnd = true;
                    init_debugger_Audio.PlaySound(31);
                    base.Inputs_OKAY();
                    InGameMenu_Junction.ReInit();
                }
            }

            private class IGMData_GF_Pool : IGMData_Pool<Saves.Data, GFs>
            {
                public IGMData_GF_Pool() : base(5, 3, new IGMDataItem_Box(pos: new Rectangle(440, 149, 385, 193), title: Icons.ID.GF), 4, 4)
                {
                }

                protected override void InitShift(int i, int col, int row)
                {
                    base.InitShift(i, col, row);
                    SIZE[i].Inflate(-22, -8);
                    SIZE[i].Offset(0, 12 + (-8 * row));
                }

                public Dictionary<GFs, Characters> JunctionedGFs { get; private set; }
                public List<GFs> UnlockedGFs { get; private set; }

                private void addGF(ref int pos, GFs g, Font.ColorID color = Font.ColorID.White)
                {
                    ITEM[pos, 0] = new IGMDataItem_String(Memory.Strings.GetName(g), SIZE[pos], color);
                    ITEM[pos, 1] = JunctionedGFs.ContainsKey(g) ? new IGMDataItem_Icon(Icons.ID.JunctionSYM, new Rectangle(SIZE[pos].X + SIZE[pos].Width - 100, SIZE[pos].Y, 0, 0)) : null;
                    ITEM[pos, 2] = new IGMDataItem_Int(Source.GFs[g].Level, new Rectangle(SIZE[pos].X + SIZE[pos].Width - 50, SIZE[pos].Y, 0, 0), spaces: 3);
                    BLANKS[pos] = false;
                    Contents[pos] = g;
                    pos++;
                }

                protected override void Init()
                {
                    base.Init();
                    SIZE[rows] = SIZE[0];
                    SIZE[rows].Y = Y;
                    ITEM[rows, 2] = new IGMDataItem_Icon(Icons.ID.Size_16x08_Lv_, new Rectangle(SIZE[rows].X + SIZE[rows].Width - 30, SIZE[rows].Y, 0, 0), scale: new Vector2(2.5f));
                }

                public override void ReInit()
                {
                    Source = Memory.State;
                    JunctionedGFs = Source.JunctionedGFs();
                    UnlockedGFs = Source.UnlockedGFs();

                    int pos = 0;
                    int skip = Page * rows;
                    foreach (GFs g in UnlockedGFs.Where(g => !JunctionedGFs.ContainsKey(g)))
                    {
                        if (pos >= rows) break;
                        if (skip-- <= 0)
                        {
                            addGF(ref pos, g);
                        }
                    }
                    foreach (GFs g in UnlockedGFs.Where(g => JunctionedGFs.ContainsKey(g) && JunctionedGFs[g] == Character))
                    {
                        if (pos >= rows) break;
                        if (skip-- <= 0)
                        {
                            addGF(ref pos, g, Font.ColorID.Grey);
                        }
                    }
                    foreach (GFs g in UnlockedGFs.Where(g => JunctionedGFs.ContainsKey(g) && JunctionedGFs[g] != Character))
                    {
                        if (pos >= rows) break;
                        if (skip-- <= 0)
                        {
                            addGF(ref pos, g, Font.ColorID.Dark_Gray);
                        }
                    }
                    for (; pos < rows; pos++)
                    {
                        ITEM[pos, 0] = null;
                        ITEM[pos, 1] = null;
                        ITEM[pos, 2] = null;
                        BLANKS[pos] = true;
                    }
                    base.ReInit();
                    UpdateTitle();
                    UpdateCharacter();
                }

                public override void UpdateTitle()
                {
                    base.UpdateTitle();
                    if (Pages == 1)
                    {
                        ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.GF;
                        ITEM[Count - 1, 0] = ITEM[Count - 2, 0] = null;
                    }
                    else
                        switch (Page)
                        {
                            case 0:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.GF_PG1;
                                break;

                            case 1:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.GF_PG2;
                                break;

                            case 2:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.GF_PG3;
                                break;

                            case 3:
                                ((IGMDataItem_Box)CONTAINER).Title = Icons.ID.GF_PG4;
                                break;
                        }
                }

                private void UpdateCharacter()
                {
                    if (InGameMenu_Junction != null)
                    {
                        GFs g = Contents[CURSOR_SELECT];
                        IGMDataItem_IGMData i = (IGMDataItem_IGMData)((IGMData_GF_Group)InGameMenu_Junction.Data[SectionName.TopMenu_GF_Group]).ITEM[2, 0];
                        ((IGMDataItem_Box)i.Data.CONTAINER).Data = JunctionedGFs.Count > 0 && JunctionedGFs.ContainsKey(g) ? Memory.Strings.GetName(JunctionedGFs[g]) : null;
                    }
                }

                protected override void PAGE_PREV()
                {
                    base.PAGE_PREV();
                    ReInit();
                }

                protected override void PAGE_NEXT()
                {
                    base.PAGE_NEXT();
                    ReInit();
                }

                public override int CURSOR_PREV()
                {
                    int ret = base.CURSOR_PREV();
                    UpdateCharacter();
                    return ret;
                }

                public override int CURSOR_NEXT()
                {
                    int ret = base.CURSOR_NEXT();
                    UpdateCharacter();
                    return ret;
                }

                public override void Inputs_CANCEL()
                {
                    base.Inputs_CANCEL();
                    InGameMenu_Junction.Data[SectionName.TopMenu_GF_Group].Hide();
                    InGameMenu_Junction.mode = Mode.TopMenu_Junction;
                }

                public override void Inputs_OKAY()
                {
                    skipsnd = true;
                    init_debugger_Audio.PlaySound(31);
                    base.Inputs_OKAY();
                    GFs select = Contents[CURSOR_SELECT];
                    Characters c = JunctionedGFs.ContainsKey(select) ? JunctionedGFs[select] : Character;
                    if (c != Characters.Blank)
                    {
                        if (c != Character)
                        {
                            //show error msg
                        }
                        else
                        {
                            //Purge everything that you can't have anymore. Because the GF provided for you.
                            List<Kernel_bin.Abilities> a = (Source.Characters[c]).UnlockedGFAbilities;
                            Source.Characters[c].JunctionnedGFs ^= Saves.ConvertGFEnum.FirstOrDefault(x => x.Value == select).Key;
                            List<Kernel_bin.Abilities> b = (Source.Characters[c]).UnlockedGFAbilities;
                            foreach (Kernel_bin.Abilities r in a.Except(b).Where(v => !Kernel_bin.Junctionabilities.ContainsKey(v)))
                            {
                                if (Kernel_bin.Commandabilities.ContainsKey(r))
                                {
                                    Source.Characters[c].Commands.Remove(r);
                                    Source.Characters[c].Commands.Add(Kernel_bin.Abilities.None);
                                }
                                else if (Kernel_bin.EquipableAbilities.ContainsKey(r))
                                {
                                    Source.Characters[c].Abilities.Remove(r);
                                    Source.Characters[c].Abilities.Add(Kernel_bin.Abilities.None);
                                }
                            }
                            foreach (Kernel_bin.Abilities r in a.Except(b).Where(v => Kernel_bin.Junctionabilities.ContainsKey(v)))
                            {
                                if (Kernel_bin.Stat2Ability.ContainsValue(r))
                                    switch (r)
                                    {
                                        case Kernel_bin.Abilities.ST_Atk_J:
                                            Source.Characters[c].Stat_J[Kernel_bin.Stat.ST_Atk] = 0;
                                            break;

                                        case Kernel_bin.Abilities.Elem_Atk_J:
                                            Source.Characters[c].Stat_J[Kernel_bin.Stat.Elem_Atk] = 0;
                                            break;

                                        case Kernel_bin.Abilities.Elem_Def_Jx1:
                                        case Kernel_bin.Abilities.Elem_Def_Jx2:
                                        case Kernel_bin.Abilities.Elem_Def_Jx4:
                                            int count = 0;
                                            if (b.Contains(Kernel_bin.Abilities.Elem_Def_Jx4))
                                                count = 4;
                                            else if (b.Contains(Kernel_bin.Abilities.Elem_Def_Jx2))
                                                count = 2;
                                            else if (b.Contains(Kernel_bin.Abilities.Elem_Def_Jx1))
                                                count = 1;
                                            for (; count < 4; count++)
                                                Source.Characters[c].Stat_J[Kernel_bin.Stat.Elem_Def_1+count] = 0;
                                            break;

                                        case Kernel_bin.Abilities.ST_Def_Jx1:
                                        case Kernel_bin.Abilities.ST_Def_Jx2:
                                        case Kernel_bin.Abilities.ST_Def_Jx4:
                                            count = 0;
                                            if (b.Contains(Kernel_bin.Abilities.ST_Def_Jx4))
                                                count = 4;
                                            else if (b.Contains(Kernel_bin.Abilities.ST_Def_Jx2))
                                                count = 2;
                                            else if (b.Contains(Kernel_bin.Abilities.ST_Def_Jx1))
                                                count = 1;
                                            for (; count < 4; count++)
                                                Source.Characters[c].Stat_J[Kernel_bin.Stat.ST_Def_1 + count] = 0;
                                            break;

                                        case Kernel_bin.Abilities.Abilityx3:
                                        case Kernel_bin.Abilities.Abilityx4:
                                            count = 2;
                                            if (b.Contains(Kernel_bin.Abilities.Abilityx4))
                                                count = 4;
                                            else if (b.Contains(Kernel_bin.Abilities.Abilityx3))
                                                count = 3;
                                            for (; count < Source.Characters[c].Abilities.Count; count++)
                                                Source.Characters[c].Abilities[count] = Kernel_bin.Abilities.None;
                                            break;

                                        default:
                                            Source.Characters[c].Stat_J[Kernel_bin.Stat2Ability.FirstOrDefault(x => x.Value == r).Key] = 0;
                                            break;
                                    }
                            }
                            InGameMenu_Junction.ReInit();
                        }
                    }
                }
            }
        }
    }
}