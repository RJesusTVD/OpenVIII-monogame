﻿namespace OpenVIII.Fields.Scripts.Instructions
{
    /// <summary>
    /// Make this entity face the PC. Speed is number of frames (larger = slower turn).
    /// </summary>
    internal sealed class PCTURN : JsmInstruction
    {
        #region Fields

        private readonly IJsmExpression _frameDuration;
        private readonly IJsmExpression _unknown;

        #endregion Fields

        #region Constructors

        public PCTURN(IJsmExpression unknown, IJsmExpression frameDuration)
        {
            _unknown = unknown;
            _frameDuration = frameDuration;
        }

        public PCTURN(int parameter, IStack<IJsmExpression> stack)
            : this(
                frameDuration: stack.Pop(),
                unknown: stack.Pop())
        {
        }

        #endregion Constructors

        #region Methods

        public override void Format(ScriptWriter sw, IScriptFormatterContext formatterContext, IServices services) => sw.Format(formatterContext, services)
                .Await()
                .Property(nameof(FieldObject.Model))
                .Method(nameof(FieldObjectModel.RotateToPlayer))
                .Argument("unknown", _unknown)
                .Argument("frameDuration", _frameDuration)
                .Comment(nameof(PCTURN));

        public override IAwaitable TestExecute(IServices services)
        {
            var currentObject = ServiceId.Field[services].Engine.CurrentObject;

            var unknown = _unknown.Int32(services);
            var frameDuration = _frameDuration.Int32(services);
            currentObject.Model.RotateToPlayer(unknown, frameDuration);

            return DummyAwaitable.Instance;
        }

        public override string ToString() => $"{nameof(PCTURN)}({nameof(_unknown)}: {_unknown}, {nameof(_frameDuration)}: {_frameDuration})";

        #endregion Methods
    }
}