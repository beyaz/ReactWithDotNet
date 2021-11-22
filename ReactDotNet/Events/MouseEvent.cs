using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class MouseEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private MouseEvent() { }

		public readonly bool AltKey;
		public readonly int Button;
		public readonly int Buttons;
		public readonly int ClientX;
		public readonly int ClientY;
		public readonly bool CtrlKey;
		public readonly bool MetaKey;
		public readonly int PageX;
		public readonly int PageY;
		public readonly Bridge.Html5.Element RelatedTarget;
		public readonly int ScreenX;
		public readonly int ScreenY;
		public readonly bool ShiftKey;
		
		[External]
		public extern bool GetModifierState(int key);
	}
}
