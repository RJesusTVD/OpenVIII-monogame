﻿using System;

namespace OpenVIII.Fields.Scripts
{
    public static partial class Jsm
    {
        public static partial class Expression
        {
            /// <summary>
            /// <para>Push actor code???</para>
            /// <para>Push the entity BattleID of an actor onto the stack. This is always used before a call to CTURN to set which character will be faced. IDK what happens when you try to use literals for CTURN or why this function is necessary.</para>
            /// </summary>
            /// <see cref="http://wiki.ffrtt.ru/index.php?title=FF8/Field/Script/Opcodes/013_PSHAC"/>
            public sealed class PSHAC : IJsmExpression, IConstExpression
            {
                /// <summary>
                /// BattleID of the entity to reference
                /// </summary>
                private Jsm.FieldObjectId _fieldObjectId;

                /// <summary>
                /// Push the entity BattleID of an actor onto the stack. This is always used before a call to CTURN to set which character will be faced. IDK what happens when you try to use literals for CTURN or why this function is necessary. 
                /// </summary>
                /// <param name="fieldObjectId">Char value</param>
                public PSHAC(Jsm.FieldObjectId fieldObjectId)
                {
                    _fieldObjectId = fieldObjectId;
                }

                public override String ToString()
                {
                    return $"{nameof(PSHAC)}({nameof(_fieldObjectId)}: {_fieldObjectId})";
                }

                public void Format(ScriptWriter sw, IScriptFormatterContext formatterContext, IServices services)
                {
                    String name = formatterContext.GetObjectNameByIndex(_fieldObjectId.Value);
                    sw.Append($"typeof({name})");
                }

                public IJsmExpression Evaluate(IServices services)
                {
                    return this;
                }

                public Int64 Calculate(IServices services)
                {
                    return _fieldObjectId.Value;
                }

                ILogicalExpression ILogicalExpression.LogicalInverse()
                {
                    return ValueExpression.Create(_fieldObjectId.Value == 0 ? 1 : 0);
                }

                Int64 IConstExpression.Value => _fieldObjectId.Value;
            }
        }
    }
}