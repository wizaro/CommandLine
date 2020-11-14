// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Represents a class used to integrate host services to a binding process.
    /// </summary>
    public interface IHostServicesBinder
    {
        /// <summary>
        ///     Defines a type from the host to be used in the binding process.
        /// </summary>
        /// <typeparam name="T">Type to be used from the host.</typeparam>
        /// <returns>This instance to allow chaining.</returns>
        IHostServicesBinder Use<T>()
            where T : notnull;
    }
}