using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class TouchEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private TouchEvent() { }

		public readonly bool AltKey;
		public readonly bool CtrlKey;
		public readonly bool MetaKey;
		public readonly bool ShiftKey;

		public readonly Touch[] ChangedTouches;
		public readonly Touch[] TargetTouches;
		public readonly Touch[] Touches;

		[External]
		public extern bool GetModifierState(int key);
	}
}
