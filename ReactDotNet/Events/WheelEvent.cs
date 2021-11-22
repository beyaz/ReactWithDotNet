using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class WheelEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private WheelEvent() { }

		public readonly int DeltaMode;
		public readonly int DeltaX;
		public readonly int DeltaY;
		public readonly int DeltaZ;
	}
}
