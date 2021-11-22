using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class CompositionEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private CompositionEvent() { }

		public readonly string Data;
	}
}
