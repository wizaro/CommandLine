// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System.CommandLine;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Returns an option instance as a built option.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class InstanceOptionBuilder
        : IOptionBuilder
    {
        /// <summary>
        ///     Option instance.
        /// </summary>
        private readonly Option option;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceOptionBuilder"/> class.
        /// </summary>
        /// <param name="option">Option instance.</param>
        public InstanceOptionBuilder(Option option)
        {
            this.option = option;
        }

        /// <inheritdoc/>
        public Option Build()
        {
            return this.option;
        }
    }
}