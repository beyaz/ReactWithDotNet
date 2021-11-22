using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class FocusEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private FocusEvent() { }

		public readonly Bridge.Html5.Element RelatedTarget;
	}
}
