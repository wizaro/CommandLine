// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Binding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WiZaRo.CommandLine
{
    /// <summary>
    ///     <see langword="abstract"/>class that integrates host services to the binding process.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    internal sealed class HostServicesBinder
        : IHostServicesBinder
    {
        /// <summary>
        ///     The binding context.
        /// </summary>
        private readonly BindingContext bindingContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HostServicesBinder"/> class.
        /// </summary>
        /// <param name="bindingContext">The binding context.</param>
        public HostServicesBinder(BindingContext bindingContext)
        {
            this.bindingContext = bindingContext;
        }

        /// <inheritdoc/>
        public IHostServicesBinder Use<T>()
            where T : notnull
        {
            this.bindingContext.AddService(AddServiceCore);
            return this;

            static T AddServiceCore(IServiceProvider services)
            {
                return services.GetRequiredService<IHost>().Services.GetRequiredService<T>();
            }
        }
    }
}