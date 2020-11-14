// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Returns an argument instance as a built argument.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class InstanceArgumentBuilder
        : IArgumentBuilder
    {
        /// <summary>
        ///     Argument instance.
        /// </summary>
        private readonly Argument argument;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceArgumentBuilder"/> class.
        /// </summary>
        /// <param name="argument">Argument instance.</param>
        internal InstanceArgumentBuilder(Argument argument)
        {
            this.argument = argument;
        }

        /// <inheritdoc/>
        public Argument Build()
        {
            return this.argument;
        }
    }
}