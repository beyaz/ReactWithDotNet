using Bridge;
using Bridge.Html5;

namespace ReactDotNet
{
	[External]
	[ObjectLiteral]
	public sealed class ImageEvent<TCurrentTarget> : SyntheticEvent<TCurrentTarget> where TCurrentTarget : Bridge.Html5.Element
	{
		private ImageEvent() { }
	}
}
