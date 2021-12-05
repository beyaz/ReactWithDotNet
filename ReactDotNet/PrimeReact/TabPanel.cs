namespace ReactDotNet.PrimeReact
{
    public class TabPanel: ElementCollection
    {
        /// <summary>
        ///     Orientation of tab headers.
        ///     <para>default: null</para>
        /// </summary>
        [React]
        public string header { get; set; }

        /// <summary>
        ///     Icons can be placed at left of a header.
        ///     <para>default: null</para>
        /// </summary>
        [React]
        public string leftIcon { get; set; }

        /// <summary>
        ///     Defines if tab can be removed.
        ///     <para>default: false</para>
        /// </summary>
        [React]
        public bool closable { get; set; }

    }
}