// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Builder;
using WiZaRo.CommandLine.Support;

namespace WiZaRo.CommandLine.Fluent
{
    /// <summary>
    ///     Extensions for the command line interface builder.
    /// </summary>
    /// <threadsafety static="false"/>
    public static class CommandLineExtensions
    {
        /// <summary>
        ///     Configures services from the host to use during command binding.
        /// </summary>
        /// <param name="builder">Command line builder.</param>
        /// <param name="binder">The action to integrate host services to the binding process.</param>
        /// <returns>The <paramref name="builder"/> to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="builder"/> or <paramref name="binder"/> is <see langword="null"/>.</exception>
        [CLSCompliant(false)]
        public static CommandLineBuilder FromHostServices(this CommandLineBuilder builder, Action<IHostServicesBinder> binder)
        {
            if (builder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(builder));
            }
            else if (binder is null)
            {
                throw Exceptions.BuildArgumentNull(nameof(binder));
            }

            return builder.UseMiddleware(
                async (context, next) =>
                {
                    binder(new HostServicesBinder(context.BindingContext));
                    await next(context).ConfigureAwait(false);
                });
        }
    }
}