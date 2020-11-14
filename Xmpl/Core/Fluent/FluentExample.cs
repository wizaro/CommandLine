// © 2020 Wilhelm Zapiain Rodríguez.
// Licensed under the MIT license. See the LICENSE file in the project root for full license information.

using System;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WiZaRo.CommandLine.Fluent;

namespace WiZaRo.CommandLine.Examples.Fluent
{
    public static class FluentExample
    {
        // The program will have two commands:
        // - HelloWorld: Prints 'Hello World!' in the console.
        // - Show -m "{message}": Prints '~~~**«{ {message} }»**~~~' in the console.
        public static Task<int> Main(string[] args)
        {
            return Start.DefineCommandLine(rootBuilder => rootBuilder
                    // The following defines the HelloWorld command.
                    .AddCommand("HelloWorld", commandBuilder => commandBuilder
                        .SetDescription("Salutes the globe.")
                        .UseHandler(CommandHandler.Create(HelloWorld)))

                    // The following defines the Show command with the '--message' required option.
                    .AddCommand("Show", commandBuilder => commandBuilder
                        .SetDescription("Shows a message prettily.")
                        .AddOption<string>(new[] { "--message", "-m" }, optionBuilder => optionBuilder
                            .Require())
                        .UseHandler(CommandHandler.Create<string, IFormatter>(Show))))

                .UseDefaults()
                .UseHost(hostBuilder => hostBuilder
                    .ConfigureServices((context, services) => services
                        .AddSingleton<IFormatter, PrettyFormatter>()))

                // The following allows to use the IFormatter in the host services to bind to the commands' parameters.
                .FromHostServices(binder => binder
                    .Use<IFormatter>())

                .Build()
                .InvokeAsync(args);
        }

        // The handler for the 'HelloWorld' command.
        public static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }

        // The handler for the 'Show' command.
        public static void Show(string message, IFormatter formatter)
        {
            Console.WriteLine(formatter?.Format(message ?? string.Empty) ?? string.Empty);
        }
    }

    // A simple interface that formats messages.
    public interface IFormatter
    {
        string Format(string message);
    }

    // An implementation of the previous interface that adorns the message.
    public class PrettyFormatter
        : IFormatter
    {
        public string Format(string message)
        {
            return new StringBuilder()
                .Append("~~~**«{ ")
                .Append(message)
                .Append(" }»**~~~")
                .ToString();
        }
    }
}