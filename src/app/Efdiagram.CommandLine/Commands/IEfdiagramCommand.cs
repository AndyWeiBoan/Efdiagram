using System;
using System.Collections.Generic;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace Efdiagram.CommandLine.Commands {
    public interface IEfdiagramCommand {
        string Name { get; }
        string Description { get; }
        void Register(CommandLineApplication app);
    }
}
